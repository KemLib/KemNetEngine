using System.Net;
using System.Net.Sockets;

namespace KemNetEngine.Network
{
    public static class SocketUtilities
    {
        #region Properties
        private const string NAME_FORMAT_REMOTE = "Remote[{0}]",
            NAME_FORMAT_LOCAL = "Local[{0}]",
            NAME_FORMAT_REMOTE_LOCAL = "Remote[{0}]_Local[{1}]";
        private const string LOG_RECEIVE_FAIL_REMOTE_CLOSE = "Remote client close the connection",
            LOG_RECEIVE_FAIL_CONVERT_MESSAGE_LENGTH = "Convert message length fail";

        public delegate void OnConnect(ResultConnect result);
        public delegate void OnConnect<T>(ResultConnect result, T state);
        public delegate void OnConnect<T1, T2>(ResultConnect result, T1 state1, T2 state2);
        public delegate void OnConnect<T1, T2, T3>(ResultConnect result, T1 state1, T2 state2, T3 state3);
        public delegate void OnConnect<T1, T2, T3, T4>(ResultConnect result, T1 state1, T2 state2, T3 state3, T4 state4);
        public delegate void OnSend(ResultSend result);
        public delegate void OnSend<T>(ResultSend result, T state);
        public delegate void OnSend<T1, T2>(ResultSend result, T1 state1, T2 state2);
        public delegate void OnSend<T1, T2, T3>(ResultSend result, T1 state1, T2 state2, T3 state3);
        public delegate void OnSend<T1, T2, T3, T4>(ResultSend result, T1 state1, T2 state2, T3 state3, T4 state4);
        public delegate void OnReceive(ResultReceive result);
        public delegate void OnReceive<T>(ResultReceive result, T state);
        public delegate void OnReceive<T1, T2>(ResultReceive result, T1 state1, T2 state2);
        public delegate void OnReceive<T1, T2, T3>(ResultReceive result, T1 state1, T2 state2, T3 state3);
        public delegate void OnReceive<T1, T2, T3, T4>(ResultReceive result, T1 state1, T2 state2, T3 state3, T4 state4);
        #endregion

        #region Methods
        public static bool TryGetText(EndPoint? localEndPoint, EndPoint? remoteEndPoint, out string text)
        {
            if (localEndPoint == null)
            {
                if (remoteEndPoint == null)
                {
                    text = string.Empty;
                    return false;
                }
                else
                {
                    text = string.Format(NAME_FORMAT_REMOTE, remoteEndPoint.ToString());
                    return true;
                }
            }
            else
            {
                if (remoteEndPoint == null)
                {
                    text = string.Format(NAME_FORMAT_LOCAL, localEndPoint.ToString());
                    return true;
                }
                else
                {
                    text = string.Format(NAME_FORMAT_REMOTE_LOCAL, remoteEndPoint.ToString(), localEndPoint.ToString());
                    return true;
                }
            }
        }
        #endregion

        #region Socket
        public static bool Socket_IsConnect(Socket? socket)
        {
            if (socket == null)
                return false;
            try
            {
                return socket.Connected;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static void Socket_Close(Socket? socket)
        {
            if (socket == null)
                return;
            try
            {
                socket.Shutdown(SocketShutdown.Both);
            }
            catch (Exception)
            {

            }
            finally
            {
                socket.Close();
            }
        }
        #endregion

        #region Send
        internal static ResultSend SendMessage(Socket socket, ReadOnlyMemory<byte> message)
        {
            byte[] messageTitle;
            try
            {
                if (message.Length == 0)
                    messageTitle = BitConverter.GetBytes(0);
                else
                    messageTitle = BitConverter.GetBytes(message.Length);
            }
            catch (Exception)
            {
                return ResultSend.Fail(LOG_RECEIVE_FAIL_CONVERT_MESSAGE_LENGTH, SocketError.SocketError);
            }
            //
            try
            {
                socket.Send(messageTitle);
                socket.Send(message.Span);
                return ResultSend.Success();
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.ConnectionReset)
                    return ResultSend.Fail(LOG_RECEIVE_FAIL_REMOTE_CLOSE, ex.SocketErrorCode);
                else
                    return ResultSend.Fail(ex.Message, ex.SocketErrorCode);
            }
            catch (Exception ex)
            {
                return ResultSend.Fail(ex.Message, SocketError.SocketError);
            }
        }
        internal static async Task<ResultSend> SendMessageAsync(Socket socket, ReadOnlyMemory<byte> message, CancellationToken cancellationToken = default)
        {
            byte[] messageTitle;
            try
            {
                if (message.Length == 0)
                    messageTitle = BitConverter.GetBytes(0);
                else
                    messageTitle = BitConverter.GetBytes(message.Length);
            }
            catch (Exception)
            {
                return ResultSend.Fail(LOG_RECEIVE_FAIL_CONVERT_MESSAGE_LENGTH, SocketError.SocketError);
            }
            //
            try
            {
                await socket.SendAsync(messageTitle, cancellationToken);
                await socket.SendAsync(message, cancellationToken);
                return ResultSend.Success();
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.ConnectionReset)
                    return ResultSend.Fail(LOG_RECEIVE_FAIL_REMOTE_CLOSE, ex.SocketErrorCode);
                else
                    return ResultSend.Fail(ex.Message, ex.SocketErrorCode);
            }
            catch (Exception ex)
            {
                return ResultSend.Fail(ex.Message, SocketError.SocketError);
            }
        }
        #endregion

        #region Receive
        internal static ResultReceive ReceiveMessage(Socket socket)
        {
            byte[] messageTitle = new byte[4];
            ResultReceive result = ReceiveMessage(socket, messageTitle);
            if (!result.IsSuccess)
                return result;
            //
            int messageLength;
            try
            {
                messageLength = BitConverter.ToInt32(messageTitle, 0);
            }
            catch (Exception)
            {
                return ResultReceive.Fail(LOG_RECEIVE_FAIL_CONVERT_MESSAGE_LENGTH, SocketError.SocketError);
            }
            //
            byte[] message = new byte[messageLength];
            return ReceiveMessage(socket, message);
        }
        internal static async Task<ResultReceive> ReceiveMessageAsync(Socket socket, CancellationToken cancellationToken = default)
        {
            byte[] messageTitle = new byte[4];
            ResultReceive result = await ReceiveMessageAsync(socket, messageTitle, cancellationToken);
            if (!result.IsSuccess)
                return result;
            //
            int messageLength;
            try
            {
                messageLength = BitConverter.ToInt32(messageTitle, 0);
            }
            catch (Exception)
            {
                return ResultReceive.Fail(LOG_RECEIVE_FAIL_CONVERT_MESSAGE_LENGTH, SocketError.SocketError);
            }
            //
            byte[] message = new byte[messageLength];
            return await ReceiveMessageAsync(socket, message, cancellationToken);
        }
        private static ResultReceive ReceiveMessage(Socket socket, byte[] dataBuffer)
        {
            int length = dataBuffer.Length,
                offset = 0;
            while (offset < length)
            {
                try
                {
                    int count = socket.Receive(dataBuffer.AsSpan(offset));
                    if (count == 0)
                        return ResultReceive.Fail(LOG_RECEIVE_FAIL_REMOTE_CLOSE, SocketError.ConnectionReset);
                    offset += count;
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.ConnectionReset)
                        return ResultReceive.Fail(LOG_RECEIVE_FAIL_REMOTE_CLOSE, ex.SocketErrorCode);
                    else
                        return ResultReceive.Fail(ex.Message, ex.SocketErrorCode);
                }
                catch (Exception ex)
                {
                    return ResultReceive.Fail(ex.Message, SocketError.SocketError);
                }
            }
            return ResultReceive.Success(dataBuffer);
        }
        private static async Task<ResultReceive> ReceiveMessageAsync(Socket socket, byte[] dataBuffer, CancellationToken cancellationToken = default)
        {
            int length = dataBuffer.Length,
                offset = 0;
            while (offset < length)
            {
                try
                {
                    int count = await socket.ReceiveAsync(dataBuffer.AsMemory(offset), cancellationToken);
                    if (count == 0)
                        return ResultReceive.Fail(LOG_RECEIVE_FAIL_REMOTE_CLOSE, SocketError.ConnectionReset);
                    offset += count;
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.ConnectionReset)
                        return ResultReceive.Fail(LOG_RECEIVE_FAIL_REMOTE_CLOSE, ex.SocketErrorCode);
                    else
                        return ResultReceive.Fail(ex.Message, ex.SocketErrorCode);
                }
                catch (Exception ex)
                {
                    return ResultReceive.Fail(ex.Message, SocketError.SocketError);
                }
            }
            return ResultReceive.Success(dataBuffer);
        }
        #endregion
    }
}

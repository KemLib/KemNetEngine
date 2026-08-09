using KemLibCore.Concurrent.Inter;
using KemNetEngine.Network;
using System.Net.Sockets;

namespace TestNetEngine.Net.Network
{
    internal class ClientManager(Action<string> onLog)
    {
        #region Properties
        private const string ACCEPT_SUCCESS = "Client accept: ",
            CLIENT_ADD_FAIL = "Remove fail: ",
            CLIENT_CLOSE = "Client closed {0}: {1}",
            RECEIVE_FAIL = "Client {0} Receive fail: {1}",
            SEND_FAIL = "Client {0} Send fail: {1}";

        private readonly Action<string> onLog = onLog;
        private InterBoolStruct interRun = new();
        private SpinLock lockStreams = new();
        private readonly List<IClient> streams = [];

        #endregion

        #region Construction

        #endregion

        #region Stream
        public void Start()
        {
            interRun.Exchange(true);
        }
        public void Stop()
        {
            if (!interRun.TryExchange(false))
                return;
            //
            bool lockToken = false;
            try
            {
                lockStreams.Enter(ref lockToken);
                if (lockToken)
                {
                    foreach (var client in streams)
                    {
                        client.Close();
                    }
                    streams.Clear();
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (lockToken)
                    lockStreams.Exit();
            }
        }
        public void Accept(IClient client)
        {
            onLog?.Invoke(ACCEPT_SUCCESS + client.Name);
            //
            bool lockToken = false;
            try
            {
                lockStreams.Enter(ref lockToken);
                if (lockToken)
                {
                    streams.Add(client);
                    client.Receive(Client_OnReceive, client);
                }
                else
                {
                    onLog?.Invoke(CLIENT_ADD_FAIL + client.Name);
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                onLog?.Invoke(CLIENT_ADD_FAIL + ex.Message);
                client.Close();
            }
            finally
            {
                if (lockToken)
                    lockStreams.Exit();
            }
        }
        private IClient[] GetClients()
        {
            IClient[] clients;
            //
            bool lockToken = false;
            try
            {
                lockStreams.Enter(ref lockToken);
                if (lockToken)
                {
                    int index = 0;
                    while (index < streams.Count)
                    {
                        IClient client = streams[index];
                        if (!client.IsConnect)
                        {
                            streams.RemoveAt(index);
                            _ = Task.Run(() =>
                            {
                                string message = string.Format(CLIENT_CLOSE, client.CloseFrom.ToString(), client.Name);
                                onLog?.Invoke(message);
                            });
                            break;
                        }
                        //
                        index++;
                    }
                    //
                    clients = [.. streams];
                }
                else
                {
                    clients = [];
                }

            }
            catch (Exception)
            {
                clients = [];
            }
            finally
            {
                if (lockToken)
                    lockStreams.Exit();
            }
            //
            return clients;
        }
        private void Client_OnReceive(ResultReceive result, IClient client)
        {
            if (!interRun.Value)
                return;
            //
            IClient[] clients = GetClients();
            if (result.IsSuccess)
            {
                foreach (var tmpClient in clients)
                    tmpClient.Send(result.Data, Client_OnSend, tmpClient);
                if (client.IsConnect)
                    client.Receive(Client_OnReceive, client);
            }
            else
            {
                if (result.ErrorCode != SocketError.NotConnected && result.ErrorCode != SocketError.ConnectionReset)
                {
                    string error = string.Format(RECEIVE_FAIL, client.Name, result.ErrorMessage);
                    onLog?.Invoke(error);
                }
            }
        }
        private void Client_OnSend(ResultSend result, IClient client)
        {
            if (!interRun.Value)
                return;
            //
            if (!result.IsSuccess)
            {
                if (result.ErrorCode != SocketError.NotConnected && result.ErrorCode != SocketError.ConnectionReset)
                {
                    string error = string.Format(SEND_FAIL, client.Name, result.ErrorMessage);
                    onLog?.Invoke(error);
                }
            }
        }
        #endregion
    }
}

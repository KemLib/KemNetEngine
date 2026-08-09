using KemLibCore;
using System.Net.Sockets;

namespace KemNetEngine.Network
{
    public readonly struct ResultReceive : IResult
    {
        #region Preoperties
        private readonly static byte[] DATA_EMPTY = [];

        private readonly bool isSuccess;
        private readonly string errorMessage;
        private readonly SocketError errorCode;
        private readonly byte[] data;

        public readonly bool IsSuccess => isSuccess;
        public readonly string ErrorMessage => errorMessage;
        public readonly SocketError ErrorCode => errorCode;
        public readonly byte[] Data => data;
        #endregion

        #region Constructors
        private ResultReceive(bool isSuccess, string errorMessage, SocketError errorCode, byte[] data)
        {
            this.isSuccess = isSuccess;
            this.errorMessage = errorMessage;
            this.errorCode = errorCode;
            this.data = data;
        }
        #endregion

        #region Methods
        public static ResultReceive Success(byte[] data)
        {
            if (data == null)
                return new ResultReceive(true, string.Empty, SocketError.Success, DATA_EMPTY);
            else
                return new ResultReceive(true, string.Empty, SocketError.Success, data);
        }
        public static ResultReceive Fail(string errorMessage, SocketError errorCode)
        {
            if (string.IsNullOrEmpty(errorMessage))
                errorMessage = IResult.ERROR_MESSAGE_UNKNOWN;
            if (errorCode == SocketError.Success)
                errorCode = SocketError.SocketError;
            return new ResultReceive(false, errorMessage, errorCode, DATA_EMPTY);
        }
        #endregion
    }
}

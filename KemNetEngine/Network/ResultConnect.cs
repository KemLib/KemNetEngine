using KemLibCore;
using System.Net.Sockets;

namespace KemNetEngine.Network
{
    public readonly struct ResultConnect : IResult
    {
        #region Preoperties
        private readonly bool isSuccess;
        private readonly string errorMessage;
        private readonly SocketError errorCode;
        private readonly IClient client;

        public readonly bool IsSuccess => isSuccess;
        public readonly string ErrorMessage => errorMessage;
        public readonly SocketError ErrorCode => errorCode;
        public readonly IClient Client => client;
        #endregion

        #region Constructors
        private ResultConnect(bool isSuccess, string errorMessage, SocketError errorCode, IClient client)
        {
            this.isSuccess = isSuccess;
            this.errorMessage = errorMessage;
            this.errorCode = errorCode;
            this.client = client;
        }
        #endregion

        #region Methods
        public static ResultConnect Success(ClientControl client)
        {
            return new ResultConnect(true, IResult.ERROR_MESSAGE_UNKNOWN, SocketError.Success, client);
        }
        public static ResultConnect Fail(string errorMessage, SocketError errorCode)
        {
            if (string.IsNullOrEmpty(errorMessage))
                errorMessage = IResult.ERROR_MESSAGE_UNKNOWN;
            if (errorCode == SocketError.Success)
                errorCode = SocketError.SocketError;
            return new ResultConnect(false, errorMessage, errorCode, IClient.ClientNone);
        }
        #endregion
    }
}

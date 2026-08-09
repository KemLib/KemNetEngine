using KemLibCore;
using System.Net.Sockets;

namespace KemNetEngine.Network
{
    public readonly struct ResultSend : IResult
    {
        #region Preoperties
        private readonly bool isSuccess;
        private readonly string errorMessage;
        private readonly SocketError errorCode;

        public readonly bool IsSuccess => isSuccess;
        public readonly string ErrorMessage => errorMessage;
        public readonly SocketError ErrorCode => errorCode;
        #endregion

        #region Constructors
        private ResultSend(bool isSuccess, string errorMessage, SocketError errorCode)
        {
            this.isSuccess = isSuccess;
            this.errorMessage = errorMessage;
            this.errorCode = errorCode;
        }
        #endregion

        #region Methods
        public static ResultSend Success()
        {
            return new ResultSend(true, IResult.ERROR_MESSAGE_UNKNOWN, SocketError.Success);
        }
        public static ResultSend Fail(string errorMessage, SocketError errorCode)
        {
            if (string.IsNullOrEmpty(errorMessage))
                errorMessage = IResult.ERROR_MESSAGE_UNKNOWN;
            if (errorCode == SocketError.Success)
                errorCode = SocketError.SocketError;
            return new ResultSend(false, errorMessage, errorCode);
        }
        #endregion
    }
}

using System.Security.Cryptography;
using System.Text;

namespace TestNetEngine
{
    public class MessageManager
    {
        #region Properties
        public const string MESSAGE_CHAR = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";
        private const string ERROR_RECEIVE_CONVERT_TO_JSON_FAIL = "Receive convert data to json fail",
            ERROR_RECEIVE_CHECK_HASH_FAIL = "Receive check hash fail",
            ERROR_RECEIVE_CHECK_INDEX_FAIL = "Receive check index fail";

        private readonly HashAlgorithm HashSHA;
        private int sendNumber;
        private readonly Dictionary<string, int> dicReceiveIndex,
            dicReceiveNumber;
        #endregion

        #region Construction
        public MessageManager()
        {
            HashSHA = SHA256.Create();
            sendNumber = 0;
            dicReceiveIndex = [];
            dicReceiveNumber = [];
        }
        ~MessageManager()
        {
            Close();
        }
        #endregion

        #region Send
        public List<MessageData> Send_Begin(string user, int number, int length)
        {
            if (number <= 0)
                return [];
            //
            sendNumber = 0;
            //
            int maxIndex = number - 1;
            List<MessageData> listMessage = new(number);
            StringBuilder stringBuilder = new(length);
            Random randomChar = new();
            for (int i = 0; i < number; i++)
            {
                string message;
                if (length <= 0)
                {
                    message = string.Empty;
                }
                else
                {
                    for (int j = 0; j < length; j++)
                        stringBuilder.Append(MESSAGE_CHAR[randomChar.Next(0, MESSAGE_CHAR.Length)]);
                    message = stringBuilder.ToString();
                    stringBuilder.Clear();
                }
                //
                string hash = Base64_ToText(Hash_Compute(UTF8_ToByte(message)));
                MessageData messageData = new(i, maxIndex, user, message, hash);
                listMessage.Add(messageData);
            }
            return listMessage;
        }
        public bool Send_Complete(MessageData messageData)
        {
            sendNumber++;
            return (sendNumber == messageData.MaxIndex + 1);
        }
        #endregion

        #region Receive
        public bool Receive_Complete(string json, [NotNullWhen(true)] out MessageData? messageData, out bool isEnd, [NotNullWhen(false)] out string? error)
        {
            if (!MessageData.FromJson(json, out messageData))
            {
                isEnd = false;
                error = ERROR_RECEIVE_CONVERT_TO_JSON_FAIL;
                return false;
            }
            //
            MessageData currentMessage = messageData.Value;
            int receiveNumber = Receive_GetReceiveNumber(currentMessage.User);
            receiveNumber++;
            isEnd = (receiveNumber == currentMessage.MaxIndex + 1);
            if (isEnd)
                Receive_SetReceiveNumber(currentMessage.User, 0);
            else
                Receive_SetReceiveNumber(currentMessage.User, receiveNumber);
            //
            string hash = Base64_ToText(Hash_Compute(UTF8_ToByte(currentMessage.Message)));
            if (!currentMessage.Hash.Equals(hash))
            {
                error = ERROR_RECEIVE_CHECK_HASH_FAIL;
                return false;
            }
            //
            int receiveIndex = Receive_GetReceiveIndex(currentMessage.User);
            if (currentMessage.Index != receiveIndex)
            {
                if (currentMessage.Index > receiveIndex)
                {
                    if (isEnd)
                        Receive_SetReceiveIndex(currentMessage.User, 0);
                    else
                        Receive_SetReceiveIndex(currentMessage.User, currentMessage.Index + 1);
                }
                error = ERROR_RECEIVE_CHECK_INDEX_FAIL;
                return false;
            }
            else
            {
                if (isEnd)
                    Receive_SetReceiveIndex(currentMessage.User, 0);
                else
                    Receive_SetReceiveIndex(currentMessage.User, receiveIndex + 1);
            }
            //
            error = null;
            return true;
        }
        private int Receive_GetReceiveNumber(string user)
        {
            if (dicReceiveNumber.TryGetValue(user, out int value))
                return value;
            return 0;
        }
        private int Receive_GetReceiveIndex(string user)
        {
            if (dicReceiveIndex.TryGetValue(user, out int value))
                return value;
            return 0;
        }
        private void Receive_SetReceiveNumber(string user, int value)
        {
            if (!dicReceiveNumber.TryAdd(user, value))
                dicReceiveNumber[user] = value;
        }
        private void Receive_SetReceiveIndex(string user, int value)
        {
            if (!dicReceiveIndex.TryAdd(user, value))
                dicReceiveIndex[user] = value;
        }
        #endregion

        #region Close
        public void Close()
        {
            HashSHA.Dispose();
        }
        #endregion

        #region Convert
        private byte[] Hash_Compute(byte[] data)
        {
            return HashSHA.ComputeHash(data);
        }
        public static byte[] UTF8_ToByte(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }
        public static string UTF8_ToText(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }
        private static string Base64_ToText(byte[] data)
        {
            return Convert.ToBase64String(data);
        }
        #endregion
    }
}

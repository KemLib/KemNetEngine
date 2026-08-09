using System.Text.Json;

namespace TestNetEngine
{
    public readonly struct MessageData
    {
        #region Properties
        public int Index
        {
            get;
            init;
        }
        public int MaxIndex
        {
            get;
            init;
        }
        public string User
        {
            get;
            init;
        }
        public string Message
        {
            get;
            init;
        }
        public string Hash
        {
            get;
            init;
        }
        #endregion

        #region Construction
        public MessageData()
        {
            Index = -1;
            MaxIndex = -1;
            User = string.Empty;
            Message = string.Empty;
            Hash = string.Empty;
        }
        public MessageData(int index, int maxIndex, string user, string message, string hash)
        {
            Index = index;
            MaxIndex = maxIndex;
            User = user;
            Message = message;
            Hash = hash;
        }
        #endregion

        #region Json
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
        public static bool FromJson(string json, [NotNullWhen(true)] out MessageData? message)
        {
            try
            {
                message = JsonSerializer.Deserialize<MessageData>(json);
                if (message == null)
                    return false;
                return true;
            }
            catch (Exception)
            {
                message = null;
                return false;
            }
        }
        #endregion
    }
}

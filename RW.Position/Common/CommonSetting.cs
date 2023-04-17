namespace RW.Position.Common
{
    public class CommonSetting
    {
        public CommonSetting(string ip, int port, string userName, string password, string salt = "", bool x64 = false)
        {
            IP = ip;
            Port = port;
            UserName = userName;
            Password = password;
            Salt = salt;
            X64 = x64;
        }

        public string IP { get;private set; }

        public int Port { get;private set; }

        public string UserName { get; private set; }

        public string Password { get; private set; }

        public string Salt { get; private set; }

        public bool X64 { get; private set; }
    }
}

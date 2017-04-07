using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace SmokeyLib
{
    class SentryFileData
    {
        public byte[] SentryHash { get; set; }
        public string SessionId { get; set; }
    }

    class SentryFileLoader
    {
        public SentryFileData data;
        private const string SENTRYFILEPATH = "sentry.bin";

        public SentryFileLoader()
        {
            if (!ReLoad())
                throw new IOException($"Can't read {SENTRYFILEPATH} !");
        }

        private byte[] Protect(byte[] data)
        {
            return ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
        }

        private byte[] UnProtect(byte[] data)
        {
            try
            {
                return ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ReLoad()
        {
            if(!File.Exists(SENTRYFILEPATH))
            {
                data = new SentryFileData();
                return true;
            }

            try
            {
                data = JsonConvert.DeserializeObject<SentryFileData>(Encoding.UTF8.GetString(UnProtect(File.ReadAllBytes(SENTRYFILEPATH))));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Save()
        {
            File.Delete(SENTRYFILEPATH);
            File.Create(SENTRYFILEPATH).Close();
            File.WriteAllBytes(SENTRYFILEPATH, Protect(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data))));
        }
    }
}

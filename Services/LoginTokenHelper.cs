using NetCore.Common.Crypto;
using NetCore.Common.Models;
using NetCore.Common.Utils;
using Newtonsoft.Json;

namespace NetCore.Common.Services
{
    public class LoginTokenHelper
    {
        public static string GenerateToken(LoginToken loginToken, TextEncryptor signer)
        {
            AssertUtils.AssertNotNull(loginToken);
            return signer.Encrypt(JsonConvert.SerializeObject(loginToken));
        }

        public static LoginToken ParseToken(string loginToken, TextEncryptor signer)
        {
            AssertUtils.AssertNotNull(loginToken);
            return JsonConvert.DeserializeObject<LoginToken>(signer.Decrypt(loginToken));
        }
    }
}

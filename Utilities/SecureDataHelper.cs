using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PIMQAAutomation.Utilities
{
    public static class SecureDataHelper
    {
        public static string DecryptWithMachineKey(string base64EncryptedString)
        {
            if (string.IsNullOrEmpty(base64EncryptedString))
            {
                throw new ArgumentException("The encrypted string cannot be null or empty.", nameof(base64EncryptedString));
            }
            else
            {
                try {
                    var encryptedData = Convert.FromBase64String(base64EncryptedString);

                    // Decrypt the data using DPAPI with LocalMachine scope
                    var decryptedBytes = ProtectedData.Unprotect(encryptedData, optionalEntropy: null, scope: DataProtectionScope.LocalMachine);

                    // Convert decrypted bytes to string (Unicode encoding)
                    var decryptedString = Encoding.Unicode.GetString(decryptedBytes);
                    return decryptedString;
                }
                catch
                {
                   return base64EncryptedString; // Return the original string if decryption fails  
                }
            }
                
        }
    }

}

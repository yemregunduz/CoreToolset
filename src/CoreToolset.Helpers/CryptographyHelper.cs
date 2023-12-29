using CoreToolset.Helpers.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoreToolset.Helpers
{
    /// <summary>
    /// Provides encryption and decryption methods using a symmetric encryption algorithm.
    /// </summary>
    public class CryptographyHelper
    {
        private readonly CryptographyHelperOptions _options;

        /// <summary>
        /// Initializes a new instance of the CryptographyHelper class with the specified options.
        /// </summary>
        /// <param name="cryptographyHelperOptions">The options for configuring the CryptographyHelper.</param>
        public CryptographyHelper(CryptographyHelperOptions cryptographyHelperOptions)
        {
            _options = cryptographyHelperOptions ??
                throw new ArgumentNullException(nameof(cryptographyHelperOptions));
            ValidateSecurityKeySize();
        }

        /// <summary>
        /// Initializes a new instance of the CryptographyHelper class with default options.
        /// </summary>
        public CryptographyHelper()
        {
            _options = new CryptographyHelperOptions();
            ValidateSecurityKeySize();
        }

        /// <summary>
        /// Creates a new instance of the CryptographyHelper class using the default constructor.
        /// </summary>
        /// <returns>A new instance of the CryptographyHelper class.</returns>
        public static CryptographyHelper CreateInstance()
        {
            return new CryptographyHelper();
        }

        /// <summary>
        /// Creates a new instance of the CryptographyHelper class with custom options.
        /// </summary>
        /// <param name="symmetricAlgorithm">The symmetric algorithm to use for encryption and decryption.</param>
        /// <param name="securityKey">The security key used for encryption and decryption.</param>
        /// <param name="autoCompleteForSecurityKey">A flag indicating whether to auto-complete the security key if its size is not valid.</param>
        /// <returns>A new instance of the CryptographyHelper class.</returns>
        public static CryptographyHelper CreateInstance(
            SymmetricAlgorithm symmetricAlgorithm,
            string securityKey = "mysupersecretkey",
            bool autoCompleteForSecurityKey = true
            )
        {
            CryptographyHelperOptions options = new(symmetricAlgorithm, securityKey, autoCompleteForSecurityKey);
            return new CryptographyHelper(options);
        }

        /// <summary>
        /// Encrypts the specified plain text using the configured algorithm and security key.
        /// </summary>
        /// <param name="plainText">The plain text to encrypt.</param>
        /// <returns>The encrypted text.</returns>
        public string Encrypt(string plainText)
        {
            if (plainText is null)
                throw new ArgumentNullException(nameof(plainText));

            byte[] encryptedBytes;

            using (var algorithm = _options.Algorithm)
            {
                algorithm.Key = Encoding.UTF8.GetBytes(_options.SecurityKey);
                algorithm.Mode = CipherMode.CBC;
                algorithm.Padding = PaddingMode.PKCS7;

                var encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV);

                using var ms = new MemoryStream();
                ms.Write(algorithm.IV, 0, algorithm.IV.Length);

                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    cs.Write(plainBytes, 0, plainBytes.Length);
                }

                encryptedBytes = ms.ToArray();
            }

            return Convert.ToBase64String(encryptedBytes);
        }

        /// <summary>
        /// Decrypts the specified encrypted text using the configured algorithm and security key.
        /// </summary>
        /// <param name="encryptedText">The encrypted text to decrypt.</param>
        /// <returns>The decrypted text.</returns>
        public string Decrypt(string encryptedText)
        {
            if (encryptedText is null)
                throw new ArgumentNullException(nameof(encryptedText));

            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] iv = new byte[_options.Algorithm.BlockSize / 8];
            byte[] cipherBytes = new byte[encryptedBytes.Length - iv.Length];

            Array.Copy(encryptedBytes, iv, iv.Length);
            Array.Copy(encryptedBytes, iv.Length, cipherBytes, 0, cipherBytes.Length);

            string decryptedText;

            using (var algorithm = _options.Algorithm)
            {
                algorithm.Key = Encoding.UTF8.GetBytes(_options.SecurityKey);
                algorithm.IV = iv;
                algorithm.Mode = CipherMode.CBC;
                algorithm.Padding = PaddingMode.PKCS7;

                var decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV);

                using var ms = new MemoryStream(cipherBytes);
                using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using var reader = new StreamReader(cs, Encoding.UTF8);
                decryptedText = reader.ReadToEnd();
            }

            return decryptedText;
        }

        /// <summary>
        /// Verifies whether the decrypted text matches the original plain text.
        /// </summary>
        /// <param name="plainText">The original plain text.</param>
        /// <param name="encryptedText">The encrypted text to verify.</param>
        /// <returns>True if the decrypted text matches the original plain text; otherwise, false.</returns>
        public bool VerifyEncyrptedText(string plainText, string encryptedText)
        {
            if (plainText is null || encryptedText is null)
                return false;

            return Decrypt(encryptedText) == plainText;
        }

        private void ValidateSecurityKeySize()
        {
            if (!_options.IsSecurityKeySizeValid)
            {
                if (_options.AutoCompleteForSecurityKey)
                    _options.SecurityKey = CompleteSecurityKey();

                else
                    throw new ArgumentException($"Algorithm key size must be {_options.AlgorithmKeySize}", nameof(_options.Algorithm));
            }
        }

        private string CompleteSecurityKey()
        {
            int keySizeInBytes = _options.AlgorithmKeySize / 8;

            if (_options.SecurityKey.Length > keySizeInBytes)
                return _options.SecurityKey[..keySizeInBytes];
            else if (_options.SecurityKey.Length < keySizeInBytes)
                return _options.SecurityKey.PadRight(keySizeInBytes, '0');
            else
                return _options.SecurityKey;
        }
    }
}

using System.Security.Cryptography;
using System.Text;

namespace CoreToolset.Helpers.Options
{
    public class CryptographyHelperOptions
    {
        public string SecurityKey { get; set; }
        public bool AutoCompleteForSecurityKey { get; set; }
        public SymmetricAlgorithm Algorithm { get; set; }

        public int AlgorithmKeySize => Algorithm.KeySize;
        public bool IsSecurityKeySizeValid => Encoding.UTF8.GetBytes(SecurityKey).Length == AlgorithmKeySize / 8;

        /// <summary>
        /// Default constructor for CryptographyHelperOptions class.
        /// Assigns default values to all properties.
        /// </summary>
        public CryptographyHelperOptions()
        {
            SecurityKey = "mysupersecretkey";
            AutoCompleteForSecurityKey = true;
            Algorithm = Aes.Create();
        }

        /// <summary>
        /// Constructor for CryptographyHelperOptions class that takes all parameters.
        /// Sets the Algorithm, SecurityKey, and AutoCompleteForSecurityKey properties based on the specified values.
        /// </summary>
        /// <param name="symmetricAlgorithm">The symmetric encryption algorithm to use.</param>
        /// <param name="securityKey">The security key to use for encryption. Defaultis mysupersecretkey.</param>
        /// <param name="autoCompleteForSecurityKey">Specifies whether to auto-complete the security key if it does not match the desired size. Default is true.</param>
        public CryptographyHelperOptions(SymmetricAlgorithm symmetricAlgorithm, string securityKey = "mysupersecretkey", bool autoCompleteForSecurityKey = true)
        {
            Algorithm = symmetricAlgorithm ?? 
                throw new ArgumentNullException(nameof(symmetricAlgorithm));
            SecurityKey = securityKey ?? 
                throw new ArgumentNullException(nameof(securityKey));
            AutoCompleteForSecurityKey = autoCompleteForSecurityKey;
        }

    }
}

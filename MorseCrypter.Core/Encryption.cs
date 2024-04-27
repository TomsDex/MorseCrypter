using System.Security.Cryptography;

namespace MorseCrypter.Core;

public static class Encryption
{
    private const int KeyBytes = 32; // 256-bit key
    private const int SaltSize = 16; // 128-bit salt
    private const int IvSize = 16; // 128-bit IV

    /// <summary>
    /// Generate a cryptographically secure random salt value.
    /// </summary>
    /// <param name="size"></param>
    /// <returns>An array of random bytes.</returns>
    private static byte[] GenerateRandomBytes(int size)
    {
        var bytes = new byte[size];
        RandomNumberGenerator.Create().GetBytes(bytes);
        return bytes;
    }

    /// <summary>
    /// Derive the key from the password and salt.
    /// </summary>
    /// <param name="password">The user-defined password.</param>
    /// <param name="salt">A consistent salt.</param>
    /// <param name="iterations">The quantity of iterations.</param>
    /// <returns>A cryptographically-secure key.</returns>
    private static byte[] GenerateKey(string password, byte[] salt, int iterations = 10000)
    {
        using var keyGenerator = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        return keyGenerator.GetBytes(KeyBytes);
    }

    /// <summary>
    /// Encrypts a byte array using AES encryption with a specified password.
    /// The method generates a random salt and initialisation vector (IV),
    /// derives a key from the password, and performs the encryption.
    /// The salt, IV, and encrypted data are concatenated and returned.
    /// </summary>
    /// <param name="password">The password used for generating the encryption key.</param>
    /// <param name="inputBytes">The data to encrypt.</param>
    /// <returns>A byte array containing the salt, IV, and encrypted data.</returns>
    public static byte[] EncryptBytes(string password, byte[] inputBytes)
    {
        // Generate random salt and IV for each encryption to enhance security.
        var salt = GenerateRandomBytes(SaltSize);
        var iv = GenerateRandomBytes(IvSize);
        var key = GenerateKey(password, salt);

        using var aesAlg = Aes.Create();
        aesAlg.Key = key;

        // Set the IV for the AES algorithm. IVs are used to prevent patterns in encrypted data.
        aesAlg.IV = iv;

        // Create an encryptor object from the AES instance.
        using var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        // MemoryStream for holding the encrypted bytes.
        using var msEncrypt = new MemoryStream();

        // CryptoStream for performing the encryption.
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            // Write all the data to be encrypted to the stream.
            csEncrypt.Write(inputBytes, 0, inputBytes.Length);
        }

        // Extract the encrypted bytes from the MemoryStream.
        var encryptedData = msEncrypt.ToArray();

        // Prepare the final byte array which includes salt, IV, and encrypted data.
        var result = new byte[SaltSize + IvSize + encryptedData.Length];

        // Copy salt, IV, and encrypted data into the result byte array.
        Buffer.BlockCopy(salt, 0, result, 0, SaltSize);
        Buffer.BlockCopy(iv, 0, result, SaltSize, IvSize);
        Buffer.BlockCopy(encryptedData, 0, result, SaltSize + IvSize, encryptedData.Length);

        return result;
    }

    /// <summary>
    /// Decrypts a byte array that was encrypted using AES encryption.
    /// The method extracts the salt and initialisation vector (IV) from the input,
    /// derives the encryption key using the provided password, and performs the decryption.
    /// </summary>
    /// <param name="password">The password used for generating the decryption key.</param>
    /// <param name="inputBufferWithSaltAndIv">The byte array containing the salt, IV, and encrypted data.</param>
    /// <returns>The decrypted data as a byte array.</returns>
    public static byte[] DecryptBytes(string password, byte[] inputBufferWithSaltAndIv)
    {
        // Extract salt and IV from the beginning of the input buffer.
        var salt = new byte[SaltSize];
        var iv = new byte[IvSize];
        Buffer.BlockCopy(inputBufferWithSaltAndIv, 0, salt, 0, SaltSize);
        Buffer.BlockCopy(inputBufferWithSaltAndIv, SaltSize, iv, 0, IvSize);

        // The rest of the input buffer is the encrypted data.
        var encryptedData = new byte[inputBufferWithSaltAndIv.Length - SaltSize - IvSize];
        Buffer.BlockCopy(inputBufferWithSaltAndIv, SaltSize + IvSize, encryptedData, 0, encryptedData.Length);

        // Derive the key from the password and salt.
        using var aesAlg = Aes.Create();
        aesAlg.Key = GenerateKey(password, salt);
        aesAlg.IV = iv;

        // Create a decrypter object from the AES instance.
        using var decrypter = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        // MemoryStream for reading the encrypted data.
        using var msDecrypt = new MemoryStream(encryptedData);

        // CryptoStream for performing the decryption.
        using var csDecrypt = new CryptoStream(msDecrypt, decrypter, CryptoStreamMode.Read);

        // MemoryStream for holding the decrypted bytes.
        using var resultStream = new MemoryStream();

        // Read decrypted bytes into the result stream.
        csDecrypt.CopyTo(resultStream);

        return resultStream.ToArray();
    }
}
using System.Security.Cryptography;

namespace MorseCrypter.Core;

public static class Encrypter
{
    private const int KeyBytes = 32; // 256-bit key
    private const int SaltSize = 16; // 128-bit salt
    private const int IvSize = 16; // 128-bit IV

    // Generate a cryptographically secure random salt value.
    private static byte[] GenerateRandomBytes(int size)
    {
        var bytes = new byte[size];
        RandomNumberGenerator.Create().GetBytes(bytes);
        return bytes;
    }
}
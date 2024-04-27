using System.Text;

namespace MorseCrypter.Core;

/// <summary>
/// Provides logic for encoding and decoding text to and from hex.
/// </summary>
public static class Encoder
{
    /// <summary>
    /// Encodes text to hex.
    /// </summary>
    /// <param name="text">The byte array text to be encoded.</param>
    /// <returns>The encoded hex string.</returns>
    public static string Encode(byte[] text)
    {
        return Convert.ToHexString(text);
    }

    /// <summary>
    /// Decodes hex to text.
    /// </summary>
    /// <param name="hex">The string to be decoded.</param>
    /// <returns>The decoded text byte array.</returns>
    public static byte[] Decode(string hex)
    {
        return Convert.FromHexString(hex);
    }
}

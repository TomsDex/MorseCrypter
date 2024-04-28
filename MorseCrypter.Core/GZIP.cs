using System.IO.Compression;

namespace MorseCrypter.Core;

/// <summary>
/// Compresses and decompresses text.
/// </summary>
public abstract class Gzip
{
    /// <summary>
    /// Compresses a byte array.
    /// </summary>
    /// <param name="buffer">The byte array to be compressed.</param>
    /// <returns>The compressed byte array.</returns>
    public static byte[] Compress(byte[] buffer)
    {
        //Create a new memory stream.
        using var memStream = new MemoryStream();

        //Create a new GZip stream.
        using (var gZipStream = new GZipStream(memStream, CompressionMode.Compress, true))
        {
            //Write the data to the GZip stream.
            gZipStream.Write(buffer, 0, buffer.Length);
        }

        return memStream.ToArray();
    } 

    /// <summary>
    /// Decompresses a byte array.
    /// </summary>
    /// <param name="compressedData">The byte array to be decompressed.</param>
    /// <returns>The decompressed byte array.</returns>
    public static byte[] Decompress(byte[] compressedData)
    {
        //Create a new memory stream.
        using var memStream = new MemoryStream(compressedData);

        //Create a new GZip stream.
        using var gZipStream = new GZipStream(memStream, CompressionMode.Decompress);

        //Create a new memory stream to store the decompressed data.
        using var resultStream = new MemoryStream();

        //Copy the decompressed data to the result stream.
        gZipStream.CopyTo(resultStream);

        return resultStream.ToArray();
    }
}
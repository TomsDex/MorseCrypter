using System.IO.Compression;

namespace MorseCrypter.Core
{
    public class GZIP
    {
        public static byte[] Compress(byte[] buffer)
        {
            using var memStream = new MemoryStream();

            using (var gZipStream = new GZipStream(memStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            return memStream.ToArray();
        }

        public static byte[] Decompress(byte[] compressedData)
        {
            using var memStream = new MemoryStream(compressedData);

            using var gZipStream = new GZipStream(memStream, CompressionMode.Decompress);

            using var resultStream = new MemoryStream();

            gZipStream.CopyTo(resultStream);

            return resultStream.ToArray();
        }
    }
}

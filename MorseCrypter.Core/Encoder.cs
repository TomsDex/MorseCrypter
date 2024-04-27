using System.Text;

namespace MorseCrypter.Core;

public static class Encoder
{
    public static string Encode(byte[] text)
    {
        //var bytes = Encoding.UTF8.GetBytes(text);
        return Convert.ToHexString(text);

    }

    public static string Decode(byte[] hex)
    {
        //var bytes = Convert.FromHexString(hex);
        return Encoding.UTF8.GetString(hex);
    }
}
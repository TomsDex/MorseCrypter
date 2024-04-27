using System.Text;

namespace MorseCrypter.Core
{
    public class Encoder
    {
        public string Encode(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToHexString(bytes);

        }

        public string Decode(string hex)
        {
            var bytes = Convert.FromHexString(hex);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
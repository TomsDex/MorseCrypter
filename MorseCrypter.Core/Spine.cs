using System.Text;

namespace MorseCrypter.Core
{
    public static class Spine
    {
        public static string UserInputToMorseCode(string userInput, Dictionary<string, string> transSet)
        {
            //Compress the user input.
            var inputCompressed = Gzip.Compress(Encoding.UTF8.GetBytes(userInput));
            //TODO: Check and compare compressed vs uncompressed sizes

#if DEBUG
            Console.WriteLine("Compressed: " + BitConverter.ToString(inputCompressed));
#endif

            //Encrypt the compressed user input with the password.
            var inputCompressedEncrypted = Encryption.EncryptBytes("placeholderpassword", inputCompressed);

#if DEBUG
            Console.WriteLine("Compressed & encrypted: " + BitConverter.ToString(inputCompressedEncrypted));
#endif

            //Encode the encrypted and compressed user input to hex.
            var inputCompressedEncryptedEncoded = Encoder.Encode(inputCompressedEncrypted);

#if DEBUG
            Console.WriteLine("Compressed & encrypted & encoded: " + inputCompressedEncryptedEncoded);
#endif

            //Translate the compressed, encrypted, and encoded user input to Morse code.
            return Base36ToMorseTranslator.ConvertBase36ToMorse(inputCompressedEncryptedEncoded, transSet);
        }
    }
}

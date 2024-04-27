using System.Text;

namespace MorseCrypter.Core
{
    public static class Spine
    {
        /// <summary>
        /// Contains the navigation for converting user input to Morse code.
        /// </summary>
        /// <param name="textUserInput">The string to be processed and converted to Morse.</param>
        /// <param name="transSet">The user-specified translation set.</param>
        /// <returns>A Morse code string which has been compressed,
        /// encrypted with a user-defined password and encoded to hex before being translated.</returns>
        public static string UserInputStringToMorseCode(string textUserInput, Dictionary<string, string> transSet)
        {
            //Before starting, the user must enter the password to encrypt the text.
            Console.WriteLine("Please enter the password to encrypt your text with:");
            var password = InputValidation.GetUserTextInput();

            //Compress the user input.
            var inputCompressed = Gzip.Compress(Encoding.UTF8.GetBytes(textUserInput));
            //TODO: Check and compare compressed vs uncompressed sizes

#if DEBUG
            Console.WriteLine("Compressed: " + BitConverter.ToString(inputCompressed));
#endif

            //Encrypt the compressed user input with the password.
            var inputCompressedEncrypted = Encryption.EncryptBytes(password, inputCompressed);

#if DEBUG
            Console.WriteLine("Compressed & encrypted: " + BitConverter.ToString(inputCompressedEncrypted));
#endif

            //Encode the encrypted and compressed user input to hex.
            var inputCompressedEncryptedEncoded = Encoder.Encode(inputCompressedEncrypted);

#if DEBUG
            Console.WriteLine("Compressed & encrypted & encoded: " + inputCompressedEncryptedEncoded);
#endif

            //Translate the compressed, encrypted, and encoded user input to Morse code.
            return Translator.ConvertEncodedToMorse(inputCompressedEncryptedEncoded, transSet);
        }

        /// <summary>
        /// Contains the navigation for converting user input from Morse code.
        /// </summary>
        /// <param name="morseUserInput">The string to be processed and converted to text.</param>
        /// <param name="transSet">The user-specified translation set.</param>
        /// <returns>A text string which has been translated into text, decoded, decrypted with a pre-specified password and decompressed.</returns>
        public static string UserInputMorseCodeToString(string morseUserInput, Dictionary<string, string> transSet)
        {
            //Before starting, the user must enter the password to decrypt the Morse code.
            Console.WriteLine("Please enter the password to decrypt the Morse code:");
            var password = InputValidation.GetUserTextInput();

            //Translate the Morse code to hex.
            var outputTranslated = Translator.ConvertMorseToEncoded(morseUserInput, transSet);
            
#if DEBUG
            Console.WriteLine("Translated to hex: " + outputTranslated);
#endif

            //Decode the hex to a byte array.
            var outputTranslatedDecoded = Encoder.Decode(outputTranslated);

#if DEBUG
            Console.WriteLine("Translated to hex and decoded: " + outputTranslatedDecoded);
#endif

            //Decrypt the decoded text with the password.
            var outputTranslatedDecodedDecrypted = Encryption.DecryptBytes(password, outputTranslatedDecoded);

#if DEBUG
            Console.WriteLine("Translated to hex, decoded and decrypted: " + outputTranslatedDecodedDecrypted);
#endif
            //Decompress the decrypted text.
            return Encoding.UTF8.GetString(Gzip.Decompress(outputTranslatedDecodedDecrypted));
        }
    }
}

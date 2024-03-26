namespace MorseCrypter
{
    /// <summary>
    /// Provides a set of functions to read the translation sets from provided files.
    /// </summary>
    public class FileReader
    {
        public string Directory { get; set; }
        public string FileName { get; set; }
        public string[] Text { get; set; }
        public Dictionary<char, string> CharacterSet { get; set; }

        public FileReader()
        {
            Directory = GetDirectory();
            FileName = GetFileName();
            Text = ReadFile(FileName);
            CharacterSet = ReturnCharacterSet();
        }

        /// <summary>
        /// Initialise directory of translation sets.
        /// </summary>
        /// <returns>The directory in which the translation sets are saved.</returns>
        public string GetDirectory()
        {
            return "..\\..\\..\\..\\resources\\";
        }

        /// <summary>
        /// Get the name of the file to be read.
        /// </summary>
        /// <returns>The file name.</returns>
        public string GetFileName()
        {
            return "international.txt";
        }

        /// <summary>
        /// Reads every line in the provided file.
        /// </summary>
        /// <param name="FileName">The name of the file to be read.</param>
        /// <returns>All lines within the specified file.</returns>
        public string[] ReadFile(string FileName)
        {
            return File.ReadAllLines(Directory + FileName);
        }

        public Dictionary<char, string> ReturnCharacterSet()
        {
            //TODO: Finish this

            //Initialise a dictionary to store the morse code translation set.
            Dictionary<char, string> MorseCodeTranslationSet = new Dictionary<char, string>();
            return null;
        }
        
        public void WriteToConsole()
        {
            foreach (var s in Text)
            {
                Console.WriteLine(s);
            }
        }

    }
}

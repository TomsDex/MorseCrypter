namespace MorseCrypter
{
    /// <summary>
    /// Provides a set of functions to read the translation sets from provided files.
    /// </summary>
    public class FileReader
    {
        public string LocalFileDirectory { get; set; }
        public List<string>? TranslationSets { get; set; }
        public List<Dictionary<char, string>> CharacterSets { get; set; }

        public FileReader()
        {
            LocalFileDirectory = GetUserInputDirectory();
            TranslationSets = GetTranslationSets(LocalFileDirectory);
            CharacterSets = GetCharacterSets();
        }

        /// <summary>
        /// Gets user input for the directory of translation sets.
        /// </summary>
        /// <returns>The user-input directory in which the translation sets are saved.</returns>
        public string GetUserInputDirectory()
        {
            while (true)
            {
                Console.WriteLine("Please enter the directory of your translation sets (not the files themselves):");
                string? input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input)) return input;
                Console.WriteLine("Input cannot be empty!");
            }
        }

        public bool ValidateUserInputDirectory()
        {
            //TODO: Validate the directory here
            return true;
        }

        public List<string>? GetTranslationSets(string inputDirectory)
        {
            //Selects every file which contains the line "# Translation Set #".
            var txtFiles = Directory.EnumerateFiles(inputDirectory, "*.txt")
                           .Where(txtFile => File.ReadLines(txtFile).Any(line => line.Equals("# Translation Set #")))
                           .ToList();

            //Return null if no files contain the specified line
            if (!txtFiles.Any())
            {
                return null;
            }

            List<string> foundFiles = [.. txtFiles];
            return foundFiles;
        }

        /// <summary>
        /// Reads every line in the provided file.
        /// </summary>
        /// <param name="FileName">The name of the file to be read.</param>
        /// <returns>All lines within the specified file.</returns>
        public string[] ReadFile(string FileName)
        {
            return File.ReadAllLines(LocalFileDirectory + FileName);
        }

        public List<Dictionary<char, string>> GetCharacterSets()
        {
            return null;
        }
    }
}

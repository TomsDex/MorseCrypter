using System.Runtime.Versioning;

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
            TranslationSets = GetListOfTranslationSets(LocalFileDirectory);
            CharacterSets = GetTranslationSets(TranslationSets);
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

        public List<string>? GetListOfTranslationSets(string inputDirectory)
        {
            //Selects every file which contains the line "# Translation Set #".
            var txtFiles = Directory.EnumerateFiles(inputDirectory, "*.txt")
                           .Where(txtFile => File.ReadLines(txtFile).Any(line => line.Equals("# Translation Set #")))
                           .ToList();

            //Return null if no files contain the specified line
            if (txtFiles.Count == 0)
            {
                return null;
            }

            return new List<string>(txtFiles);
        }

        /// <summary>
        /// Converts each file into a translation set.
        /// </summary>
        /// <param name="files">The files which are tagged as translation sets.</param>
        /// <returns>A list of a dictionary of the translation sets.</returns>
        public List<Dictionary<char, string>> GetTranslationSets(List<string> files)
        {
            List<Dictionary<char, string>> translationSets = [];
            foreach (var file in files)
            {
                Dictionary<char, string> dict = [];

                var lines = File.ReadAllLines(file);
                
                foreach (var line in lines)
                {
                    //Skip lines starting with #.
                    if (!line.StartsWith("#"))
                    {
                        //Store the Base36 character.
                        char base36Key = line[0];
                        //Store the morse translation.
                        //The morse translation is expected to start on the third character of each line.
                        string morseValue = line[2..];

                        //Add the translation set to the dictionary.
                        dict[base36Key] = morseValue;
                    }
                }
                //Add the dictionary to the dictionary set list.
                translationSets.Add(dict);
            }
            return translationSets;
        }
    }
}

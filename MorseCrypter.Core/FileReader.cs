namespace MorseCrypter.Core;

/// <summary>
/// Provides a set of functions to read the translation sets from provided files.
/// </summary>
public static class FileReader
{
    /// <summary>
    /// Returns a list of every txt file containing the line "# Translation Set #".
    /// </summary>
    /// <param name="inputDirectory">The validated user-specified directory of translation sets.</param>
    /// <returns>A list of the names of the files.</returns>
    public static List<string> GetListOfTranslationSets(string inputDirectory)
    {
        
        while (true)
        {
            //Select every file which contains the line "# Translation Set #".
            var txtFiles = Directory.EnumerateFiles(inputDirectory, "*.txt")
                .Where(txtFile => File.ReadLines(txtFile).Any(line => line.Equals("# Translation Set #")))
                .ToList();

            //If no files contain the specified line.
            if (txtFiles.Count != 0) return txtFiles;
            Console.WriteLine("No translation files were found in that directory!\nPress 1 to try again."); //TODO: Fix

            //Stores the user key input.
            var keyInfo = Console.ReadKey();
            var input = keyInfo.KeyChar;

            if (input == 1)
            {
                Console.Clear();
                //Retrieve the new user directory.
                InputValidation.GetUserDirectoryInput("your local translation files");
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
    

    /// <summary>
    /// Converts each file into a translation set.
    /// </summary>
    /// <param name="files">The files which are tagged as translation sets.</param>
    /// <returns>A list of a dictionary of the translation sets.</returns>
    public static List<Dictionary<string, string>>? GetTranslationSets(List<string> files)
    {
        List<Dictionary<string, string>> translationSets = [];
        foreach (var file in files)
        {
            Dictionary<string, string> dict = [];

            if (!FileIsEmpty(file))
            {
                //Store the name of the translation set.
                dict["TranslationSetName"] = Path.GetFileNameWithoutExtension(file);

                var lines = File.ReadAllLines(file);

                foreach (var line in lines)
                {
                    //Skip empty lines or lines starting with #.
                    if (!line.StartsWith('#') && !string.IsNullOrEmpty(line))
                    {
                        //Store the Base36 character.
                        var base36Key = line[0].ToString();

                        //Store the Morse translation.
                        //The Morse translation is expected to start on the third character of each line.
                        var morseValue = line[2..];

                        //Add the translation set to the dictionary.
                        dict[base36Key] = morseValue;
                    }
                }

                //Add the dictionary to the dictionary set list.
                translationSets.Add(dict);
            }
            else return null;
        }
        return translationSets;
    }

    /// <summary>
    /// Prints out the list of available translation sets to the console.
    /// </summary>
    public static void PrintTranslationSetsToConsole(List<string> files)
    {
        Console.Clear();
        Console.WriteLine($"There are {files.Count} translation sets available to translate to.\nPlease select which one you would like to use:");
        for (var i = 0; i < files.Count; i++)
        {
            //Prints out the name of each translation set.
            Console.WriteLine("{0}. {1}", i, Path.GetFileNameWithoutExtension(files[i]).ToUpper());
        }
    }

    /// <summary>
    /// Checks if the file is empty.
    /// </summary>
    /// <param name="file">The file to be checked</param>
    /// <returns>True if the length of the file is 0.</returns>
    public static bool FileIsEmpty(string file)
    {
        return new FileInfo(file).Length == 0;
    }
}
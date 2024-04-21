namespace MorseCrypter
{
    public class Program
    {
        private static void Main()
        {
            var fileReader = new FileReader.FileReader();
            fileReader.Initialise();
            fileReader.WriteToConsole();
        }
    }
}
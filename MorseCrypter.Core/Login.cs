using System.Security.Cryptography;

namespace MorseCrypter.Core;

/// <summary>
/// Handles logic for logging in and registering users.
/// </summary>
public abstract class Login
{
    /// <summary>
    /// The main menu for the login system.
    /// </summary>
    /// <returns>True if the user successfully logs in.</returns>
    public static bool StartLogin()
    {
        Console.WriteLine("Welcome to the Login Page.");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Exit");
            var input = InputValidation.GetUserNumberInput();

            switch (input)
            {
                //Attempts to log the user in.
                case 1:
                    return LoginUser();

                //Registers a new user.
                case 2:
                    RegisterUser();
                    break;

                //Exits the program.
                case 3:
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid input! Please enter 1-3.");
                    break;
            }

            //If the user is not logged in, prompt the user to try again.
            return false;
    }

    /// <summary>
    /// Logs the user in.
    /// </summary>
    /// <returns>True if the user enters correct credentials.</returns>
    private static bool LoginUser()
    {
        Console.WriteLine("Please enter your username:");
        var username = InputValidation.GetUserTextInput();

        //Check if the user file exists.
        var filePath = username + ".txt";
        if (!File.Exists(filePath))
        {
            Console.WriteLine("User not found.");
            return false;
        }

        //Check if the user file is corrupted.
        var lines = File.ReadAllLines(filePath);
        if (lines.Length != 3)
        {
            Console.WriteLine("User file is corrupted.");
            return false;
        }

        Console.WriteLine("Please enter your password:");
        var password = InputValidation.GetUserTextInput();

        //Hash the password and compare it to the stored hash and salt.
        var salt = Convert.FromHexString(lines[1].Substring(6));
        var hash = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
        var hashedPassword = Convert.ToHexString(hash.GetBytes(64));

        //If the password is correct, log the user in.
        if (hashedPassword == lines[2].Substring(10))
        {
            Console.WriteLine("Login successful.");
            return true;
        }

        //If the password is incorrect, deny access.
        Console.WriteLine("Login failed.");
        return false;
    }

    /// <summary>
    /// Registers a new user by creating a new text file with their credentials.
    /// </summary>
    private static void RegisterUser()
    {
        User user = new();
        //Check if a file with this username already exists.
        var filePath = user.Username + ".txt";
        if (File.Exists(filePath))
        {
            Console.WriteLine("Username already taken. Please choose a different username.");
            return;
        }
        //Otherwise, write the user's credentials to a new file.
        user.WriteToFile();
        Console.WriteLine("User registered!");
    }
}
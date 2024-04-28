using System.Security.Cryptography;

namespace MorseCrypter.Core;

/// <summary>
/// Handles logic for logging in and registering users.
/// </summary>
public class User
{
    /// <summary>
    /// The user-defined username.
    /// </summary>
    public string Username { get; }

    /// <summary>
    /// A randomly generated salt.
    /// </summary>
    private string Salt { get; }

    /// <summary>
    /// The hashed password.
    /// </summary>
    private string Password { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public User()
    {
        Username = GetUsername();
        Password = GetAndHashPassword(out var salt);
        Salt = Convert.ToHexString(salt);
    }

    /// <summary>
    /// Prompts the user to enter their username.
    /// </summary>
    /// <returns>The username.</returns>
    private static string GetUsername()
    {
        Console.WriteLine("Please enter your username:");
        return InputValidation.GetUserTextInput();
    }

    /// <summary>
    /// Prompts the user to enter their password and generates a salt.
    /// </summary>
    /// <param name="salt">The randomly generated salt.</param>
    /// <returns>The password, stored as a hex string.</returns>
    private static string GetAndHashPassword(out byte[] salt)
    {
        Console.WriteLine("Please enter your password:");
        var password = InputValidation.GetUserTextInput();

        salt = new byte[32];
        using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);

        var hash = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
        return Convert.ToHexString(hash.GetBytes(64));
    }

    /// <summary>
    /// Writes the user's credentials to a text file.
    /// </summary>
    public void WriteToFile()
    {
        var filePath = Username + ".txt";
        using var writer = new StreamWriter(filePath);
        writer.WriteLine($"Username: {Username}");
        writer.WriteLine($"Salt: {Salt}");
        writer.WriteLine($"Password: {Password}");
    }
}

using System;
using System.Security.Cryptography;
using System.Text;
using TextCopy;

public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
        if (password == null)
        {
            // nameof演算子を使って引数の名前を例外に渡す
            throw new ArgumentNullException(nameof(password));
        }
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public static string InputChecker(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Password cannot be null, empty, or whitespace.", nameof(password));
        }

        if (password.Length > 256) // 任意の長さ制限を設定する場合
        {
            throw new ArgumentException("Password is too long.", nameof(password));
        }

        return password;
    }

}

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter password to hash: ");
        string defaultPassword = Console.ReadLine();
        string hashedPassword = PasswordHasher.HashPassword(defaultPassword);
        Console.WriteLine($"Hashed password: {hashedPassword}");
        //クリップボードにコピー
        ClipboardService.SetText(hashedPassword);
    }
}

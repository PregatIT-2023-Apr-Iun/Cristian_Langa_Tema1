using System;
using System.Text.RegularExpressions;

namespace Tema1
{
    class Program
    {
        // this function is meant to replace all the characters we would normally see when typing a password with "*" characters
        // whatever we type will be added in the "password" variable
        // also if we press backspace, the "*" character will be replaced with a blank space and we will delete the last character in "password" 
        static string type_password()
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);
            return password;
        }
        static void Main(string[] args)
        {
            int error_counter = 0;
            string email;
            string password;
            // we will use the Enter_Data method to insert the data which the user will then use to "log in"
            UserData data = new UserData();
            data.Enter_Data();
            // in this loop we enter the email and password and if at least one of these 2 don't match the data we inserted earlier, then the user has to try again
            // the user can try up to 3 times to enter the data correctly, once he runs out of tries, or he succesfully enters the data correctly, the loop stops 
            while (true)
            {
                if (error_counter == 3)
                {
                    break;
                }
                Console.Write("\nEnter your email: ");
                email = Console.ReadLine();
                Console.Write("Enter your password: ");
                password = type_password();
                if (!password.Equals(data.Password) || !email.Equals(data.Email))
                {
                    error_counter++;
                    Console.Write($"\nWrong password or email address, please try again, you have {3 - error_counter} number of tries left");
                }
                else {
                    Console.Write("\nPassword and email address succesfully entered!");
                    break;
                }
            }
        }

        // this class is used to store the email and password
        public class UserData
        {
            private string? email;
            private string? password;

            public string Email
            {
                get { return email; }
                set { email = value; }
            }
            public string Password
            {
                get { return password; }
                set { password = value; }
            }
            
            // this method is used to enter and confirm the email address and password.
            public void Enter_Data()
            {   // we check if the email address that was written adheres to the correct email address format, if it does not the user has to try entering the address again
                while (true)
                {
                    Console.Write("Type in your email address: ");
                    Email = Console.ReadLine();
                    if (Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid email address, please try again");
                    }
                }
                // the user types in the password, then has to type it again to confirm it, if the user does not type it twice correctly he has to type the password and confirm it all over again
                while (true)
                {
                    Console.Write("Type in your password: ");
                    Password = type_password();
                    Console.Write("\nConfirm your password: ");
                    string confirmed_password = "";
                    confirmed_password = type_password();
                    if (confirmed_password.Equals(Password))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nPassword confirm does not match what you typed in originally, please try again");
                    }
                }
            }
        }
    }
}

using System;
using Users.Common.Models;
using Users.Common.Services;

namespace Users.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            // Skriv ett program som:
            // 1. Loggar in en användare.
            // 2. Om användaren inte existerar, registrerar användaren via inmatning och 
            //    låter användaren ange in ett lösenord,
            //    alt. genererar ett lösenord.
            // 3. Direkt efter ny användare registrerats, skall användaren kunna logga in (inloggning får ej ske automatiskt).

            /*
             (4. Om lösenord lagras i fil får detta ej ske i klartext.)

             Tilläggsvis är detta ett grovt förenklat sätt att jobba med användare.
             Det är inte representativt för verkligheten, men innehåller delar som är jämförbara.

             I verkligheten gäller:
             Lösenord måste krypteras och får ej lagras i fritextform.
             Lösenord får inte visas på skärm.
             Lösenord har vanligtvis löjliga/svåra lösenordsregelkrav pga brute force-algoritmer.
             Normalt skickar man en epost med länk till användaren som registrerats.
             Användaren får efter klick på verifieringslänk ange ett lösenord i en maskad inmatning.
            */

            IUserService _userService = new UserService();
            IUser _currentUser = null;
            bool _run = true;

            while (_run)
            {
                while (_currentUser == null)
                {
                    Console.WriteLine("----------------------\nLogin:");
                    Console.Write("Your email? ");
                    string email = Console.ReadLine();

                    if (email.Length <= 1 || !email.Contains('@'))
                        continue;

                    if (_userService.Get(email) == null)
                    {
                        Console.WriteLine("------------------------");
                        Console.WriteLine("User not found!");
                        User newUser = RegisterForm(email);
                        string newPassword = PasswordForm();

                        _userService.SetPassword(newUser.Id, newPassword);
                        IServiceResponse registerResponse = _userService.Register(newUser);

                        if (registerResponse.Success)
                        {
                            Console.WriteLine(registerResponse.Message);
                            Console.WriteLine();
                        }

                    }
                    else
                    {
                        Console.Write("Your password? ");
                        string password = Console.ReadLine();
                        IServiceResponse loginResponse = _userService.Login(email, password);

                        if (loginResponse.Success)
                        {
                            _currentUser = _userService.Get(email);
                        }
                        Console.WriteLine(loginResponse.Message);
                    }
                }

                Console.WriteLine("------------------------");
                Console.WriteLine($"Welcome {_currentUser.Name}!");

                while (_currentUser != null)
                {

                    Console.WriteLine("1: Log out");
                    Console.WriteLine("2: Show user info");

                    if (int.TryParse(Console.ReadLine(), out int input))
                    {
                        if (input == 1)
                        {
                            Console.WriteLine("Bye!");
                            _currentUser = null;
                        }

                        if (input == 2)
                            ShowUserInfo(_currentUser);
                    }
                }
            }
        }

        static User RegisterForm(string email)
        {
            Console.WriteLine($"Registering new user {email}");

            Console.Write("What's your name? ");
            string name = Console.ReadLine();

            Console.Write("What's your phone number? ");
            string phone = Console.ReadLine();

            Console.Write("Do you want to subscribe to our newsletter? (y/n) ");
            bool subscribe = Console.ReadLine() == "y" ? true : false;

            User newUser = new User()
            {
                Email = email,
                Id = Guid.NewGuid(),
                Name = name,
                Phone = phone,
                SubscribeToNewsletter = subscribe
            };
            return newUser;
        }

        static string PasswordForm()
        {
            string password = "";
            PasswordService passwordService = new PasswordService();
            IServiceResponse passwordValidation;
            do
            {
                Console.WriteLine("Set your password: (Must contain at least 8 characters. At least 3 capital letters, 3 numbers & 2 special characters)");
                password = Console.ReadLine();

                passwordValidation = passwordService.ValidatePassword(password, 8, 3, 3, 2);
                Console.WriteLine(passwordValidation.Success ? "Password set!" : passwordValidation.Message);
                Console.WriteLine();

            } while (!passwordValidation.Success);

            return password;
        }

        static void ShowUserInfo(IUser currentUser)
        {
            Console.WriteLine("\nYour user info:");
            Console.WriteLine("---------------------------");
            Console.WriteLine($"Email: {currentUser.Email}");
            Console.WriteLine($"Name: {currentUser.Name}");
            Console.WriteLine($"Phone: {currentUser.Phone}");
            if (currentUser.SubscribeToNewsletter)
                Console.WriteLine("You're a subscriber of our newsletter!");
            Console.WriteLine("------------------------");
            Console.WriteLine();
        }
    }
}

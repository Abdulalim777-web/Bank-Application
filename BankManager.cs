namespace Banking_Application
{
    public class BankManager : IBankManager
    {
        private readonly ILoginManager _loginManager;

        public BankManager()
        {
            _loginManager = new LoginManager();
        }

        public void Login()
        {
            try
            {
                Console.Write("Enter Account Username: ");
                string usernamecheck = Console.ReadLine()!;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Enter Password: ");
                string passwordcheck = ReadPassword();
                Console.ResetColor();
                if (_loginManager.VerifyPassword(usernamecheck, passwordcheck))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Login Successful.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Username or Password.");
                    Console.ResetColor();
                    return;
                }

                bool running = true;
                while (running)
                {
                    LoginMenu();
                    string choice = Console.ReadLine()!;

                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter Account username: ");
                            // why are you converting to upper it is giving issues because of case sensitivity
                           // and also your code does not check if a user wih that username already exist 
                            string username = Console.ReadLine()!;
                            Console.Write("Enter Password: ");
                            string password = ReadPassword();
                            if (_loginManager.VerifyPassword(username, password))
                            {
                                Console.Write("Enter Amount to Add: ");
                                decimal amount = decimal.Parse(Console.ReadLine()!);
                                _loginManager.AddMoney(username, password, amount);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid Username or Password.");
                                Console.ResetColor();
                            }
                            continue;

                        case "2":
                            Console.Write("Enter From Account user: ");
                            string fromAccountuser = Console.ReadLine()!;
                            Console.Write("Enter Password: ");
                            string fromPassword = ReadPassword();
                            if (_loginManager.VerifyPassword(fromAccountuser, fromPassword))
                            {
                                Console.Write("Enter To Account user: ");
                                string toAccountUser = Console.ReadLine()!;
                                Console.Write("Enter Amount to Transfer: ");
                                decimal transferAmount = decimal.Parse(Console.ReadLine()!);
                                _loginManager.TransferMoney(fromAccountuser, fromPassword, toAccountUser, transferAmount);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid Username or Password.");
                                Console.ResetColor();
                            }
                            continue;

                        case "3":
                            Console.Write("Enter Account User: ");
                            string withdrawAccountuser = Console.ReadLine()!;
                            Console.Write("Enter Password: ");
                            string withdrawPassword = ReadPassword();
                            if (_loginManager.VerifyPassword(withdrawAccountuser, withdrawPassword))
                            {
                                Console.Write("Enter Amount to Withdraw: ");
                                decimal withdrawAmount = decimal.Parse(Console.ReadLine()!);
                                _loginManager.WithdrawMoney(withdrawAccountuser, withdrawPassword, withdrawAmount);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid Username or Password.");
                                Console.ResetColor();
                            }
                            continue;

                        case "4":
                            Console.Write("Enter Account Username: ");
                            string checkBalanceAccountUser = Console.ReadLine()!;
                            Console.Write("Enter Password: ");
                            string checkBalancePassword = ReadPassword();
                            if (_loginManager.VerifyPassword(checkBalanceAccountUser, checkBalancePassword))
                            {
                                _loginManager.CheckBalance(checkBalanceAccountUser, checkBalancePassword);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid Username or Password.");
                                Console.ResetColor();
                            }
                            continue;

                        case "5":
                            Console.Write("Enter Account Username: ");
                            string receiptAccountUser = Console.ReadLine()!;
                            Console.Write("Enter password:");
                            string recieptpassword = ReadPassword();
                            if (_loginManager.VerifyPassword(receiptAccountUser, recieptpassword))
                            {
                                _loginManager.Receipt(receiptAccountUser, recieptpassword);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid Username or Password.");
                                Console.ResetColor();
                            }
                            continue;

                        case "6":
                            Console.Write("Enter Account Username: ");
                            string resetAccountUser = Console.ReadLine()!;
                            Console.Write("Enter old password:");
                            string formalpassword = ReadPassword();
                            Console.Write("Enter New Password: ");
                            string newPassword = ReadPassword();
                            if (_loginManager.VerifyPassword(resetAccountUser, formalpassword))
                            {
                                _loginManager.ResetPassword(resetAccountUser, formalpassword, newPassword);
                                continue;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid Username or Password.");
                                Console.ResetColor();
                            }
                            continue;

                        case "7":
                            Console.Write("Enter Account Username: ");
                            string transactionsAccountUser = Console.ReadLine()!;
                            Console.Write("Enter Password: ");
                            string transactionpassword = ReadPassword();
                            if (_loginManager.VerifyPassword(transactionsAccountUser, transactionpassword))
                            {
                                _loginManager.Transactions(transactionsAccountUser, transactionpassword);
                                continue;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid Username or Password.");
                                Console.ResetColor();
                            }
                            continue;

                        case "8":
                            Console.Write("Enter Account Username: ");
                            string userDetailsAccountUser = Console.ReadLine()!;
                            _loginManager.UserDetails(userDetailsAccountUser);
                            continue;

                        case "9":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Exiting Application......");
                            Console.ResetColor();
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid Number. Please try again.");
                            Console.ResetColor();
                            continue;
                    }
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Invalid Format: {e.Message}");
            }
        }

        public static void LoginMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("=======Welcome To Login Section=======");
            Console.WriteLine("Enter 1 to Add Money to your Account");
            Console.WriteLine("Enter 2 to Transfer Money From your Account");
            Console.WriteLine("Enter 3 to Withdraw from your Account");
            Console.WriteLine("Enter 4 to Check your Account Balance");
            Console.WriteLine("Enter 5 to Receipts in your Account");
            Console.WriteLine("Enter 6 to Reset your Account Password");
            Console.WriteLine("Enter 7 to Show all Transactions");
            Console.WriteLine("Enter 8 to View your Account Details");
            Console.WriteLine("Enter 9 to Exit");
            Console.ResetColor();
        }

        public void SignIn()
        {
            Console.Write("Enter Account Username: ");
            string accountUser = Console.ReadLine()!;
            Console.Write("Enter Name: ");
            string name = Console.ReadLine()!;
            Console.Write("Enter Password: ");
            string password = Console.ReadLine()!;
            if (password.Length != 4)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Password must not be LESS than or MORE than 4 characters.");
                Console.ResetColor();
                return;
            }

            var newAccount = new Account
            {
                Username = accountUser,
                Name = name,
                Password = password,
                Balance = 0,
                Transactions = new List<string>()
            };

            _loginManager.AddAccount(newAccount);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Account created successfully.");
            Console.ResetColor();
        }

        public string ReadPassword()
        {
            string password = "";
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        Console.Write("\b \b");
                        password = password.Substring(0, password.Length - 1);
                    }
                }
                else
                {
                    Console.Write("*");
                    password += keyInfo.KeyChar;
                }
            }
            return password;
        }
    }
}



namespace Banking_Application
{
    public class LoginManager : ILoginManager
    {
        private readonly List<Account> Accounts;

        public LoginManager()
        {
            Accounts = new List<Account>();
        }

        public void AddMoney(string accountUser, string password, decimal amount)
        {
            var account = Accounts.FirstOrDefault(a => a.Username == accountUser);
            if (account == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Account not found.");
                Console.ResetColor();
                return;
            }

            if (account.Password != password)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid password.");
                Console.ResetColor();
                return;
            }

            if (amount < 50 || amount > 100000)
            {
                DateTime addfailTime = DateTime.Now;
                account.Transactions!.Add($"{addfailTime}: Failed to add {amount} due to not in range of money addable to account");
                Console.WriteLine("You can only add money from 50 - 100,000");
                return;
            }

            account.Balance += amount;
            Console.ForegroundColor = ConsoleColor.Green;
            DateTime addTime = DateTime.Now;
            account.Transactions!.Add($"Added {amount} at {addTime}");
            Console.WriteLine($"Added {amount} to account {accountUser}. New balance: {account.Balance}");
            Console.ResetColor();
        }


        public void TransferMoney(string fromAccountUser,string password, string toAccountUser, decimal amount)
        {
            var fromAccount = Accounts.FirstOrDefault(a => a.Username == fromAccountUser);
            var toAccount = Accounts.FirstOrDefault(a => a.Username == toAccountUser);
            var passcode  = Accounts.FirstOrDefault(a => a.Username == password);

            if (fromAccount != null && toAccount != null && passcode != null)
            {
                if (fromAccount.Balance >= amount)
                {
                    fromAccount.Balance -= amount;
                    toAccount.Balance += amount;
                    DateTime transferTime = DateTime.Now;
                    toAccount.Transactions ??= new List<string>();
                    fromAccount.Transactions ??= new List<string>();
                    fromAccount.Transactions!.Add($"{transferTime}:Transferred {amount} to {toAccountUser}:status:successful"); 
                    toAccount.Transactions!.Add($"{transferTime}:Received {amount} from {fromAccountUser}:status:successful");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Transferred {amount} from account User {fromAccountUser} to account User {toAccountUser}.");
                    Console.ResetColor();
                    
                }
                else
                {
                    DateTime transferTime = DateTime.Now;
                    toAccount.Transactions ??= new List<string>();
                    fromAccount.Transactions ??= new List<string>();
                    
                    fromAccount.Transactions!.Add($"{transferTime}:Failed to transfer {amount} to {toAccountUser} due to insufficient balance.");
                    toAccount.Transactions!.Add($"{transferTime}:Failed to receive {amount} from {fromAccountUser} due to {fromAccountUser}insufficient balance.");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Insufficient balance.");
                    Console.ResetColor();
                }
            }
            else
            {
                DateTime transferTime = DateTime.Now;
                fromAccount!.Transactions!.Add($"{transferTime}:Failed to transfer {amount} to {toAccountUser} due to account not found.");
                if (toAccount != null)
                {
                    toAccount!.Transactions!.Add($"{transferTime}:Failed to receive {amount} from {fromAccountUser} due to account not found.");
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("One or both accounts not found.");
                Console.ResetColor();
            }
        }

        public void WithdrawMoney(string accountUser,string withdrawPassword, decimal amount)
        {
            var account = Accounts.FirstOrDefault(a => a.Username == accountUser);
            var passcode  = Accounts.FirstOrDefault(a => a.Username == withdrawPassword);
            if (account != null && passcode != null)
            {
                if (account.Balance >= amount)
                {
                    account.Balance -= amount;
                    DateTime withdrawTime = DateTime.Now;
                    account.Transactions ??= new List<string>();
    
                    account.Transactions!.Add($"{withdrawTime}:Withdrew {amount}:status:successful");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Withdrew {amount} from account {accountUser}. New balance: {account.Balance}");
                    Console.ResetColor();
                }
                else
                {
                    DateTime withdrawTime = DateTime.Now;
                    account.Transactions ??= new List<string>();
                    account.Transactions!.Add($"{withdrawTime}:Failed to withdraw {amount} due to insufficient balance.");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Insufficient balance.");
                    Console.ResetColor();
                }
            }
            else
            {
                DateTime withdrawTime = DateTime.Now;
                account!.Transactions = new List<string>();
                account!.Transactions!.Add($"{withdrawTime}:Failed to withdraw {amount} due to account not found.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Account not found.");
            }
        }

        public void CheckBalance(string accountUser ,string password)
        {
            var account = Accounts.FirstOrDefault(a => a.Username == accountUser);
            // just so you know this is bad i fixed it the ux is bad i keep entering password and username
            var passcode  = Accounts.FirstOrDefault(a => a.Password == password);
            if (account != null && passcode != null)
            {
                DateTime checkTime = DateTime.Now;
                account.Transactions ??= new List<string>();
                account.Transactions!.Add($"{checkTime}:Checked balance:status:successful");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Account {accountUser} balance: {account.Balance}");
                Console.ResetColor();
            }
            else
            {
                DateTime checkTime = DateTime.Now;
                account!.Transactions ??= new List<string>();
                account!.Transactions!.Add($"{checkTime}:Failed to check balance due to account not found.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Account not found.");
                Console.ResetColor();
            }
        }

        public void Receipt(string accountUser, string recieptpassword)
        {
            var account = Accounts.FirstOrDefault(a => a.Username == accountUser);
            var passcode  = Accounts.FirstOrDefault(a => a.Username == recieptpassword);
            if (account != null && passcode != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Receipt for account {accountUser}");
                Console.WriteLine($"User name: {account.Username}");
                Console.WriteLine($"Password: {account.Password}");
                Console.WriteLine($"Balance: {account.Balance}");
                Console.ResetColor();
                
            }
            else
            {
                DateTime receiptTime = DateTime.Now;
                account!.Transactions ??= new List<string>();

                account!.Transactions!.Add($"{receiptTime}:Failed to generate receipt due to account not found.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Account not found.");
                Console.ResetColor();
            }
        }

        public void ResetPassword(string accountUser,string formalpassword, string newPassword)
        {
            var account = Accounts.FirstOrDefault(a => a.Username == accountUser);
            var passcode  = Accounts.FirstOrDefault(a => a.Username == formalpassword);
            var password  = Accounts.FirstOrDefault(a => a.Username == newPassword);
            if (account != null && passcode != password)
            {
                account.Password = newPassword;
                DateTime resetTime = DateTime.Now;
                account!.Transactions ??= new List<string>();

                account.Transactions!.Add($"{resetTime}:Reset password:formal Password:{formalpassword}:new Password:{newPassword} as at {resetTime}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Password for account {accountUser} has been reset.");
                Console.WriteLine("your new password is: " + newPassword);
                Console.ResetColor();
            }
            else
            {
                DateTime resetTime = DateTime.Now;
                account!.Transactions ??= new List<string>();

                account!.Transactions!.Add($"{resetTime}:Failed to reset password due to account not found.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Account not found.");
                Console.ResetColor();
            }
        }

        public void Transactions(string accountUser, string transactionpassword)
        {
            var account = Accounts.FirstOrDefault(a => a.Username == accountUser);
            var passcode  = Accounts.FirstOrDefault(a => a.Password == transactionpassword);
            if (account != null && passcode != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Transactions for account {accountUser}:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (var transaction in account.Transactions!)
                {
                    Console.WriteLine(transaction);
                }
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Account not found.");
                Console.ResetColor();
            }
        }

        public void UserDetails(string accountUser)
        {
            var account = Accounts.FirstOrDefault(a => a.Username == accountUser);
            if (account != null)
            {
                DateTime userTime = DateTime.Now;
                account!.Transactions ??= new List<string>();

                account.Transactions!.Add($"{userTime}:Checked user details,Status:successful");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"User details for account User {accountUser}:");
                Console.WriteLine($"User name: {account.Username}");
                Console.WriteLine($"Password: {account.Password}");
                Console.WriteLine($"Balance: {account.Balance}");
                Console.ResetColor();
            }
            else
            {
                DateTime userTime = DateTime.Now;
                account!.Transactions ??= new List<string>();

                account!.Transactions!.Add($"{userTime}:Failed to check user details due to account not found.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Account not found.");
                Console.ResetColor();
            }
        }

        public void AddAccount(Account account)
        {

            Accounts.Add(account);
            DateTime dateTime = DateTime.Now;
            account!.Transactions ??= new List<string>();

            account.Transactions!.Add($"{dateTime}:Account created:status:successful");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Account with Username {account.Username} added successfully.");
            Console.ResetColor();
        }

        public bool VerifyPassword(string username , string password)
        {
            var account = Accounts.FirstOrDefault(a => a.Username == username);
            return account != null && account.Password == password;
        }
    }
}

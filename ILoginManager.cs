namespace Banking_Application
{
    public interface ILoginManager
    {
        void AddMoney(string accountUser,string password, decimal amount);
        void TransferMoney(string password,string fromAccountUser, string toAccountUser, decimal amount);
        void WithdrawMoney(string accountUser,string password, decimal amount);
        void CheckBalance(string accountUser, string password);
        void Receipt(string accountUser, string password);
        void ResetPassword(string accountUser,string password, string newPassword);
        void Transactions(string accountUser,string password);
        void UserDetails(string accountUser);
        void AddAccount(Account account);
        bool VerifyPassword(string accountUser, string password); 
    }
}
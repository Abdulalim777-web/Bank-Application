namespace Banking_Application
{
    public class Account
    {
        public string Username { get; set; } =  default!;
        public string? Name { get; set; } = default;
        public DateTime Logtime { get; set; } = default;
        public string? Password { get; set; } = default;
        public decimal Balance { get; set; } = default;
        public List<string>? Transactions { get; set; } = new List<string>();

        public Account()
        {
            Transactions = [];
        }
    }
}
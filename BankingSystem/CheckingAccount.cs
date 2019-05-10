namespace BankingSystem
{
    public class CheckingAccount : Account
    {
        public CheckingAccount() : base()
        { }
        public CheckingAccount(decimal funds, string accountName, AccountType accountType) : base(funds, accountName, accountType)
        { }

    }
}

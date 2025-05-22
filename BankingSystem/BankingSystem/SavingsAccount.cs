namespace BankingSystem
{
    public class SavingsAccount : Account
    {
        public SavingsAccount() : base()
        { }

        public SavingsAccount(decimal funds, string accountName, AccountType accountType) : base(funds, accountName, accountType)
        { }
    }
}

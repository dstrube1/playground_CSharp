// See https://aka.ms/new-console-template for more information
namespace BankingSystem
{
    class Program
    {
        const string DEPOSIT = "1";
        const string WITHDRAW = "2";
        const string VIEW = "3";
        const string QUIT = "4";

        static List<Account> accounts;

        static void Main(string[] args)
        {
            createAccounts();

            var input = "";
            while (input != QUIT) 
            {
                Console.WriteLine("What would you like to do: " +
                	$"\n({DEPOSIT}) Deposit funds " +
                	$"\n({WITHDRAW}) Withdraw funds " +
                	$"\n({VIEW}) View funds " +
                	$"\n({QUIT}) Quit");
                input = Console.ReadLine();
                switch (input)
                {
                    case DEPOSIT:
                        Console.WriteLine("Deposit funds");
                        foreach (var account in accounts)
                        {
                            try {
                                Console.WriteLine($"Depositing $1 in account {account.GetName()}. Resulting funds are: {string.Format("{0:C}", account.DepositFunds(1M))}");
                            }
                            catch(Exception exception)
                            {
                                Console.WriteLine(exception.Message);
                            }
                        }
                        break;
                    case WITHDRAW:
                        Console.WriteLine("Withdraw funds");
                        foreach (var account in accounts)
                        {
                            try
                            {
                                Console.WriteLine($"Withdrawing $1 from account {account.GetName()}. Resulting funds are: {string.Format("{0:C}", account.WithdrawFunds(1M))}");
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception.Message);
                            }
                        }
                        break;
                    case VIEW:
                        Console.WriteLine("View funds");
                        foreach (var account in accounts)
                        {
                            account.ViewFunds();
                        }
                        break;
                    case QUIT:
                        Console.WriteLine("Quitting. Have a nice day!");
                        break;
                    default:
                        Console.WriteLine("Unrecognized input. Please try again.");
                        break;
                }
            }
        }

        static void createAccounts() 
        {
            accounts = new List<Account>();
            var checkingAccount = new CheckingAccount(1M,"Fun Checking", AccountType.Checking);
            var savingsAccount = new SavingsAccount(2M, "Yay Savings", AccountType.Savings);

            accounts.Add(checkingAccount);
            accounts.Add(savingsAccount);
        }
    }
}


using System;

namespace BankingSystem
{
    public abstract class Account
    {
        protected decimal _funds;
        protected readonly string _accountName;
        protected readonly AccountType _accountType;

        protected Account() 
        {
            _funds = 0;
            _accountName = "My Account";
            _accountType = AccountType.Unspecified;
        }
        protected Account(decimal funds, string accountName, AccountType accountType) 
        {
            _funds = funds;
            _accountName = accountName;
            _accountType = accountType;
        }
        public decimal DepositFunds(decimal funds) 
        {
            if (funds < 0)//What about when trying to deposit $0? Should we allow that?
            {
                throw new Exception($"Cannot deposit negative funds in {_accountType} account {_accountName}.");
            }
            if (decimal.Remainder(funds, 0.01M) > 0)
            {
                throw new Exception($"Cannot deposit values less than 1 cent in {_accountType} account {_accountName}. " +
                	$"Attemped deposit: {string.Format("{0:C}", funds)}");
            }
            _funds += funds;
            return _funds;
        }

        public void ViewFunds()
        {
            Console.WriteLine($"Funds for {_accountType} account {_accountName} are as follows: {string.Format("{0:C}", _funds)}");
        }

        public decimal WithdrawFunds(decimal funds)
        {
            if (funds < 0)//What about when trying to withdraw $0? Should we allow that?
            {
                throw new Exception($"Cannot withdraw negative funds from {_accountType} account {_accountName}." );
            }
            if (funds > _funds)
            {
                throw new Exception($"Attemped to withdraw ${string.Format("{0:C}", funds)}. " +

                    $"Cannot withdraw more funds than available from {_accountType} account {_accountName}. Available funds are {string.Format("{0:C}", _funds)}");
            }
            if (decimal.Remainder(funds, 0.01M) > 0)
            {
                throw new Exception($"Cannot withdraw values less than 1 cent from {_accountType} account {_accountName}. " +
                	$"Attemped withdrawl: {string.Format("{0:C}", funds)}");
            }
            _funds -= funds;
            return _funds;
        }

        public string GetName() 
        {
            return _accountName;
        }
    }

    public enum AccountType { Checking, Savings, Unspecified};
}

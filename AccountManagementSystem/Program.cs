namespace AccountManagementSystem
{

    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string name = "Unnamed Account", double balance = 0.0)
        {
            this.Name = name;
            this.Balance = balance;
        }

        public bool Deposit(double amount)
        {
            if (amount < 0)
                return false;
            else
            {
                Balance += amount;
                return true;
            }
        }

        public bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"Name: {Name} , Balance : {Balance}";
        }
    }


    public static class AccountUtil<T> where T : Account
    {
        // Utility helper functions for Account class
        public static void Deposit(List<T> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void Withdraw(List<T> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }
    }




    public class CheckingAccount : Account
    {
        public CheckingAccount(string name = "Unnamed Account", double balance = 0.0, double fee = 0.0) : base(name, balance)
        {
            Fee = fee;
        }

        public double Fee { get; set; }

        public new bool Withdraw(double amount)
        {
            return base.Withdraw(amount + Fee);
        }

        public override string ToString()
        {
            return $"{base.ToString()} , Fee : {Fee}";
        }
    }

    public class SavingsAccount : Account
    {
        public SavingsAccount(string name = "Unnamed Account", double balance = 0.0, double interestRata = 0.0) : base(name, balance)
        {
            InterestRata = interestRata;
        }

        public double InterestRata { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()} , InterestRata : {InterestRata}";
        }
    }


    internal class TrustAccount : SavingsAccount
    {
        private readonly double depositThreshold = 5000.0;
        private readonly double withdrawTriesThreshold = 3;
        private readonly double withdrawThreshold = 0.20;
        private int triesCounter = 0;
        private DateTime triesDate = DateTime.Now.Date;

        public TrustAccount(string name = "Unnamed Account", double balance = 0.0, double interestRata = 0.0) : base(name, balance, interestRata)
        {
        }

        public new bool Deposit(double amount)
        {
            if (amount > depositThreshold)
            {
                return base.Deposit(amount + depositThreshold);
            }

            return base.Deposit(amount);
        }

        public new bool Withdraw(double amount)
        {
            if (DateTime.Now.Date > triesDate.AddYears(1))
            {
                triesDate = DateTime.Now.Date;
                triesCounter = 0;
            }

            if (triesCounter <= withdrawTriesThreshold && amount < (Balance * withdrawThreshold))
            {
                triesCounter++;

                base.Withdraw(amount);
                return true;
            }

            return false;
        }

    }


    internal class Program
    {
        static void Main()
        {
            // Accounts
            var accounts = new List<Account>();
            accounts.Add(new Account());
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));

            AccountUtil<Account>.Deposit(accounts, 1000);
            AccountUtil<Account>.Withdraw(accounts, 2000);

            // Savings
            var savAccounts = new List<SavingsAccount>();
            savAccounts.Add(new SavingsAccount());
            savAccounts.Add(new SavingsAccount("Superman"));
            savAccounts.Add(new SavingsAccount("Batman", 2000));
            savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

            AccountUtil<SavingsAccount>.Deposit(savAccounts, 1000);
            AccountUtil<SavingsAccount>.Withdraw(savAccounts, 2000);

            // Checking
            var checAccounts = new List<CheckingAccount>();
            checAccounts.Add(new CheckingAccount());
            checAccounts.Add(new CheckingAccount("Larry2"));
            checAccounts.Add(new CheckingAccount("Moe2", 2000));
            checAccounts.Add(new CheckingAccount("Curly2", 5000));

            AccountUtil<CheckingAccount>.Deposit(checAccounts, 1000);
            AccountUtil<CheckingAccount>.Withdraw(checAccounts, 2000);
            AccountUtil<CheckingAccount>.Withdraw(checAccounts, 2000);

            // Trust
            var trustAccounts = new List<TrustAccount>();
            trustAccounts.Add(new TrustAccount());
            trustAccounts.Add(new TrustAccount("Superman2"));
            trustAccounts.Add(new TrustAccount("Batman2", 2000));
            trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

            AccountUtil<TrustAccount>.Deposit(trustAccounts, 1000);
            AccountUtil<TrustAccount>.Deposit(trustAccounts, 6000);
            AccountUtil<TrustAccount>.Withdraw(trustAccounts, 2000);
            AccountUtil<TrustAccount>.Withdraw(trustAccounts, 3000);
            AccountUtil<TrustAccount>.Withdraw(trustAccounts, 500);

            Console.WriteLine();
        }
    }
}

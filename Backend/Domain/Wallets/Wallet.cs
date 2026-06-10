using Domain.Common;
using Domain.Wallets.Transaction;


namespace Domain.Wallets
{
    public class Wallet: BaseEntity
    {

        public string Name { get; private set; }

        public decimal InitialBalance { get; private set; }

        public decimal CurrentBalance { get; private set; }

        public decimal MonthlyBudget { get; private set; }

        public string Currency { get; private set; }

        public Guid UserId { get; private set; }

        public UserEntity User { get; private set; } = default!;

        public ICollection<Transaction> Transactions { get; private set; }
            = new List<Transaction>();


        private Wallet()
        {
        }

        public Wallet(
            string name,
            decimal initialBalance,
            decimal monthlyBudget,
            string currency,
            Guid userId)
        {
            Id = Guid.NewGuid();

            Name = name;
            InitialBalance = initialBalance;
            CurrentBalance = initialBalance;
            MonthlyBudget = monthlyBudget;
            Currency = currency;
            UserId = userId;

            CreatedAt = DateTime.UtcNow;
        }

        public void Update(
            string name,
            decimal monthlyBudget,
            string currency)
        {
            Name = name;
            MonthlyBudget = monthlyBudget;
            Currency = currency;

            UpdatedAt = DateTime.UtcNow;
        }

        public void SoftDelete()
        {
            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void RecalculateBalance()
        {
            var income = Transactions
                .Where(x => x.Type == TransactionType.Income)
                .Sum(x => x.Amount);

            var expense = Transactions
                .Where(x => x.Type == TransactionType.Expense)
                .Sum(x => x.Amount);

            CurrentBalance =
                InitialBalance
                + income
                - expense;
        }
    }


}

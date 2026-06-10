using Domain.Common;
using Domain.Wallets.Transaction.Catergories;
using Domain.Wallets.Transaction.Tags;


namespace Domain.Wallets.Transaction
{
    public sealed class Transaction : BaseEntity
    {
        public string Title { get; private set; } = default!;

        public string? Description { get; private set; }

        public decimal Amount { get; private set; }

        public TransactionType Type { get; private set; }

        public DateTime TransactionDate { get; private set; }

        public Guid WalletId { get; private set; }

        public Wallet Wallet { get; private set; } = default!;

        public Guid CategoryId { get; private set; }

        public Category Category { get; private set; } = default!;

        public ICollection<Tag> Tags { get; private set; }
            = new List<Tag>();

        private Transaction()
        {
        }

        public Transaction(
            string title,
            string? description,
            decimal amount,
            TransactionType type,
            DateTime transactionDate,
            Guid walletId,
            Guid categoryId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.");

            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            Id = Guid.NewGuid();

            Title = title.Trim();
            Description = description?.Trim();
            Amount = amount;
            Type = type;
            TransactionDate = transactionDate;

            WalletId = walletId;
            CategoryId = categoryId;

            CreatedAt = DateTime.UtcNow;
        }

        public void Update(
            string title,
            string? description,
            decimal amount,
            DateTime transactionDate,
            Guid categoryId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.");

            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            Title = title.Trim();
            Description = description?.Trim();
            Amount = amount;
            TransactionDate = transactionDate;
            CategoryId = categoryId;

            UpdatedAt = DateTime.UtcNow;
        }

        public void AddTag(Tag tag)
        {
            if (Tags.Any(x => x.Id == tag.Id))
                return;

            Tags.Add(tag);
        }

        public void RemoveTag(Guid tagId)
        {
            var tag = Tags.FirstOrDefault(x => x.Id == tagId);

            if (tag is null)
                return;

            Tags.Remove(tag);
        }
    }


}

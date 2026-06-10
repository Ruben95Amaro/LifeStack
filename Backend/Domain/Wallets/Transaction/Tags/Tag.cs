using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Wallets.Transaction.Tags
{
    public sealed class Tag : BaseEntity
    {
        public string Name { get; private set; } = default!;

        public Guid UserId { get; private set; }

        public UserEntity User { get; private set; } = default!;

        public ICollection<Transaction> Transactions { get; private set; }
            = new List<Transaction>();

        private Tag()
        {
        }

        public Tag(
            string name,
            Guid userId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tag name is required.");

            Id = Guid.NewGuid();

            Name = name.Trim();
            UserId = userId;

            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tag name is required.");

            Name = name.Trim();

            UpdatedAt = DateTime.UtcNow;
        }
    }
}

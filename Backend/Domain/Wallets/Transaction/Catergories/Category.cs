using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Wallets.Transaction.Catergories
{
    public sealed class Category : BaseEntity
    {
        public string Name { get; private set; } = default!;

        public string? Icon { get; private set; }

        public string? Color { get; private set; }

        public CategoryType Type { get; private set; }

        public bool IsDefault { get; private set; }

        public Guid? UserId { get; private set; }

        public UserEntity? User { get; private set; }

        public ICollection<Transaction> Transactions { get; private set; }
            = new List<Transaction>();

        private Category()
        {
        }

        public Category(
            string name,
            CategoryType type,
            bool isDefault,
            Guid? userId = null,
            string? icon = null,
            string? color = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name is required.");

            Id = Guid.NewGuid();

            Name = name.Trim();
            Type = type;
            IsDefault = isDefault;
            UserId = userId;
            Icon = icon;
            Color = color;

            CreatedAt = DateTime.UtcNow;
        }

        public void Update(
            string name,
            string? icon,
            string? color)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name is required.");

            Name = name.Trim();
            Icon = icon;
            Color = color;

            UpdatedAt = DateTime.UtcNow;
        }
    }
}

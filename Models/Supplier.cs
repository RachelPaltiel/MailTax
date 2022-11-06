using System;
using System.Collections.Generic;

namespace MailTax.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Expenses = new HashSet<Expense>();
        }

        public string SupplierName { get; set; } = null!;
        public string? SupplierId { get; set; }
        public int? Category { get; set; }

        public virtual Category? CategoryNavigation { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace MailTax.Models
{
    public partial class Expense
    {
        public int ExpenseId { get; set; }
        public string? SupplierName { get; set; }
        public DateTime ExpenseDate { get; set; }
        public double ExpenseAmmount { get; set; }
        public string? ExpenseDescription { get; set; }

        public virtual Supplier? SupplierNameNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace MailTax.Models
{
    public partial class Category
    {
        public Category()
        {
            Suppliers = new HashSet<Supplier>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace MailTax.Models
{
    public partial class Doc
    {
        public int DocId { get; set; }
        public int? FolderId { get; set; }
        public string DocName { get; set; } = null!;
        public string? DocDesc { get; set; }
        public string DocPath { get; set; } = null!;

        public virtual Folder? Folder { get; set; }
    }
}

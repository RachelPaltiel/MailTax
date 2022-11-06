using System;
using System.Collections.Generic;

namespace MailTax.Models
{
    public partial class Folder
    {
        public Folder()
        {
            Docs = new HashSet<Doc>();
        }

        public int FolderId { get; set; }
        public string FolderName { get; set; } = null!;
        public string? FolderDesc { get; set; }

        public virtual ICollection<Doc> Docs { get; set; }
    }
}

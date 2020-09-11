using System;
using System.Collections.Generic;

namespace ChatRoom_Middleware.Models
{
    public partial class ArchivedInvoiceLineItems
    {
        public int InvoiceID { get; set; }
        public short InvoiceSequence { get; set; }
        public int? AccountNo { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
    }
}

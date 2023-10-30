using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vb_DTO
{
    public class PaymentRequest
    {
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public int ReferenceNumber { get; set; }
    }

    public class PaymentResponse
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public int ReferenceNumber { get; set; }
        public int InvoiceId { get; set; }
        public virtual InvoiceResponse Invoice { get; set; }
    }

    public class PaymentResponseShort
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public int ReferenceNumber { get; set; }
        public int InvoiceId { get; set; }
    }
}

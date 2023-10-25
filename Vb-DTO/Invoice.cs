using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vb_DTO
{
    public class InvoiceRequest
    {
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
    }

    public class InvoiceResponse
    {
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public bool InvoiceExist { get; set; }

        public int OrderId { get; set; }
        public virtual OrderResponse Order { get; set; }
        public int PaymentId { get; set; }
        public virtual PaymentResponse Payment { get; set; }

        public virtual List<InvoiceDetailResponse> InvoiceDetails { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vb_DTO
{
    public class OrderRequest
    {
        public string PaymentMethod { get; set; }
        public Dictionary<int, int> ProductList { get; set; }
    }

    public class OrderResponse
    {
        public int OrderNumber { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public bool PaymentSuccess { get; set; }
        public bool CompanyApprove { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }

        public int DealerId { get; set; }
        public virtual DealerResponseShort Dealer { get; set; }
        public int CompanyId { get; set; }
        public virtual CompanyResponseShort Company { get; set; }

        public virtual List<InvoiceResponse> Product { get; set; }
    }

    public class OrderResponseShort
    {
        public int OrderNumber { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public bool PaymentSuccess { get; set; }
        public bool CompanyApprove { get; set; }
        public string Address { get; set; }

        public int DealerId { get; set; }
        public int CompanyId { get; set; }

        public virtual List<OrderDetailResponse> Product { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vb_DTO
{
    public class ReportRequest
    {
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
    }

    public class ReportResponse
    {
        public string totalAmount { get; set; }
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
        public string mostUsedPaymentMethod { get; set; }
        public string mostSellingProductName { get; set; }
        public string dealerNameWhoBuysMost { get; set; }
    }
}

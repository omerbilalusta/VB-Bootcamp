using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vb_Data.Domain.Report
{
    public class ReportByProduct
    {
        public decimal TotalAmountByProduct { get; set; }
        public string ProductName { get; set; }

    }

    public class ReportByOrder
    {
        public string DealerName { get; set; }
        public string PaymentMethod { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vb_Data.Domain.Report
{
    public class ReportByDate
    {
        public decimal Amount { get; set; }

        public decimal AmountByProduct { get; set; }
        public string DealerName { get; set; }
        public string ProductName { get; set; }
        public string PaymentMethod { get; set; }

    }
}

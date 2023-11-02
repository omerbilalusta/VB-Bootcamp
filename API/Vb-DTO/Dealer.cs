using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vb_DTO
{
    public class DealerRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string InvoiceAddress { get; set; }
        public decimal Dividend { get; set; }
        public decimal OpenAccountLimit { get; set; }
    }
    public class DealerRequestShort
    {
        public decimal Dividend { get; set; }
        public decimal OpenAccountLimit { get; set; }
    }
    public class DealerServiceRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string InvoiceAddress { get; set; }
    }
    public class DealerResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string InvoiceAddress { get; set; }
        public decimal Dividend { get; set; }
        public decimal OpenAccountLimit { get; set; }

        public virtual List<OrderResponseShort> Orders { get; set; }
    }

    public class DealerResponseShort
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string InvoiceAddress { get; set; }
        public decimal Dividend { get; set; }
        public decimal OpenAccountLimit { get; set; }
    }
}

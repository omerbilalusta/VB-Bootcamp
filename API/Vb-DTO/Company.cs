using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vb_DTO
{
    public class CompanyRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int TaxNumber { get; set; }
        public string IBAN { get; set; }
    }

    public class CompanyResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int TaxNumber { get; set; }
        public string IBAN { get; set; }

        public virtual List<ProductResponse> Products { get; set; }
    }

    public class CompanyResponseShort
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int TaxNumber { get; set; }
        public string IBAN { get; set; }
    }
}

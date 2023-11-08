using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vb_DTO
{
    public class OrderDetailRequest
    {
        public int Piece { get; set; }
        public int TotalAmountByProduct { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }

    public class OrderDetailResponse
    {
        public int Id { get; set; }
        public int Piece { get; set; }
        public int TotalAmountByProduct { get; set; }
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int OrderNumber { get; set; }
    }
}

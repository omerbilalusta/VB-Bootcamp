using AutoMapper;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_Base.Response;
using Vb_Data.Domain.Report;
using Vb_Data.UnitOfWork;
using Vb_DTO;
using Vb_Operation.Cqrs;

namespace Vb_Operation.Query
{
    public class ReportQueryHandler : IRequestHandler<GetReportByDateQuery, ApiResponse<ReportResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ReportQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<ReportResponse>> Handle(GetReportByDateQuery request, CancellationToken cancellationToken)
        {
            decimal total = 0;
            var listProduct = DapperQueryOrderDetails(request.dateFrom, request.dateTo, request.userId);
            var listOrder = DapperQueryOrders(request.dateFrom, request.dateTo, request.userId);

            var mostSellingProductName = listProduct.GroupBy(x => x.ProductName).First().Key;
            var dealerNameWhoBuysMost = listOrder.GroupBy(x => x.DealerName).First().Key;
            var mostUsedPaymentMethod = listOrder.GroupBy(x => x.PaymentMethod).First().Key;
            foreach (var item in listProduct)
                total += item.TotalAmountByProduct;

            ReportResponse response = new ReportResponse()
            {
                dateFrom = request.dateFrom,
                dateTo = request.dateTo,
                totalAmount = total.ToString(),
                mostSellingProductName = mostSellingProductName,
                mostUsedPaymentMethod = mostUsedPaymentMethod,
                dealerNameWhoBuysMost = dealerNameWhoBuysMost
            };
            return new ApiResponse<ReportResponse>(response);
        }


        public List<ReportByProduct> DapperQueryOrderDetails(DateTime DateFrom, DateTime DateTo, int userId)
        {
            var dateFrom = DateFrom.ToString("yyyy-MM-dd HH:mm:ss") + ".0000000";
            var dateTo = DateFrom.ToString("yyyy-MM-dd HH:mm:ss") + ".0000000";
            var query = "SELECT [o0].TotalAmountByProduct, [p].Name as ProductName, [o].PaymentMethod, [d].Name as DealerName  FROM [OrderDetail] AS [o0]" +
                " LEFT JOIN [Order] AS [o] ON [o].[Id] = [o0].[OrderId]" +
                " LEFT JOIN [Company] AS [c] ON [o].[CompanyId] = [c].[Id]" +
                " LEFT JOIN [Product] AS [p] ON [p].[Id] = [o0].[Id]" +
                " LEFT JOIN [Dealer] AS [d] ON [o].[DealerId] = [d].[Id]" +
                " where [o].InsertDate Between @dateFrom and @dateTo and [o].CompanyId = @companyId";

            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("dateFrom", DateFrom);
            dynamicParameters.Add("dateTo", DateTo);
            dynamicParameters.Add("companyId", userId);
            using (SqlConnection con = new SqlConnection("Server=(localdb)\\mssqllocaldb; Database=Vb-DB;Trusted_Connection=false;TrustServerCertificate=True;"))
            {
                List<ReportByProduct> results = con.Query<ReportByProduct>(query, dynamicParameters).ToList();
                return results;
            }
        }

        public List<ReportByOrder> DapperQueryOrders(DateTime DateFrom, DateTime DateTo, int userId)
        {
            var dateFrom = DateFrom.ToString("yyyy-MM-dd HH:mm:ss") + ".0000000";
            var dateTo = DateFrom.ToString("yyyy-MM-dd HH:mm:ss") + ".0000000";
            var query = "SELECT [d].Name as DealerName,[o].PaymentMethod   FROM [Order] AS [o]" +
                " LEFT JOIN [Dealer] AS [d] ON [o].[DealerId] = [d].[Id]" +
                " where [o].InsertDate Between @dateFrom and @dateTo and [o].CompanyId = @companyId";

            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("dateFrom", DateFrom);
            dynamicParameters.Add("dateTo", DateTo);
            dynamicParameters.Add("companyId", userId);
            using (SqlConnection con = new SqlConnection("Server=(localdb)\\mssqllocaldb; Database=Vb-DB;Trusted_Connection=false;TrustServerCertificate=True;"))
            {
                List<ReportByOrder> results = con.Query<ReportByOrder>(query, dynamicParameters).ToList();
                return results;
            }
        }
    }
}

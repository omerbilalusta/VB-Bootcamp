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
            var list = DapperQuery(request.dateFrom, request.dateTo, request.userId);
            var mostUsedPaymentMethod = list.OrderByDescending(d => d.PaymentMethod).First().PaymentMethod;
            var mostSellingProductName = list.OrderByDescending(d => d.ProductName).First().ProductName;
            var dealerNameWhoBuysMost = list.OrderByDescending(d => d.DealerName).First().DealerName;
            foreach (var item in list)
                total += item.Amount;

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


        public List<ReportByDate> DapperQuery(DateTime DateFrom, DateTime DateTo, int userId)
        {
            var dateFrom = DateFrom.ToString("yyyy-MM-dd HH:mm:ss") + ".0000000";
            var dateTo = DateFrom.ToString("yyyy-MM-dd HH:mm:ss") + ".0000000";
            var query = "SELECT [o].Amount, [o0].TotalAmountByProduct, [d].Name as DealerName, [p].Name as ProductName, [o].PaymentMethod  FROM [Order] AS [o]" +
                " LEFT JOIN [Company] AS [c] ON [o].[CompanyId] = [c].[Id]" +
                " LEFT JOIN [Dealer] AS [d] ON [o].[DealerId] = [d].[Id]" +
                " LEFT JOIN [Product] AS [p] ON [p].[CompanyId] = [c].[Id]" +
                " LEFT JOIN [OrderDetail] AS [o0] ON [o].[Id] = [o0].[OrderId]" +
                " where [o].InsertDate Between @dateFrom and @dateTo and [o].CompanyId = @companyId";
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("dateFrom", DateFrom);
            dynamicParameters.Add("dateTo", DateTo);
            dynamicParameters.Add("companyId", userId);
            using (SqlConnection con = new SqlConnection("Server=(localdb)\\mssqllocaldb; Database=Vb-DB;Trusted_Connection=false;TrustServerCertificate=True;"))
            {
                List<ReportByDate> results = con.Query<ReportByDate>(query, dynamicParameters).ToList();
                return results;
            }
        }
    }
}

using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_Base.Response;
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
            var entity = unitOfWork.DapperRepository.DapperQuery(request.dateFrom, request.dateTo);
            Console.WriteLine(entity);
            return new ApiResponse<ReportResponse>("asda");
        }
    }
}

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
    public class CompanyQueryHandler :
        IRequestHandler<GetAllCompanyQuery, ApiResponse<List<CompanyResponse>>>, 
        IRequestHandler<GetCompanyByIdQuery, ApiResponse<CompanyResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CompanyQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<CompanyResponse>>> Handle(GetAllCompanyQuery request, CancellationToken cancellationToken)
        {
            var list = unitOfWork.CompanyRepository.GetAsQueryable().ToList();
            var mappedList = mapper.Map<List<CompanyResponse>>(list);
            return new ApiResponse<List<CompanyResponse>>(mappedList);
        }

        public async Task<ApiResponse<CompanyResponse>> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = unitOfWork.CompanyRepository.GetAsQueryable().Where(x => x.Id == request.Id).FirstOrDefault();
            var mappedList = mapper.Map<CompanyResponse>(entity);
            return new ApiResponse<CompanyResponse>(mappedList);
        }
    }
}

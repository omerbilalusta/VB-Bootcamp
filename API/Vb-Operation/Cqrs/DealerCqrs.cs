using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_Base.Response;
using Vb_DTO;

namespace Vb_Operation.Cqrs
{
    public record CreateDealerCommand(DealerRequest model, int userId) : IRequest<ApiResponse<DealerResponse>>;
    public record CreateDealerServiceCommand(DealerServiceRequest model, int userId) : IRequest<ApiResponse<DealerResponseShort>>;
    public record UpdateDealerCommand(DealerRequest model, int Id, int userId) : IRequest<ApiResponse>;
    public record UpdateDealerShortCommand(DealerRequestShort model, int Id, int userId) : IRequest<ApiResponse>;
    public record DeleteDealerCommand(int Id, int userId) : IRequest<ApiResponse>;
    public record GetAllDealerQuery() : IRequest<ApiResponse<List<DealerResponse>>>;
    public record GetDealerByIdQuery(int id) : IRequest<ApiResponse<DealerResponse>>;
}

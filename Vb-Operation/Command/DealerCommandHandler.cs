﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_Base.Response;
using Vb_Data.Domain.User;
using Vb_Data.UnitOfWork;
using Vb_DTO;
using Vb_Operation.Cqrs;

namespace Vb_Operation.Command
{
    public class DealerCommandHandler :
        IRequestHandler<CreateDealerCommand, ApiResponse<DealerResponse>>,
        IRequestHandler<UpdateDealerCommand, ApiResponse>,
        IRequestHandler<DeleteDealerCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DealerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<DealerResponse>> Handle(CreateDealerCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<Dealer>(request.model);
            var entity = await unitOfWork.DealerRepository.CreateAsync(mapped, request.userId, cancellationToken);
            unitOfWork.CommitAsync(cancellationToken);

            var response = mapper.Map<DealerResponse>(entity);
            return new ApiResponse<DealerResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateDealerCommand request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.DealerRepository.GetAsQueryable().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
                return new ApiResponse("Record not found");

            entity.Name = request.model.Name;
            entity.Address = request.model.Address;
            entity.InvoiceAddress = request.model.InvoiceAddress;
            entity.Dividend = request.model.Dividend;
            entity.OpenAccountLimit = request.model.OpenAccountLimit;
            entity.UpdateUserId = request.userId;
            entity.UpdateDate = DateTime.Now;

            unitOfWork.DealerRepository.Update(entity, request.userId);
            unitOfWork.CommitAsync(cancellationToken);

            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteDealerCommand request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.DealerRepository.GetAsQueryable().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
                return new ApiResponse("Record not found");

            unitOfWork.DealerRepository.Delete(entity, request.userId);
            unitOfWork.CommitAsync(cancellationToken);
            return new ApiResponse();
        }
    }
}

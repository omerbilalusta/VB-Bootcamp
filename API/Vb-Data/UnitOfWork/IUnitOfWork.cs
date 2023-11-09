using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_Base.Model;
using Vb_Data.Domain;
using Vb_Data.Domain.User;
using Vb_Data.Repository;

namespace Vb_Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        void CommitAsync(CancellationToken cancellationToken);
        void CommitTransaction();
        IGenericRepository<Company> CompanyRepository { get; }
        IGenericRepository<Dealer> DealerRepository { get; }
        IGenericRepository<Chat> ChatRepository { get; }
        IGenericRepository<Invoice> InvoiceRepository { get; }
        IGenericRepository<OrderDetail> OrderDetailRepository { get; }
        IGenericRepository<Message> MessageRepository { get; }
        IGenericRepository<Order> OrderRepository { get; }
        IGenericRepository<OrderReject> OrderRejectRepository { get; }
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<Payment> PaymentRepository { get; }
    }
}

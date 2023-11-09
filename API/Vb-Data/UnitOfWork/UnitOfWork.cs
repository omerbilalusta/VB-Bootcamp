using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vb_Base.Model;
using Vb_Data.Context;
using Vb_Data.Domain;
using Vb_Data.Domain.User;
using Vb_Data.Repository;

namespace Vb_Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VbDbContext dbContext;
        public UnitOfWork(VbDbContext dbContext)
        {
            this.dbContext = dbContext;

            CompanyRepository = new GenericRepository<Company>(dbContext);
            DealerRepository = new GenericRepository<Dealer>(dbContext);
            ChatRepository = new GenericRepository<Chat>(dbContext);
            InvoiceRepository = new GenericRepository<Invoice>(dbContext);
            OrderDetailRepository = new GenericRepository<OrderDetail>(dbContext);
            MessageRepository = new GenericRepository<Message>(dbContext);
            OrderRepository = new GenericRepository<Order>(dbContext);
            OrderRejectRepository = new GenericRepository<OrderReject>(dbContext);
            ProductRepository = new GenericRepository<Product>(dbContext);
            PaymentRepository = new GenericRepository<Payment>(dbContext);

        }

        public void CommitAsync(CancellationToken cancellationToken)
        {
             dbContext.SaveChanges();
        }

        public void CommitTransaction() //Transaction işleminin async olmaması gerektiğini düşündüm. Çünkü transaction içerisinde manipüle
        {                               //ettiğimiz veriler RollBack gerçekleşmesi gerektiği takdirde veri tutarlılığını bozacağını düşündüm.
                                        //Bu düşünce hatalı ise benim bildiğim Transaction mantığının hatalı olduğundandır.
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new InvalidOperationException("Transaction failed. " + ex.Message);
                }
            }
        }

        public IGenericRepository<Company> CompanyRepository { get; private set; }
        public IGenericRepository<Dealer> DealerRepository { get; private set; }
        public IGenericRepository<Chat> ChatRepository { get; private set; }
        public IGenericRepository<Invoice> InvoiceRepository { get; private set; }
        public IGenericRepository<OrderDetail> OrderDetailRepository { get; private set; }
        public IGenericRepository<Message> MessageRepository { get; private set; }
        public IGenericRepository<Order> OrderRepository { get; private set; }
        public IGenericRepository<OrderReject> OrderRejectRepository { get; private set; }
        public IGenericRepository<Product> ProductRepository { get; private set; }
        public IGenericRepository<Payment> PaymentRepository { get; private set; }
    }
}

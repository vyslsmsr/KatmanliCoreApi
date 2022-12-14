using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// bu alanda async ve async olmayan iki adet savechange method tanımladık. async olan işlemlerde async method ile işlem yapacaz async olmayan işlemlerde ise normal method ile işlem yapacaz.
        /// </summary>
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Comit()
        {
           _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

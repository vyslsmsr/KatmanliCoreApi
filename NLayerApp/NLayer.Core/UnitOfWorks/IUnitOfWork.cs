using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// bu alanda ise işlemler veri tabanına eklenmeden yada silinmeden yani save change yapılmadığı sürece bu memory alanda tutar.
        /// </summary>
        Task CommitAsync();
        void Comit();
    }
}

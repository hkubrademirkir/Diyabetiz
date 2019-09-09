using System;

namespace Diyabetiz.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        //void OpenTransaction();
        //void CloseTransaction();
    }
}

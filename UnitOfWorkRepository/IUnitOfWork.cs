﻿#region

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace UnitOfWorkRepository
{
    public interface IUnitOfWork : IUnitOfWorkForService
    {
        void Save();
        void Dispose(bool disposing);
        void Dispose();
    }

    public interface IUnitOfWorkForService
    {
        IRepository<T> Repository<T>() where T : class, new();
    }
}
﻿#region

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

#endregion

namespace UnitOfWorkRepository
{
    public interface IDbContext
    {
        IDbSet<T> Set<T>() where T : class;
        int SaveChanges();
        void Dispose();
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        void ApplyStateChanges();
        DbContextConfiguration Configuration { get; }
        Guid InstanceId { get; }
    }
}
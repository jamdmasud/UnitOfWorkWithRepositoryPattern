using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using UnitOfWorkRepository;

namespace UnitOfWorkRepository
{
    public class DbContextBase : DbContext, IDbContext
    {
        private readonly Guid _instanceId;
        public DbContextBase(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            _instanceId = Guid.NewGuid();
            Database.Log = tSql => Debug.Write(tSql);
        }

        public Guid InstanceId
        {
            get { return _instanceId; }
        }

        public void ApplyStateChanges()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                var entityState = dbEntityEntry.Entity as IObjectState;
                if (entityState == null)
                    throw new InvalidCastException("All entites must implement the IObjectState interface, " +
                                                   "this interface must be implemented so each entites state can explicitely determined when updating graphs.");

                dbEntityEntry.State = StateHelper.ConvertState(entityState.State);
            }
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ApplyStateChanges();
            return base.SaveChanges();
        }
    }
}
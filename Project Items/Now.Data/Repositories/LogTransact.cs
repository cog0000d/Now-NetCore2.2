using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Now.Data.Interfaces;

namespace Now.Data.Repositories
{
    public abstract class LogTransact<TContext> : ILogTransact where TContext : DbContext
    {
        private readonly TContext _context;

        protected LogTransact(TContext context)
        {
            _context = context;
        }

        public virtual void Dispose()
        {
            _context.Dispose();
        }

        public virtual void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual Task SaveAsync()
        {
            try
            {
                _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Task.FromResult(0);
        }
    }
}
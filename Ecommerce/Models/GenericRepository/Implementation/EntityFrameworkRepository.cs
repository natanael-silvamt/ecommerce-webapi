using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Models.EntityFrameWork;
using Ecommerce.Models.GenericRepository.Repository;


namespace Ecommerce.Models.GenericRepository.Implementation
{
    public class EntityFrameworkRepository : EntityFrameworkRepositoryReadOnly, IRepository
    {
        private readonly EntityFrameWorkDbContext _context;
        public EntityFrameworkRepository(EntityFrameWorkDbContext context) : base(context)
        {
            _context = context;
        }

        public virtual void Create<TEntity>(TEntity entity, string createdBy = null) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
        }

        public virtual void Update<TEntity>(TEntity entity, string modifiedBy = null) where TEntity : class
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            var dbSet = _context.Set<TEntity>();
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        }

        public virtual void Delete<TEntity>(object id) where TEntity : class
        {
            var entity = _context.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public virtual void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                ThrowEnhancedValidationException(e);
            }
        }

        public virtual Task SaveAsync()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                ThrowEnhancedValidationException(e);
            }

            return Task.FromResult(0);
        }
        
        protected virtual void ThrowEnhancedValidationException(DbUpdateException e)
        {
            throw new DbUpdateException(e.Message, e.InnerException);
        }
    }
}
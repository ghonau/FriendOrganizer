﻿using FriendOrganizer.Model;

using System.Data.Entity;

namespace FriendOrganizer.UI.Data.Repositories
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity> 
        where TContext: DbContext
        where TEntity : class
    {
         public readonly TContext Context;

        public GenericRepository(TContext context) 
        {
            this.Context = context; 
        }
        public void Add(TEntity model)
        {
            Context.Set<TEntity>().Add(model); 
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            
            return await Context.Set<TEntity>().FindAsync(id); 
        }

        public bool HasChanges()
        {
            return Context.ChangeTracker.HasChanges();
        }

        public void Remove(TEntity model)
        {
            Context.Set<TEntity>().Remove(model);
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync(); 
        }       
    }
}

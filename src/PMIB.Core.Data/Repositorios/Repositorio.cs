using Microsoft.EntityFrameworkCore;
using PMIB.Core.Business.Interfaces;
using PMIB.Core.Business.Models;
using PMIB.Core.Data.Context;
using System.Linq.Expressions;

namespace PMIB.Core.Data.Repositorios;

public abstract class Repositorio<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
{
    protected readonly PmibContext Db;
    protected readonly DbSet<TEntity> DbSet;

    protected Repositorio(PmibContext context)
    {
        Db = context;
        DbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public virtual async Task<TEntity> ObterPorId(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<List<TEntity>> ObterTodos()
    {
        return await DbSet.ToListAsync();
    }

    public virtual async Task Adicionar(TEntity entity)
    {
        DbSet.Add(entity);
        await SaveChanges();
    }

    public virtual async Task Atualizar(TEntity entity)
    {
        DbSet.Update(entity);
        await SaveChanges();
    }

    public virtual async Task Remover(Guid id)
    {
        DbSet.Remove(new TEntity { Id = id });
        await SaveChanges();
    }

    private async Task<int> SaveChanges()
    {
        return await context.SaveChangesAsync();
    }

    public void Dispose()
    {
        context?.Dispose();
    }
}
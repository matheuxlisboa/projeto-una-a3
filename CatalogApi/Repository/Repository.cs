﻿using CatalogApi.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CatalogApi.Repository
{
    /// <summary>
    /// Implementação padrão do repositório para operações CRUD em uma entidade.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AppDbContext _context;

        /// <summary>
        /// Construtor para inicializar o repositório com o contexto especificado.
        /// </summary>
        /// <param name="contexto">Contexto do banco de dados.</param>
        public Repository(AppDbContext contexto)
        {
            _context = contexto;
        }

        /// <summary>
        /// Obtém todos os registros da entidade.
        /// </summary>
        /// <returns>Uma consulta IQueryable para a entidade.</returns>
        public IQueryable<T> Get()
        {
            return _context.Set<T>().AsNoTracking();
        }

        /// <summary>
        /// Obtém um registro da entidade baseado em um predicado.
        /// </summary>
        /// <param name="property">Propriedade para filtrar o registro.</param>
        /// <returns>O registro da entidade que atende ao predicado, ou null se nenhum for encontrado.</returns>
        public async Task<T> GetById(Expression<Func<T, bool>> property)
        {
            return await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(property);
        }

        /// <summary>
        /// Adiciona um novo registro à entidade.
        /// </summary>
        /// <param name="entity">Registro a ser adicionado.</param>
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        /// <summary>
        /// Deleta um registro da entidade.
        /// </summary>
        /// <param name="entity">Registro a ser deletado.</param>
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Atualiza um registro existente da entidade.
        /// </summary>
        /// <param name="entity">Registro a ser atualizado.</param>
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }
    }
}

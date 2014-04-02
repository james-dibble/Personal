﻿namespace Personal.Persistence
{
    using Personal.DomainModel;
    using System;
    using System.Linq.Expressions;
    using System.Collections.Generic;

    public interface IRepository
    {
    }

    public interface IRepository<TEntity> : IRepository where TEntity : IDomainObject
    {
        int Count();

        int Count(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Place a <typeparamref name="TEntity"/> into the <see cref="IRepository{TEntity}"/>.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to add.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Change a <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to change.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Remove a <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="entity">The <typeparamref name="TEntity"/> to remove.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete a group of <typeparamref name="TEntity"/>s using a given criterion.
        /// </summary>
        /// <param name="where">The criteria by which to delete <typeparamref name="TEntity"/>s.</param>
        void Delete(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Get a <typeparamref name="TEntity"/> using a given criterion where it would be expected
        /// that only one should be found.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>.</param>
        /// <returns>The <typeparamref name="TEntity"/> or null if none could be found.</returns>
        TEntity Single(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Get the first <typeparamref name="TEntity"/> from a collection derived from a given
        /// criterion.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>.</param>
        /// <returns>The first <typeparamref name="TEntity"/> or null if none could be found.</returns>
        TEntity First(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// Retrieve all the known <typeparamref name="TEntity"/>s.
        /// </summary>
        /// <returns>All the known <typeparamref name="TEntity"/>s.</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Retrieve a collection of <typeparamref name="TEntity"/>s defined by a criterion.
        /// </summary>
        /// <param name="where">The criteria by which to find the <typeparamref name="TEntity"/>s.</param>
        /// <returns>A collection of <typeparamref name="TEntity"/>s.</returns>
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);  
    }
}
﻿using LinkDev.Talabat.Core.Domain.Common;
using System.Linq.Expressions;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
    public interface ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        // Where [LINQ Operator]
        public Expression<Func<TEntity, bool>>? Criteria { get; set; }

        public List<Expression<Func<TEntity, object>>> Includes { get; set; }

        public Expression<Func<TEntity, object>>? OrderBy { get; set; }
        public Expression<Func<TEntity, object>>? OrderByDesc { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.DataAccess.Specifications
{
    public abstract class Specification<TEntity> where TEntity : class    
    {
        protected Specification(Expression<Func<TEntity, bool>> criteria) 
        {
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get;  }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new List<Expression<Func<TEntity, object>>>();
        public List<Expression<Func<TEntity, object>>>? OrderByExpressions { get; }

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) 
        {
            IncludeExpressions.Add(includeExpression);
        }

    }
}

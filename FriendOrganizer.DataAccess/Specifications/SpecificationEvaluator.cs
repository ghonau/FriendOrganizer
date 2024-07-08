using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.DataAccess.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> inputQuery,
            Specification<TEntity> specification) where TEntity : class
        {
            IQueryable<TEntity> query = inputQuery;
            if (specification == null) 
            { 
                if(specification.Criteria != null)
                {
                    query = query.Where(specification.Criteria);
                    
                }
                if (specification.IncludeExpressions != null) 
                {
                    // Aggregate seed ==> starting 
                    // Current will change by each iteration 
                    // And the function on the current with items 
                    specification.IncludeExpressions.Aggregate(query, (current, includeExpression)
                        => current.Include(includeExpression)); 
                }
            }
            return query;
        }
    }
}

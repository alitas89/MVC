using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;
using Dapper;

namespace Core.DataAccessLayer.Dapper
{
    public abstract class DpEntityMultiRepositoryBase<TEntity> : IEntityMultiRepository<TEntity,TEntity,TEntity>
        where TEntity : class, IEntity, new()
    {
        public List<TEntity> GetListMapping2(string query, Func<TEntity, TEntity, TEntity> mapping, object parameters)
        {
            using (IDbConnection db =
                new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                return db.Query<TEntity, TEntity, TEntity>(query, mapping, parameters, null, true).ToList();
            }
        }
    }
}

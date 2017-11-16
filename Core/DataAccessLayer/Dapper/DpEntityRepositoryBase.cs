using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;
using Dapper;
using DapperExtensions;

namespace Core.DataAccessLayer.Dapper
{
    public abstract class DpEntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        public List<TEntity> GetList(string query, object parameters)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                return db.Query<TEntity>(query, parameters).ToList();
            }
        }

        public TEntity Get(string query, object parameters)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                return db.Query<TEntity>(query, parameters).SingleOrDefault();
            }
        }

        public int Add(string query, object parameters)
        {
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                var count = connection.Execute(query, parameters);
                return count;
            }
        }

        public int Update(string query, object parameters)
        {
            return 1;
        }

        public void Delete(string query, object parameters)
        {
            throw new NotImplementedException();
        }
    }
}

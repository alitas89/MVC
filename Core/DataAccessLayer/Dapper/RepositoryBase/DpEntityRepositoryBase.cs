using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Core.DataAccessLayer.Dapper.RepositoryInterface;
using Core.EntityLayer;
using Dapper;

namespace Core.DataAccessLayer.Dapper.RepositoryBase
{
    public abstract class DpEntityRepositoryBase<TEntity> : IDapper<TEntity>
        where TEntity : class, IEntity, new()
    {
        public List<TEntity> GetListQuery(string query, object parameters)
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

        public TEntity GetQuery(string query, object parameters)
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

        public int AddQuery(string query, object parameters)
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

        public int UpdateQuery(string query, object parameters)
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

        public int DeleteQuery(string query, object parameters)
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

        public int DeleteSoftQuery(string query, object parameters)
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
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete.DatabaseModel;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpProductWithCatDal : IProductWithCategoryDal
    {
        public List<Product> GetListMapping2(string query, Func<Product, Category, Product> mapping, object parameters)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                return db.Query<TEntity, TEntity, TEntity>(query, mapping, parameters).ToList();
            }
        }
    }
}

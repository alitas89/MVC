using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper.RepositoryInterface;
using Dapper;

namespace Core.DataAccessLayer.Dapper.RepositoryBase
{
    public class DpDtoRepositoryBase<TEntity>: IDto<TEntity>
    {
        public List<TEntity> GetListDtoQuery(string query, object parameters)
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
    }
}

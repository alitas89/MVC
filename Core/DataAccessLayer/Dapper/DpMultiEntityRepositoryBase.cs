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
    public abstract class DpMultiEntityRepositoryBase<TA, TB, TC> : IMultiEntityRepository<TA, TB, TC>
        where TA : class, IEntity, new()
        where TB : class, IEntity, new()
        where TC : class, IEntity, new()
    {

        public IQueryable<TC> GetListMapping(string query, Func<TA, TB, TC> mapping, object parameters)
        {
            using (IDbConnection db =
                new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                return db.Query<TA,TB,TC>(query, mapping, parameters).AsQueryable();
            }
        }
    }
}
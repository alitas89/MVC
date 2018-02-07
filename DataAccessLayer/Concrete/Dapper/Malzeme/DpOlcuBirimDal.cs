using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace DataAccessLayer.Concrete.Dapper.Malzeme
{
    public class DpOlcuBirimDal : DpEntityRepositoryBase<OlcuBirim>, IOlcuBirimDal
    {
        public List<OlcuBirim> GetList()
        {
            return GetListQuery("select * from OlcuBirim where Silindi=0", new { });
        }

        public OlcuBirim Get(int Id)
        {
            return GetQuery("select * from OlcuBirim where OlcuBirimID= @Id and Silindi=0", new { Id });
        }

        public int Add(OlcuBirim olcubirim)
        {
            return AddQuery("insert into OlcuBirim(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)", olcubirim);
        }

        public int Update(OlcuBirim olcubirim)
        {
            return UpdateQuery("update OlcuBirim set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where OlcuBirimID=@OlcuBirimID", olcubirim);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from OlcuBirim where OlcuBirimID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update OlcuBirim set Silindi = 1 where OlcuBirimID=@Id", new { Id });
        }

        public List<OlcuBirim> GetListPagination(PagingParams pagingParams)
        {
            string filterQuery = "";
            string orderQuery = "ORDER BY 1";
            if (pagingParams.filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                pagingParams.filterVal = '%' + pagingParams.filterVal + '%';
                filterQuery = $"and {pagingParams.filterCol} like @filterVal";
            }

            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                orderQuery = $"ORDER BY {arrOrder[0]} {arrOrder[1]}";
            }

            return GetListQuery($@"SELECT * FROM OlcuBirim where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filterVal, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            string filter = "";
            if (filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                filterVal = '%' + filterVal + '%';
                filter = $"and {filterCol} like @filterVal";
            }
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM OlcuBirim where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}

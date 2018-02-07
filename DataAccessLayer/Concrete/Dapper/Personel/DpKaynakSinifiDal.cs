using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace DataAccessLayer.Concrete.Dapper.Personel
{
    public class DpKaynakSinifiDal : DpEntityRepositoryBase<KaynakSinifi>, IKaynakSinifiDal
    {
        public List<KaynakSinifi> GetList()
        {
            return GetListQuery("select * from KaynakSinifi where Silindi=0", new { });
        }

        public KaynakSinifi Get(int Id)
        {
            return GetQuery("select * from KaynakSinifi where KaynakSinifiID= @Id and Silindi=0", new { Id });
        }

        public int Add(KaynakSinifi kaynaksinifi)
        {
            return AddQuery("insert into KaynakSinifi(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)", kaynaksinifi);
        }

        public int Update(KaynakSinifi kaynaksinifi)
        {
            return UpdateQuery("update KaynakSinifi set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where KaynakSinifiID=@KaynakSinifiID", kaynaksinifi);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from KaynakSinifi where KaynakSinifiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update KaynakSinifi set Silindi = 1 where KaynakSinifiID=@Id", new { Id });
        }

        public List<KaynakSinifi> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM KaynakSinifi where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM KaynakSinifi where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}

using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace DataAccessLayer.Concrete.Dapper.Personel
{
    public class DpKaynakTuruDal : DpEntityRepositoryBase<KaynakTuru>, IKaynakTuruDal
    {
        public List<KaynakTuru> GetList()
        {
            return GetListQuery("select * from KaynakTuru where Silindi=0", new { });
        }

        public KaynakTuru Get(int Id)
        {
            return GetQuery("select * from KaynakTuru where KaynakTuruID= @Id and Silindi=0", new { Id });
        }

        public int Add(KaynakTuru kaynakturu)
        {
            return AddQuery("insert into KaynakTuru(Ad,Silindi) values (@Ad,@Silindi)", kaynakturu);
        }

        public int Update(KaynakTuru kaynakturu)
        {
            return UpdateQuery("update KaynakTuru set Ad=@Ad,Silindi=@Silindi where KaynakTuruID=@KaynakTuruID", kaynakturu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from KaynakTuru where KaynakTuruID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update KaynakTuru set Silindi = 1 where KaynakTuruID=@Id", new { Id });
        }

        public List<KaynakTuru> GetListPagination(PagingParams pagingParams)
        {
              string filterQuery = Datatables.FilterFabric(pagingParams.filter);
            string orderQuery = "ORDER BY 1";

            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                orderQuery = $"ORDER BY {arrOrder[0]} {arrOrder[1]}";
            }

            //columns ayrımı yapılır
            string columnsQuery = "*";
            if (pagingParams.columns.Length != 0)
            {
                columnsQuery = pagingParams.columns;
            }

            return GetListQuery($@"SELECT {columnsQuery} FROM KaynakTuru where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM KaynakTuru where Silindi=0 {filterQuery}", new { filterQuery }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
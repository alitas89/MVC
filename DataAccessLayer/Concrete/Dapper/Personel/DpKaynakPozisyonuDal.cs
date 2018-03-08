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
    public class DpKaynakPozisyonuDal : DpEntityRepositoryBase<KaynakPozisyonu>, IKaynakPozisyonuDal
    {
        public List<KaynakPozisyonu> GetList()
        {
            return GetListQuery("select * from KaynakPozisyonu where Silindi=0", new { });
        }

        public KaynakPozisyonu Get(int Id)
        {
            return GetQuery("select * from KaynakPozisyonu where KaynakPozisyonuID= @Id and Silindi=0", new { Id });
        }

        public int Add(KaynakPozisyonu kaynakpozisyonu)
        {
            return AddQuery("insert into KaynakPozisyonu(Kod,Ad,UstDuzeyPozisyonID,Aciklama,Teknisyendir,Silindi) values (@Kod,@Ad,@UstDuzeyPozisyonID,@Aciklama,@Teknisyendir,@Silindi)", kaynakpozisyonu);
        }

        public int Update(KaynakPozisyonu kaynakpozisyonu)
        {
            return UpdateQuery("update KaynakPozisyonu set Kod=@Kod,Ad=@Ad,UstDuzeyPozisyonID=@UstDuzeyPozisyonID,Aciklama=@Aciklama,Teknisyendir=@Teknisyendir,Silindi=@Silindi where KaynakPozisyonuID=@KaynakPozisyonuID", kaynakpozisyonu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from KaynakPozisyonu where KaynakPozisyonuID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update KaynakPozisyonu set Silindi = 1 where KaynakPozisyonuID=@Id", new { Id });
        }

        public List<KaynakPozisyonu> GetListPagination(PagingParams pagingParams)
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

            //columns ayrımı yapılır
            string columnsQuery = "*";
            if (pagingParams.columns.Length != 0)
            {
                columnsQuery = pagingParams.columns;
            }

            return GetListQuery($@"SELECT {columnsQuery} FROM KaynakPozisyonu where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM KaynakPozisyonu where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}

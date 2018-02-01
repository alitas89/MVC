using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpHurdaDal : DpEntityRepositoryBase<Hurda>, IHurdaDal
    {
        public List<Hurda> GetList()
        {
            return GetListQuery($"select * from Hurda where Silindi=0", new { });
        }

        public Hurda Get(int Id)
        {
            return GetQuery("select * from Hurda where HurdaID= @Id and Silindi=0", new { Id });
        }

        public int Add(Hurda hurda)
        {
            return AddQuery("insert into Hurda(BarkodKod,VarlikID,OzurKod,OzurAd,OzurTip,Tarih,Miktar,Toplam,Aciklama) values (@BarkodKod,@VarlikID,@OzurKod,@OzurAd,@OzurTip,@Tarih,@Miktar,@Toplam,@Aciklama)", hurda);
        }

        public int Update(Hurda hurda)
        {
            return UpdateQuery("update Hurda set BarkodKod=@BarkodKod,VarlikID=@VarlikID,OzurKod=@OzurKod,OzurAd=@OzurAd,OzurTip=@OzurTip,Tarih=@Tarih,Miktar=@Miktar,Toplam=@Toplam,Aciklama=@Aciklama where HurdaID=@HurdaID", hurda);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Hurda where HurdaID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Hurda set Silindi = 1 where HurdaID=@Id", new { Id });
        }
        public List<Hurda> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM Hurda where Silindi=0 {filterQuery} {orderQuery}
        OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filterVal, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            string where = "";
            if (filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                filterVal = '%' + filterVal + '%';
                where = $" where {filterCol} like @filterVal";
            }
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Hurda {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}
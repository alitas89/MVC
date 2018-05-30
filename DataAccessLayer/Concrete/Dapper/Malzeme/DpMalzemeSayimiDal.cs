using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace DataAccessLayer.Concrete.Dapper.Malzeme
{
    public class DpMalzemeSayimiDal : DpEntityRepositoryBase<MalzemeSayimi>, IMalzemeSayimiDal
    {
        public List<MalzemeSayimi> GetList()
        {
            return GetListQuery("select * from View_MalzemeSayimiDto where Silindi=0", new { });
        }

        public MalzemeSayimi Get(int Id)
        {
            return GetQuery("select * from MalzemeSayimi where MalzemeSayimiID= @Id and Silindi=0", new { Id });
        }

        public int Add(MalzemeSayimi malzemesayimi)
        {
            return AddQuery("insert into MalzemeSayimi(SayacNo,MalzemeID,AmbarID,Miktar,Tarih,Saat,Silindi) values (@SayacNo,@MalzemeID,@AmbarID,@Miktar,@Tarih,@Saat,@Silindi)", malzemesayimi);
        }

        public int Update(MalzemeSayimi malzemesayimi)
        {
            return UpdateQuery("update MalzemeSayimi set SayacNo=@SayacNo,MalzemeID=@MalzemeID,AmbarID=@AmbarID,Miktar=@Miktar,Tarih=@Tarih,Saat=@Saat,Silindi=@Silindi where MalzemeSayimiID=@MalzemeSayimiID", malzemesayimi);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from MalzemeSayimi where MalzemeSayimiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update MalzemeSayimi set Silindi = 1 where MalzemeSayimiID=@Id", new { Id });
        }

        public List<MalzemeSayimi> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM View_MalzemeSayimiDto where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM MalzemeSayimi where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
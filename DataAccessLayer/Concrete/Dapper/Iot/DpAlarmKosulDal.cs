using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Iot;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;

namespace DataAccessLayer.Concrete.Dapper.Iot
{
    public class DpAlarmKosulDal : DpEntityRepositoryBase<AlarmKosul>, IAlarmKosulDal
    {
        public List<AlarmKosul> GetList()
        {
            return GetListQuery("select * from AlarmKosul where Silindi=0", new { });
        }

        public AlarmKosul Get(int Id)
        {
            return GetQuery("select * from AlarmKosul where AlarmKosulID= @Id and Silindi=0", new { Id });
        }

        public int Add(AlarmKosul alarmkosul)
        {
            return AddQuery("insert into AlarmKosul(AlarmID,OznitelikID,KosulID,Deger,Tarih,Silindi) values (@AlarmID,@OznitelikID,@KosulID,@Deger,@Tarih,@Silindi)", alarmkosul);
        }

        public int Update(AlarmKosul alarmkosul)
        {
            return UpdateQuery("update AlarmKosul set AlarmID=@AlarmID,OznitelikID=@OznitelikID,KosulID=@KosulID,Deger=@Deger,Tarih=@Tarih,Silindi=@Silindi where AlarmKosulID=@AlarmKosulID", alarmkosul);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from AlarmKosul where AlarmKosulID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update AlarmKosul set Silindi = 1 where AlarmKosulID=@Id", new { Id });
        }

        public List<AlarmKosul> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM AlarmKosul where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM AlarmKosul where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
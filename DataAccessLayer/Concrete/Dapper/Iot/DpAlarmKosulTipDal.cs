using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Iot;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;

namespace DataAccessLayer.Concrete.Dapper.Iot
{
    public class DpAlarmKosulTipDal : DpEntityRepositoryBase<AlarmKosulTip>, IAlarmKosulTipDal
    {
        public List<AlarmKosulTip> GetList()
        {
            return GetListQuery("select * from AlarmKosulTip where Silindi=0", new { });
        }

        public AlarmKosulTip Get(int Id)
        {
            return GetQuery("select * from AlarmKosulTip where AlarmKosulTipID= @Id and Silindi=0", new { Id });
        }

        public int Add(AlarmKosulTip alarmkosultip)
        {
            return AddQuery("insert into AlarmKosulTip(Ad,Silindi) values (@Ad,@Silindi)", alarmkosultip);
        }

        public int Update(AlarmKosulTip alarmkosultip)
        {
            return UpdateQuery("update AlarmKosulTip set Ad=@Ad,Silindi=@Silindi where AlarmKosulTipID=@AlarmKosulTipID", alarmkosultip);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from AlarmKosulTip where AlarmKosulTipID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update AlarmKosulTip set Silindi = 1 where AlarmKosulTipID=@Id", new { Id });
        }

        public List<AlarmKosulTip> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM AlarmKosulTip where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM AlarmKosulTip where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
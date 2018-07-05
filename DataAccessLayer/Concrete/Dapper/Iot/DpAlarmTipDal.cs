using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Iot;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;

namespace DataAccessLayer.Concrete.Dapper.Iot
{
    public class DpAlarmTipDal : DpEntityRepositoryBase<AlarmTip>, IAlarmTipDal
    {
        public List<AlarmTip> GetList()
        {
            return GetListQuery("select * from AlarmTip where Silindi=0", new { });
        }

        public AlarmTip Get(int Id)
        {
            return GetQuery("select * from AlarmTip where AlarmTipID= @Id and Silindi=0", new { Id });
        }

        public int Add(AlarmTip alarmtip)
        {
            return AddQuery("insert into AlarmTip(Ad,Silindi) values (@Ad,@Silindi)", alarmtip);
        }

        public int Update(AlarmTip alarmtip)
        {
            return UpdateQuery("update AlarmTip set Ad=@Ad,Silindi=@Silindi where AlarmTipID=@AlarmTipID", alarmtip);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from AlarmTip where AlarmTipID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update AlarmTip set Silindi = 1 where AlarmTipID=@Id", new { Id });
        }

        public List<AlarmTip> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM AlarmTip where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM AlarmTip where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
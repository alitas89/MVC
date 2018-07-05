using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Iot;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;

namespace DataAccessLayer.Concrete.Dapper.Iot
{
    public class DpAlarmDal : DpEntityRepositoryBase<Alarm>, IAlarmDal
    {
        public List<Alarm> GetList()
        {
            return GetListQuery("select * from Alarm where Silindi=0", new { });
        }

        public Alarm Get(int Id)
        {
            return GetQuery("select * from Alarm where AlarmID= @Id and Silindi=0", new { Id });
        }

        public int Add(Alarm alarm)
        {
            return AddQuery("insert into Alarm(Ad,Aciklama,IsTipiID,AlarmTipID,Tarih,Silindi) values (@Ad,@Aciklama,@IsTipiID,@AlarmTipID,@Tarih,@Silindi)", alarm);
        }

        public int Update(Alarm alarm)
        {
            return UpdateQuery("update Alarm set Ad=@Ad,Aciklama=@Aciklama,IsTipiID=@IsTipiID,AlarmTipID=@AlarmTipID,Tarih=@Tarih,Silindi=@Silindi where AlarmID=@AlarmID", alarm);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Alarm where AlarmID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Alarm set Silindi = 1 where AlarmID=@Id", new { Id });
        }

        public List<Alarm> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM Alarm where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Alarm where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
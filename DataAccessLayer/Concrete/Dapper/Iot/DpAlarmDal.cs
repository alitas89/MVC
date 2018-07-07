using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using Dapper;
using DataAccessLayer.Abstract.Iot;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Iot;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
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
            return AddQuery("insert into Alarm(Ad,Aciklama,IsTipiID,AlarmTipID,OlusturanID,Tolerans,VarlikID,Tarih,Silindi) values (@Ad,@Aciklama,@IsTipiID,@AlarmTipID,@OlusturanID,@Tolerans,@VarlikID,@Tarih,@Silindi)", alarm);
        }

        public int Update(Alarm alarm)
        {
            return UpdateQuery("update Alarm set Ad=@Ad,Aciklama=@Aciklama,IsTipiID=@IsTipiID,AlarmTipID=@AlarmTipID," +
                               "OlusturanID=@OlusturanID,Tolerans=@Tolerans,VarlikID=@VarlikID," +
                               "Tarih=@Tarih,Silindi=@Silindi where AlarmID=@AlarmID", alarm);
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

        public List<AlarmDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<AlarmDto>().GetListDtoQuery($@"SELECT * FROM View_AlarmDto where Silindi=0 {filterQuery} {orderQuery}
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

        public int AddWithTransaction(Alarm alarm, List<AlarmKosul> listAlarmKosul)
        {
            var count = 0;

            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IDbTransaction transaction = connection.BeginTransaction();

                var strAlarmID = connection.ExecuteScalar("insert into Alarm(Ad,Aciklama,IsTipiID,AlarmTipID," +
                                                          "OlusturanID,Tolerans,VarlikID,Tarih,Silindi) values (@Ad,@Aciklama,@IsTipiID," +
                                                          "@AlarmTipID,@OlusturanID,@Tolerans,@VarlikID,@Tarih,@Silindi); " +
                                                               "SELECT CAST(SCOPE_IDENTITY() as int)", alarm, transaction);
                int.TryParse(strAlarmID + "", out int AlarmID);



                foreach (var item in listAlarmKosul)
                {
                    item.AlarmID = AlarmID;
                    count += connection.Execute("insert into AlarmKosul(AlarmID,OznitelikID,KosulID,Deger,Tarih,Silindi) values (@AlarmID,@OznitelikID,@KosulID,@Deger,@Tarih,@Silindi)", item, transaction);
                }

                transaction.Commit();
            }
            return count;
        }

        public int UpdateWithTransaction(Alarm alarm, List<AlarmKosul> listAlarmKosul)
        {
            var count = 0;

            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IDbTransaction transaction = connection.BeginTransaction();

                connection.Execute("update Alarm set Ad=@Ad,Aciklama=@Aciklama,IsTipiID=@IsTipiID," +
                                   "AlarmTipID=@AlarmTipID,OlusturanID=@OlusturanID,Tolerans=@Tolerans," +
                                   "VarlikID=@VarlikID,Tarih=@Tarih,Silindi=@Silindi where AlarmID=@AlarmID"
                    , alarm, transaction);

                connection.Execute("update AlarmKosul set Silindi = 1 where AlarmID = @AlarmID"
                    , alarm, transaction);

                foreach (var item in listAlarmKosul)
                {
                    count += connection.Execute("insert into AlarmKosul(AlarmID,OznitelikID,KosulID,Deger,Tarih,Silindi) values (@AlarmID,@OznitelikID,@KosulID,@Deger,@Tarih,@Silindi)"
                        , item, transaction);
                }

                transaction.Commit();
            }
            return count;
        }
    }
}
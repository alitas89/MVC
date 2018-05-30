using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using Dapper;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpBakimPlaniDal : DpEntityRepositoryBase<BakimPlani>, IBakimPlaniDal
    {
        public List<BakimPlani> GetList()
        {
            return GetListQuery("select * from BakimPlani where Silindi=0", new { });
        }

        public BakimPlani Get(int Id)
        {
            return GetQuery("select * from BakimPlani where BakimPlaniID= @Id and Silindi=0", new { Id });
        }

        public int Add(BakimPlani bakimplani)
        {
            return AddQuery("insert into BakimPlani(BakimPlaniTanim,ToplamBakimSuresi,ToplamIscilikSuresi,Aciklama,Silindi) values (@BakimPlaniTanim,@ToplamBakimSuresi,@ToplamIscilikSuresi,@Aciklama,@Silindi)", bakimplani);
        }

        public int Update(BakimPlani bakimplani)
        {
            return UpdateQuery("update BakimPlani set BakimPlaniTanim=@BakimPlaniTanim,ToplamBakimSuresi=@ToplamBakimSuresi,ToplamIscilikSuresi=@ToplamIscilikSuresi,Aciklama=@Aciklama,Silindi=@Silindi where BakimPlaniID=@BakimPlaniID", bakimplani);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BakimPlani where BakimPlaniID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BakimPlani set Silindi = 1 where BakimPlaniID=@Id", new { Id });
        }

        public List<BakimPlani> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM BakimPlani where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM BakimPlani where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }



        public int AddWithTransaction(BakimPlani bakimplani, List<IsAdimlari> listIsAdimlari)
        {
            var count = 0;

            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IDbTransaction transaction = connection.BeginTransaction();

                var strBakimPlaniID = connection.ExecuteScalar("insert into BakimPlani(BakimPlaniTanim,ToplamBakimSuresi,ToplamIscilikSuresi,Aciklama,Silindi) values (@BakimPlaniTanim,@ToplamBakimSuresi,@ToplamIscilikSuresi,@Aciklama,@Silindi); " +
                                                            "SELECT CAST(SCOPE_IDENTITY() as int)", bakimplani, transaction);
                int.TryParse(strBakimPlaniID + "", out int BakimPlaniID);

                foreach (var item in listIsAdimlari)
                {
                    item.BakimPlaniID = BakimPlaniID;
                    count += connection.Execute("insert into IsAdimlari(BakimPlaniID,IsAdimlariTanim,Sure,TekrarSayisi,Aciklama,Silindi) values (@BakimPlaniID,@IsAdimlariTanim,@Sure,@TekrarSayisi,@Aciklama,@Silindi)", item, transaction);
                }

                transaction.Commit();
            }
            return count;
        }

        public int UpdateWithTransaction(BakimPlani bakimplani, List<IsAdimlari> listIsAdimlari)
        {
            var count = 0;

            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IDbTransaction transaction = connection.BeginTransaction();

                connection.Execute("update BakimPlani set BakimPlaniTanim = @BakimPlaniTanim, ToplamBakimSuresi = @ToplamBakimSuresi, ToplamIscilikSuresi = @ToplamIscilikSuresi, Aciklama = @Aciklama, Silindi = @Silindi where BakimPlaniID = @BakimPlaniID"
                    , bakimplani, transaction);


                foreach (var item in listIsAdimlari)
                {
                    count += connection.Execute("update IsAdimlari set BakimPlaniID=@BakimPlaniID,IsAdimlariTanim=@IsAdimlariTanim,Sure=@Sure,TekrarSayisi=@TekrarSayisi,Aciklama=@Aciklama,Silindi=@Silindi where IsAdimlariID=@IsAdimlariID"
                         , item, transaction);
                }

                transaction.Commit();
            }
            return count;
        }

    }
}
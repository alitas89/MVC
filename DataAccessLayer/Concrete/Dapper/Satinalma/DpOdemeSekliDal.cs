using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Satinalma;
using System.Collections.Generic;
using Core.Utilities.Dal;

namespace DataAccessLayer.Concrete.Dapper.Satinalma
{
    public class DpOdemeSekliDal : DpEntityRepositoryBase<OdemeSekli>, IOdemeSekliDal
    {
        public List<OdemeSekli> GetList()
        {
            return GetListQuery("select * from OdemeSekli where Silindi=0", new { });
        }

        public OdemeSekli Get(int Id)
        {
            return GetQuery("select * from OdemeSekli where OdemeSekliID= @Id and Silindi=0", new { Id });
        }

        public int Add(OdemeSekli odemesekli)
        {
            return AddQuery("insert into OdemeSekli(Kod,Ad,GunSayisi,Aciklama,VarsayilanDeger,Silindi) values (@Kod,@Ad,@GunSayisi,@Aciklama,@VarsayilanDeger,@Silindi)", odemesekli);
        }

        public int Update(OdemeSekli odemesekli)
        {
            return UpdateQuery("update OdemeSekli set Kod=@Kod,Ad=@Ad,GunSayisi=@GunSayisi,Aciklama=@Aciklama,VarsayilanDeger=@VarsayilanDeger,Silindi=@Silindi where OdemeSekliID=@OdemeSekliID", odemesekli);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from OdemeSekli where OdemeSekliID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update OdemeSekli set Silindi = 1 where OdemeSekliID=@Id", new { Id });
        }

        public List<OdemeSekli> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM OdemeSekli where Silindi=0 {filterQuery} {orderQuery}
                            OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM OdemeSekli where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
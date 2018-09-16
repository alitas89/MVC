using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.Dapper.Sistem
{
    public class DpDosyaDal : DpEntityRepositoryBase<Dosya>, IDosyaDal
    {
        public List<Dosya> GetList()
        {
            return GetListQuery("select * from Dosya where Silindi=0", new { });
        }

        public Dosya Get(int Id)
        {
            return GetQuery("select * from Dosya where DosyaID= @Id and Silindi=0", new { Id });
        }

        public int Add(Dosya dosya)
        {
            return AddQuery("insert into Dosya(BagliID,Ad,Path,DosyaModul,YuklenmeTarih,Silindi) values (@BagliID,@Ad,@Path,@DosyaModul,@YuklenmeTarih,@Silindi)", dosya);
        }

        public int Update(Dosya dosya)
        {
            return UpdateQuery("update Dosya set BagliID=@BagliID, Ad=@Ad,Path=@Path,DosyaModul=@DosyaModul,YuklenmeTarih=@YuklenmeTarih,Silindi=@Silindi where DosyaID=@DosyaID", dosya);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Dosya where DosyaID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Dosya set Silindi = 1 where DosyaID=@Id", new { Id });
        }

        public List<Dosya> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM Dosya where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Dosya where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

        public List<Dosya> GetListByBagliID(int id)
        {
            return GetListQuery("select * from Dosya where BagliID= @id and Silindi=0", new { id });
        }
    }
}

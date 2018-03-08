using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace DataAccessLayer.Concrete.Dapper.Personel
{
    public class DpMesaiDal : DpEntityRepositoryBase<Mesai>, IMesaiDal
    {
        public List<Mesai> GetList()
        {
            return GetListQuery("select * from Mesai where Silindi=0", new { });
        }

        public Mesai Get(int Id)
        {
            return GetQuery("select * from Mesai where MesaiID= @Id and Silindi=0", new { Id });
        }

        public int Add(Mesai mesai)
        {
            return AddQuery("insert into Mesai(Kod,Ad,UcretCarpani,MesaiTuruID,Silindi) values (@Kod,@Ad,@UcretCarpani,@MesaiTuruID,@Silindi)", mesai);
        }

        public int Update(Mesai mesai)
        {
            return UpdateQuery("update Mesai set Kod=@Kod,Ad=@Ad,UcretCarpani=@UcretCarpani,MesaiTuruID=@MesaiTuruID,Silindi=@Silindi where MesaiID=@MesaiID", mesai);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Mesai where MesaiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Mesai set Silindi = 1 where MesaiID=@Id", new { Id });
        }

        public List<Mesai> GetListPagination(PagingParams pagingParams)
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

            //columns ayrımı yapılır
            string columnsQuery = "*";
            if (pagingParams.columns.Length != 0)
            {
                columnsQuery = pagingParams.columns;
            }

            return GetListQuery($@"SELECT {columnsQuery} FROM Mesai where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filterVal, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            string filter = "";
            if (filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                filterVal = '%' + filterVal + '%';
                filter = $"and {filterCol} like @filterVal";
            }
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Mesai where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}

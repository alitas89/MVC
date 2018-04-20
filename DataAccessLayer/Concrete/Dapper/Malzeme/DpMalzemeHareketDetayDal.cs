using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.DtoModel.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;
using System.Collections.Generic;

namespace DataAccessLayer.Concrete.Dapper.Malzeme
{
    public class DpMalzemeHareketDetayDal : DpEntityRepositoryBase<MalzemeHareketDetay>, IMalzemeHareketDetayDal
    {
        public List<MalzemeHareketDetay> GetList()
        {
            return GetListQuery("select * from MalzemeHareketDetay where Silindi=0", new { });
        }

        public MalzemeHareketDetay Get(int Id)
        {
            return GetQuery("select * from MalzemeHareketDetay where MalzemeHareketDetayID= @Id and Silindi=0", new { Id });
        }

        public int Add(MalzemeHareketDetay malzemehareketdetay)
        {
            return AddQuery("insert into MalzemeHareketDetay(MalzemeHareketFisNo,MalzemeID,Miktar,Silindi) values (@MalzemeHareketFisNo,@MalzemeID,@Miktar,@Silindi)", malzemehareketdetay);
        }

        public int Update(MalzemeHareketDetay malzemehareketdetay)
        {
            return UpdateQuery("update MalzemeHareketDetay set MalzemeHareketFisNo=@MalzemeHareketFisNo,MalzemeID=@MalzemeID,Miktar=@Miktar,Silindi=@Silindi where MalzemeHareketDetayID=@MalzemeHareketDetayID", malzemehareketdetay);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from MalzemeHareketDetay where MalzemeHareketDetayID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update MalzemeHareketDetay set Silindi = 1 where MalzemeHareketDetayID=@Id", new { Id });
        }

        public List<MalzemeHareketDetay> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM MalzemeHareketDetay where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM MalzemeHareketDetay where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

        public List<MalzemeHareketDetayDto> GetListByFisNo(int MalzemeHareketFisNo)
        {
            return new DpDtoRepositoryBase<MalzemeHareketDetayDto>().GetListDtoQuery("select d.MalzemeHareketDetayID, d.MalzemeHareketFisNo, d.MalzemeID," +
                                "d.Miktar, m.Kod, m.Ad " +
                                "from MalzemeHareketDetay d left outer join Malzeme m on d.MalzemeID = m.MalzemeID " +
                                "where d.Silindi = 0 and d.MalzemeHareketFisNo = @MalzemeHareketFisNo",
                new { MalzemeHareketFisNo });
        }
    }
}

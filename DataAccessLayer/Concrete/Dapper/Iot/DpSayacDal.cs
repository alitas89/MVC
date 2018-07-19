using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Iot;
using EntityLayer.ComplexTypes.DtoModel.Iot;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.Dapper.Iot
{
    public class DpSayacDal : DpEntityRepositoryBase<Sayac>, ISayacDal
    {
        public int Add(Sayac obj)
        {
            throw new NotImplementedException();
        }

        public int AddSayacKomut(SayacKomut sayacKomut)
        {
            return AddQuery("insert into [VOLUMETRIK].[dbo].[komut_abone](sayacNo,komut,parametre) values (@SayacNo,@Komut,@Parametre)", sayacKomut);
        }

        public int Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public int DeleteSoft(int Id)
        {
            throw new NotImplementedException();
        }

        public Sayac Get(int Id)
        {
            throw new NotImplementedException();
        }

        public int GetCount(string filter = "")
        {
            throw new NotImplementedException();
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM [View_SayacDto] where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

        public int GetCountDtoByModemSeriNo(string modemserino = "", string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM [View_SayacDto] where Silindi = 0 and modemSeriNo=@modemserino
                                            {filterQuery} ", new { modemserino }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<Sayac> GetList()
        {
            throw new NotImplementedException();
        }


        /*public List<SayacDto> GetListByModemSeriNo(string ModemSeriNo)
        {
            return new DpDtoRepositoryBase<SayacDto>().GetListDtoQuery(@"SELECT * FROM [MVC].[dbo].[View_SayacDto]" +
                                  " where modemSeriNo = @ModemSeriNo and Silindi = 0", new { ModemSeriNo });
        }*/

        public List<Sayac> GetListPagination(PagingParams pagingParams)
        {
            throw new NotImplementedException();
        }

        public List<SayacDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<SayacDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_SayacDto where Silindi=0 {filterQuery} {orderQuery}
                                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public List<SayacDto> GetListPaginationDtoByModemSeriNo(PagingParams pagingParams, string modemserino)
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

            return new DpDtoRepositoryBase<SayacDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_SayacDto where modemSeriNo=@modemserino and Silindi=0 {filterQuery} {orderQuery}
                                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { modemserino, pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int Update(Sayac obj)
        {
            throw new NotImplementedException();
        }
    }
}

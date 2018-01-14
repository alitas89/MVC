using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpModelDal : DpEntityRepositoryBase<Model>, IModelDal
    {
        public List<Model> GetList()
        {
            return GetListQuery($"select * from Model where Silindi=0", new { });
        }

        public Model Get(int Id)
        {
            return GetQuery("select * from Model where ModelID= @Id and Silindi=0", new { Id });
        }

        public int Add(Model model)
        {
            return AddQuery("insert Model(Kod,Ad,Aciklama) values (@Kod,@Ad,@Aciklama)", model);
        }

        public int Update(Model model)
        {
            return UpdateQuery("update Model set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama where ModelID=@ModelID", model);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Model where ModelID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Model set Silindi = 1 where ModelID=@Id", new { Id });
        }

        public List<ModelDto> GetListDto()
        {
            return new DpDtoRepositoryBase<ModelDto>().GetListDtoQuery("select M.*, MA.Ad as MarkaAd from Model M inner join Marka MA on MA.MarkaID = M.MarkaID", new { });
        }
    }
}
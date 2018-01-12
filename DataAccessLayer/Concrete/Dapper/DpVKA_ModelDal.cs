using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpVK_ModelDal : DpEntityRepositoryBase<VK_Model>, IModelDal
    {
        public List<VK_Model> GetList()
        {
            return GetListQuery($"select * from Model where Silindi=0", new { });
        }

        public VK_Model Get(int Id)
        {
            return GetQuery("select * from Model where ModelID= @Id and Silindi=0", new { Id });
        }

        public int Add(VK_Model model)
        {
            return AddQuery("insert Model(Kod,Ad,Aciklama) values (@Kod,@Ad,@Aciklama)", model);
        }

        public int Update(VK_Model model)
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
    }
}
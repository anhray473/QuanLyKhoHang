using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class VIEW_USER_IN_GROUP
    {
        Entities db;
        public VIEW_USER_IN_GROUP()
        {
            db = Entities.CreateEntities();

        }
        public List<tb_SYS_USER> getGroupByUser(string macty, string madvi, int idUS)
        {
            List<tb_SYS_USER> lstGroup = new List<tb_SYS_USER>();
            List<V_USER_IN_GROUP> lst = db.V_USER_IN_GROUP.Where(x=>x.ThanhVien == idUS && x.MaCTy ==macty &&x.MaDVi==madvi).ToList();
            tb_SYS_USER u;
            foreach (var item in lst)
            {
                u = new tb_SYS_USER();
                u = db.tb_SYS_USER.FirstOrDefault(x=>x.IDUser == item.Group);
                
                lstGroup.Add(u);

            }
            return lstGroup;
        }
        public List<tb_SYS_USER> getGroupByDonVi(string macty, string madvi)
        {
            return db.tb_SYS_USER.Where(x=> x.MaCTy ==macty && x.MaDVi==madvi && x.Isgroup == true).ToList();
        }
        public List<V_USER_IN_GROUP> getUserInGroup(string madvi, string macty, int idgroup)
        {
            return db.V_USER_IN_GROUP.Where(x => x.MaDVi == madvi && x.MaCTy == macty && x.Group == idgroup && x.Isgroup == false && x.Disabled == false).ToList(); 
        }
        public bool checkGroupByUser(int idUser, int idGroup)
        {
            var gr = db.tb_SYS_GROUP.FirstOrDefault(x => x.ThanhVien == idUser && x.Group == idGroup);
            if (gr == null)
            {
                return false;

            }
            else
                return true;
        }
    }
}

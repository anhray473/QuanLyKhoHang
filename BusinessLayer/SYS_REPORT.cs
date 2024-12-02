using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SYS_REPORT
    {
        Entities db;
        public SYS_REPORT() 
        { 
            db=Entities.CreateEntities();
        }
        public tb_SYS_REPORT getItem(int rep_code)
        {
            return db.tb_SYS_REPORT.FirstOrDefault(x=>x.RepCode==rep_code);

        }
        public List<tb_SYS_REPORT> getList()
        {
            return db.tb_SYS_REPORT.ToList();
        }
        public List<tb_SYS_REPORT> getListByRight(List<tb_SYS_RIGHT_REP> list) 
        { 
            List<int> rep = list.Select(ls => ls.RepCode).ToList();
            return db.tb_SYS_REPORT.Where(x=>rep.Contains(x.RepCode)).OrderBy(x=>x.RepCode).ToList();
        }
    }
}

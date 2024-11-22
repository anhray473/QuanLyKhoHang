using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SYS_FUNC
    {
        Entities db;

        public SYS_FUNC() 
        {
            db = Entities.CreateEntities();
        }

        public List<tb_SYS_FUNC> getParent()
        {
            return db.tb_SYS_FUNC.Where(x => x.Isgroup == true && x.Menu == true).OrderBy(s => s.SORT).ToList();
        }
        public List<tb_SYS_FUNC> getChild(string parent) 
        {
            return db.tb_SYS_FUNC.Where(x => x.Isgroup == false && x.Menu == true && x.Parent == parent).OrderBy(s => s.SORT).ToList();        
        }
    }
}

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

        public List<DataLayer.SYS_FUNC> getParent()
        {
            return db.SYS_FUNC.Where(x => x.Isgroup == true && x.Menu == true).OrderBy(s => s.SORT).ToList();
        }
        public List<DataLayer.SYS_FUNC> getChild(string parent) 
        {
            return db.SYS_FUNC.Where(x => x.Isgroup == false && x.Menu == true && x.Parent == parent).OrderBy(s => s.SORT).ToList();        
        }
    }
}

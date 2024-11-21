using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class XUATXU
    {
        Entities db;
        public XUATXU() 
        { 
            db = Entities.CreateEntities();
        }
        public DataLayer.XUATXU getItem(int id)
        {
            return db.XUATXUs.FirstOrDefault(x => x.ID == id);
        }

        public List<DataLayer.XUATXU> getAll()
        {
            return db.XUATXUs.ToList();
        }

        public List<DataLayer.XUATXU> getAll(int id)
        {
            return db.XUATXUs.Where(x => x.ID == id).ToList();
        }
        public void add(DataLayer.XUATXU xx)
        {
            try
            {
                db.XUATXUs.Add(xx);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }

        public void update(DataLayer.XUATXU xx)
        {
            DataLayer.XUATXU _xx = db.XUATXUs.FirstOrDefault(x => x.ID == xx.ID);
            _xx.ID = xx.ID;
            _xx.Ten = xx.Ten;
            _xx.Disabled = xx.Disabled;
            try
            {

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void delete(int id)
        {
            DataLayer.XUATXU _xx = db.XUATXUs.FirstOrDefault(x => x.ID == id);
            _xx.Disabled = true;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
    
    }
   
}

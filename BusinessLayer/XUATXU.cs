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
        public tb_XUATXU getItem(int id)
        {
            return db.tb_XUATXU.FirstOrDefault(x => x.ID == id);
        }

        public List<tb_XUATXU> getAll()
        {
            return db.tb_XUATXU.ToList();
        }

        public List<tb_XUATXU> getXXNoDis()
        {
            return db.tb_XUATXU.Where(x=> x.Disabled==false).ToList();
        }

        public List<tb_XUATXU> getAll(int id)
        {
            return db.tb_XUATXU.Where(x => x.ID == id).ToList();
        }
        public void add(tb_XUATXU xx)
        {
            try
            {
                db.tb_XUATXU.Add(xx);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }

        public void update(tb_XUATXU xx)
        {
            tb_XUATXU _xx = db.tb_XUATXU.FirstOrDefault(x => x.ID == xx.ID);
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
            tb_XUATXU _xx = db.tb_XUATXU.FirstOrDefault(x => x.ID == id);
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

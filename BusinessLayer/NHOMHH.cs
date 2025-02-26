using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class NHOMHH
    {
        Entities db;

        public NHOMHH()

        {
            db = Entities.CreateEntities();
        }

        public tb_NHOMHH getItem(int maNhom)
        {
            return db.tb_NHOMHH.FirstOrDefault(x => x.IDNhom == maNhom);
        }

        public List<tb_NHOMHH> getAll()
        {
            return db.tb_NHOMHH.ToList();
        }
        public List<tb_NHOMHH> getNhomHHNoDis()
        {
            return db.tb_NHOMHH.Where(x => x.Disabled == false).ToList();
        }
        public void add(tb_NHOMHH nhom)
        {
            try
            {
                db.tb_NHOMHH.Add(nhom);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }

        }
        public void update(tb_NHOMHH nhh)
        {
            tb_NHOMHH _nhh = db.tb_NHOMHH.FirstOrDefault(x => x.IDNhom == nhh.IDNhom);
            _nhh.TenNhom = nhh.TenNhom;
            _nhh.Disabled = nhh.Disabled;
            _nhh.Mota = nhh.Mota;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void delete(int idn)
        {
            tb_NHOMHH _nhh = db.tb_NHOMHH.FirstOrDefault(x => x.IDNhom == idn);
            _nhh.Disabled = true;
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

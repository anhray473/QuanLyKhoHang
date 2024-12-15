using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class NHACUNGCAP
    {
        Entities db;
        public NHACUNGCAP()
        {
            db =Entities.CreateEntities();
        }
        public tb_NHACUNGCAP getItem(string maNCC)
        {
            return db.tb_NHACUNGCAP.FirstOrDefault(x => x.MaNCC == maNCC);
        }
        public List<tb_NHACUNGCAP> getAll() 
        {
            return db.tb_NHACUNGCAP.ToList();
        }
        public void add(tb_NHACUNGCAP ncc)
        {
            try
            {
                db.tb_NHACUNGCAP.Add(ncc);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }

        }
        public void update(tb_NHACUNGCAP ncc)
        {
            tb_NHACUNGCAP _ncc = db.tb_NHACUNGCAP.FirstOrDefault(x => x.MaNCC == ncc.MaNCC);
            _ncc.TenNCC = ncc.TenNCC;
            _ncc.SDT = ncc.SDT;
            _ncc.Email = ncc.Email;
            _ncc.Fax = ncc.Fax;
            _ncc.DiaChi = ncc.DiaChi;
            _ncc.Disabled = ncc.Disabled;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void delete(string mancc)
        {
            tb_NHACUNGCAP _ncc = db.tb_NHACUNGCAP.FirstOrDefault(x => x.MaNCC == mancc);
            _ncc.Disabled = true;
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

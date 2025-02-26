using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CONGTY
    {
        Entities db;

        public CONGTY() 
        {
            db = Entities.CreateEntities();        
        }

        public tb_CONGTY getItem(string maCTy)
        {
            return db.tb_CONGTY.FirstOrDefault(x=>x.MaCTy==maCTy);
        }

        public List<tb_CONGTY> getAll() 
        {
            return db.tb_CONGTY.ToList();
        }
        public List<tb_CONGTY> getCtyNoDis()
        {
            return db.tb_CONGTY.Where(x => x.Disabled == false).ToList();
        }

        public void add(tb_CONGTY cty)
        {
            try
            {
                db.tb_CONGTY.Add(cty);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
            
        }
        public void update(tb_CONGTY cty)
        {
            tb_CONGTY _cty = db.tb_CONGTY.FirstOrDefault(x => x.MaCTy == cty.MaCTy);
            _cty.TenCTy = cty.TenCTy;
            _cty.SDT = cty.SDT;
            _cty.Email = cty.Email;
            _cty.Fax = cty.Fax;
            _cty.DiaChi = cty.DiaChi;
            _cty.Disabled = cty.Disabled;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void delete(string maCTy)
        {
            tb_CONGTY _cty = db.tb_CONGTY.FirstOrDefault(x => x.MaCTy == maCTy);
            _cty.Disabled = true;
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

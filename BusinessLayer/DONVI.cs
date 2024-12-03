using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class DONVI
    {
        Entities db;
        public DONVI()
        {
            db = Entities.CreateEntities();
        }

        public tb_DONVI getItem(string maDVi)
        {
            return db.tb_DONVI.FirstOrDefault(x => x.MaDVi == maDVi);
        }

        public List<tb_DONVI> getAll() 
        { 
            return db.tb_DONVI.ToList();
        }

        public List<tb_DONVI> getAllCTy(string maCTy)
        {
            return db.tb_DONVI.Where(x => x.MaCTy == maCTy).ToList();
        }

        public List<tb_DONVI> getKhoByCty(string maCTy)
        {
            return db.tb_DONVI.Where(x => x.MaCTy == maCTy && x.Kho == true).ToList();
        }
        public List<tb_DONVI> getDonViByCty(string maCTy, bool kho)
        {
            return db.tb_DONVI.Where(x => x.MaCTy == maCTy && x.Kho == kho).ToList();
        }
        public void add (tb_DONVI dvi) 
        {
            try
            {
                db.tb_DONVI.Add(dvi);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu"+ ex.Message);
            }
        }

        public void update(tb_DONVI dvi)
        {
            tb_DONVI _dvi = db.tb_DONVI.FirstOrDefault(x => x.MaDVi == dvi.MaDVi);
            _dvi.MaCTy = dvi.MaCTy;
            _dvi.TenDVi = dvi.TenDVi;
            _dvi.SDT = dvi.SDT;
            _dvi.Fax = dvi.Fax;
            _dvi.Email = dvi.Email;
            _dvi.DiaChi = dvi.DiaChi;
            _dvi.Disabled = dvi.Disabled;
            _dvi.Kho = dvi.Kho;
            _dvi.KyHieu = dvi.KyHieu;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void delete(string maDVi)
        {
            tb_DONVI _dvi= db.tb_DONVI.FirstOrDefault(x => x.MaDVi==maDVi);
            _dvi.Disabled = true;
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

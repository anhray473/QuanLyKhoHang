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

        public DataLayer.DONVI getItem(string maDVi)
        {
            return db.DONVIs.FirstOrDefault(x => x.MaDVi == maDVi);
        }

        public List<DataLayer.DONVI> getAll() 
        { 
            return db.DONVIs.ToList();
        }

        public List<DataLayer.DONVI> getAll(string maCTy)
        {
            return db.DONVIs.Where(x => x.MaCTy == maCTy).ToList();
        }

        public void add (DataLayer.DONVI dvi) 
        {
            try
            {
                db.DONVIs.Add(dvi);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu"+ ex.Message);
            }
        }

        public void update(DataLayer.DONVI dvi)
        {
            DataLayer.DONVI _dvi = db.DONVIs.FirstOrDefault(x => x.MaDVi == dvi.MaDVi);
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
            DataLayer.DONVI _dvi= db.DONVIs.FirstOrDefault(x => x.MaDVi==maDVi);
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

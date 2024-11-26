using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CHUNGTU
    {
        Entities db;
        public CHUNGTU() { 
            db = Entities.CreateEntities();
        }
        public tb_CHUNGTU getItem(Guid id)
        {
            return db.tb_CHUNGTU.FirstOrDefault(x => x.ID == id);
        }
        public List<tb_CHUNGTU> getList(int lct, DateTime startDate, DateTime endDate, string kho)
        {
            return db.tb_CHUNGTU.Where(x => x.LCT == lct
                      && x.Ngay >= startDate
                      && x.Ngay < endDate
                      && x.MaDVi == kho)
             .ToList();
        }
        public tb_CHUNGTU add(tb_CHUNGTU chungtu)
        {
            try
            {
                db.tb_CHUNGTU.Add(chungtu);
                db.SaveChanges();
                return chungtu;
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }

        }
        public tb_CHUNGTU update(tb_CHUNGTU chungtu)
        {
            tb_CHUNGTU _chungtu = db.tb_CHUNGTU.FirstOrDefault(x => x.ID == chungtu.ID);
            _chungtu.SCT = chungtu.SCT;
            _chungtu.Ngay = chungtu.Ngay;
            _chungtu.SCT2 = chungtu.SCT2;
            _chungtu.Ngay2 = chungtu.Ngay2;
            _chungtu.SoLuong = chungtu.SoLuong;
            _chungtu.TongTien = chungtu.TongTien;
            _chungtu.GhiChu = chungtu.GhiChu;
            _chungtu.MaCTy = chungtu.MaCTy;
            _chungtu.MaDVi = chungtu.MaDVi;
            _chungtu.MaDVi2 = chungtu.MaDVi2;
            _chungtu.TrangThai = chungtu.TrangThai;
            _chungtu.NguoiTao = chungtu.NguoiTao;
            _chungtu.NgayTao = chungtu.NgayTao;
            _chungtu.NgaySua = chungtu.NgaySua;
            _chungtu.NguoiSua = chungtu.NguoiSua;
            _chungtu.NguoiXoa = chungtu.NguoiXoa;
            _chungtu.NgayXoa = chungtu.NgayXoa;
            
            try
            {
                db.SaveChanges();
                return _chungtu;
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void delete(Guid id, int lct)
        {
            tb_CHUNGTU _chungtu = db.tb_CHUNGTU.FirstOrDefault(x => x.ID == id);
            _chungtu.NguoiXoa = 1;
            _chungtu.NgayXoa = DateTime.Now;
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

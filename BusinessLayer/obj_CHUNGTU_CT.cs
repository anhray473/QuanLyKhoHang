using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class obj_CHUNGTU_CT
    {
        public System.Guid IDCT { get; set; }
        public Nullable<System.Guid> ID { get; set; }
        public string Code { get; set; }
        public string TenHang { get; set; }
        public string DVT { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<double> DonGia { get; set; }
        public Nullable<double> ThanhTien { get; set; }
        public Nullable<System.DateTime> Ngay { get; set; }
        public Nullable<int> STT { get; set; }
    }
}

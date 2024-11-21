using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SYS_SEQUENCE
    {
        Entities db;
        public SYS_SEQUENCE()
        {
            db = Entities.CreateEntities();
        }
        public DataLayer.SYS_SEQUENCE getItem(string seqname)
        {
            return db.SYS_SEQUENCE.FirstOrDefault(x=>x.SEQNAME==seqname);
        }
        public void add(DataLayer.SYS_SEQUENCE sequence) 
        {
            try
            {
                db.SYS_SEQUENCE.Add(sequence);
                db.SaveChanges();
            }
            catch (Exception ex) 
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public void update(DataLayer.SYS_SEQUENCE sequence)
        {
            var seq = db.SYS_SEQUENCE.FirstOrDefault(x=>x.SEQNAME ==sequence.SEQNAME );
            seq.SEQVUALE = sequence.SEQVUALE + 1;
            try
            {
                db.SYS_SEQUENCE.Add(sequence);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }

    }
}

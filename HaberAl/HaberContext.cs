using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberAl
{
    public class HaberContext : DbContext
    {
        public virtual DbSet<Kategori> Kategoriler { get; set; }
        public virtual DbSet<Haber> Haberler { get; set; }
        public virtual DbSet<Kaynak> Kaynaklar { get; set; }

    }
}

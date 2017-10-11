using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberAl
{
    [Table("tblHaber")]
    public class Haber
    {
        [Key]
        public int HaberID { get; set; }

        [Column(TypeName ="nvarchar")]
        [StringLength(500)]
        public string Baslik { get; set; }

        [Column(TypeName ="nvarchar")]
        [StringLength(2000)]
        public string Aciklama { get; set; }

        public DateTime YayinTarih { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(500)]
        public string ResimURL { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(500)]
        public string HaberLink { get; set; }
        public DateTime Tarih { get; set; }

        [ForeignKey("Kat")] //Parantez içerisndeki ifade değişken.KategoriID ye bağlı olanlarla ilgili işlem.
        public int KategoriID { get; set; }

        public virtual Kategori Kat { get; set; }

        public Haber()
        {
            Tarih = DateTime.Now;
        }
    }
}

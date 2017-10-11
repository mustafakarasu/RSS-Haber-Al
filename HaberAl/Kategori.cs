using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberAl
{
    [Table("tblKategori")]
    public class Kategori
    {
        [Key]
        public int KategoriID { get; set; }

        [Column(TypeName ="nvarchar")]
        [StringLength(250)]
        public string KategoriAdi { get; set; }

        public virtual List<Haber> Haberler { get; set; }

        //Genelde ForeignKey tanımlanınca diğer tabloda List oluşturulur.
        //virtual asenkron olarak hız kazandırıyor.
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberAl
{
    [Table("tblKaynak")]
    public class Kaynak
    {
        [Key]
        public int KaynakID { get; set; }

        [Column(TypeName ="nvarchar")]
        [StringLength(500)]
        [Required]
        public string KaynakURL { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime SonTarama { get; set; }
    }
}

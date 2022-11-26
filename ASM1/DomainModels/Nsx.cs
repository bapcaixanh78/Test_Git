using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ASM1.DomainModels
{
    [Table("NSX")]
    [Index(nameof(Ma), Name = "UQ__NSX__3214CC9E8040625B", IsUnique = true)]
    public partial class Nsx
    {
        public Nsx()
        {
            ChiTietSps = new HashSet<ChiTietSp>();
        }

        [Key]
        public Guid Id { get; set; }
        [StringLength(20)]
        public string Ma { get; set; }
        [StringLength(30)]
        public string Ten { get; set; }

        [InverseProperty(nameof(ChiTietSp.IdNsxNavigation))]
        public virtual ICollection<ChiTietSp> ChiTietSps { get; set; }
    }
}

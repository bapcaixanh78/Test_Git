using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ASM1.DomainModels
{
    [Table("ChucVu")]
    [Index(nameof(Ma), Name = "UQ__ChucVu__3214CC9E0E3CF5EE", IsUnique = true)]
    public partial class ChucVu
    {
        public ChucVu()
        {
            NhanViens = new HashSet<NhanVien>();
        }

        [Key]
        public Guid Id { get; set; }
        [StringLength(20)]
        public string Ma { get; set; }
        [StringLength(50)]
        public string Ten { get; set; }

        [InverseProperty(nameof(NhanVien.IdCvNavigation))]
        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}

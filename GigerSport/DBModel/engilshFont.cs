namespace GigerSport.DBModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("engilshFont")]
    public partial class engilshFont
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public engilshFont()
        {
            orderDetail = new HashSet<orderDetail>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int engilshFontId { get; set; }

        [Required]
        [StringLength(50)]
        public string engilshFontName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orderDetail> orderDetail { get; set; }
    }
}

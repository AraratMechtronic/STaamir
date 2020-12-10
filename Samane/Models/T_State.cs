namespace Samane
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_State
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_State()
        {
            T_City = new HashSet<T_City>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int State_ID { get; set; }

        [StringLength(255)]
        public string State_Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_City> T_City { get; set; }
    }
}

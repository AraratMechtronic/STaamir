namespace Samane
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int City_Code { get; set; }

        public int? State_ID { get; set; }

        [StringLength(255)]
        public string City_Name { get; set; }

        public virtual T_State T_State { get; set; }
    }
}

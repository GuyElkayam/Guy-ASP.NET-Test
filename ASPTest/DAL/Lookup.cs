namespace ASPTest.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Lookup")]
    public partial class Lookup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Text { get; set; }

        [StringLength(50)]
        public string Value { get; set; }

        public int LangID { get; set; }
    }
}

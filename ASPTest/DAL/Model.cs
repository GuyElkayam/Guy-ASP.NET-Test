namespace ASPTest.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=ModelDB")
        {
        }

        public virtual DbSet<Lookup> Lookup { get; set; }
        public virtual DbSet<SelectedItems> SelectedItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

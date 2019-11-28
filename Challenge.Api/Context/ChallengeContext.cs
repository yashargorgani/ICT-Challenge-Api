namespace Challenge.Api.Context
{
    using Challenge.Api.Models;
    using System.Data.Entity;

    public class ChallengeContext : DbContext
    {
        // Your context has been configured to use a 'ChallengeContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Challenge.Api.Context.ChallengeContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ChallengeContext' 
        // connection string in the application configuration file.
        public ChallengeContext()
            : base("name=ChallengeContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
    }
}
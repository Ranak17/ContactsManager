using ContactsManager.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ContactsManager.Core.Domain.Entities;
using System.Text.Json;
namespace ContactsManager.Infrastructure.DBContext
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {

        public ApplicationDBContext(DbContextOptions options) : base(options)
        { }

        //name of dbcontext should be plural - Convention strongly recommended
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Person>().ToTable("Persons");



            //Seed to Countries
            string countriesJson = File.ReadAllText("countries.json");
            List<Country>? countries = JsonSerializer.Deserialize<List<Country>>(countriesJson);
            foreach (Country country in countries)
            {
                modelBuilder.Entity<Country>().HasData(country); // Seeding the data
            }

            //Seed to Persons
            string personsJson = File.ReadAllText("persons.json");
            List<Person> persons = JsonSerializer.Deserialize<List<Person>>(personsJson);
            foreach (Person person in persons)
            {
                modelBuilder.Entity<Person>().HasData(person);
            }

            //Fluent APi
            modelBuilder.Entity<Person>().Property(temp => temp.TIN)
                .HasColumnName("TaxIdentificationNumber")
                .HasColumnType("varchar(8)")
                .HasDefaultValue("ABC123");
            //code for unique value in ITN Column
          /*  modelBuilder.Entity<Person>().HasIndex(temp => temp.TIN)
                .IsUnique();*/

            //putting constratints on column values  - TODO
            



        }
    }
}

using System;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class ApplicationDbContext: IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbSet<Contact> Contacts { get; set; }= default!;
        public DbSet<ContactType> ContactTypes { get; set; }= default!;
        public DbSet<Family> Families { get; set; }= default!;
        public DbSet<Gender> Genders { get; set; }= default!;
        public DbSet<Person> Persons { get; set; } = default!;
        public DbSet<Relationships> Relationships { get; set; }= default!;
        public DbSet<RelationshipTypes> RelationshipTypes { get; set; }= default!;
        public DbSet<RelationshipRoles> RelationshipRoles { get; set; }= default!;
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
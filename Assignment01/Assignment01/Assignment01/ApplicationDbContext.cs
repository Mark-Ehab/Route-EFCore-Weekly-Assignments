using Assignment01.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment01;

public class ApplicationDbContext : DbContext
{
    /* DbSets */
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Category> Categories { get; set; }

    /* Methods */
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.; Database=ReadMoreBooksDb; Trusted_Connection=True; TrustServerCertificate=True;");
    }
}
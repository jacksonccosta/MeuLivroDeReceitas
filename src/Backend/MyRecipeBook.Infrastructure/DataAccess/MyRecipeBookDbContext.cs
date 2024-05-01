﻿using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain;

namespace MyRecipeBook.Infrastructure;

public class MyRecipeBookDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyRecipeBookDbContext).Assembly);
    }
}
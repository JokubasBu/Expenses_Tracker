﻿namespace ExpensesTracker.Server.Data
{
    public class DataContext : DbContext // DbContext - used to query from a database and group together changes
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) // constructor
        {

        }

        public DbSet<MonthlyExp> MonthlyExps { get; set; } // DbSet represents the collection of all entities in the context, or that can be queried from the database

        public DbSet<Category> Categories { get; set; }
    }
}
﻿using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class InsuranceDbContext: DbContext
    {
        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Token> Tokens { get; set; }
    }
}
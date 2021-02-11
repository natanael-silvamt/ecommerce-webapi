using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Models.Dto;

namespace Ecommerce.Models.EntityFrameWork
{
    public partial class EntityFrameWorkDbContext : DbContext
    {
        public EntityFrameWorkDbContext(DbContextOptions<EntityFrameWorkDbContext> options) : base(options)
        {}
        
        public virtual DbSet<Product> Products { get; set; }
        
        public virtual DbSet<BasketItem> BasketItems { get; set; }
    }
}
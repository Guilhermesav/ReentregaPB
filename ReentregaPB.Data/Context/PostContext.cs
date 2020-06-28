using Microsoft.EntityFrameworkCore;
using ReentregaPB.Dominio.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReentregaPB.Data.Context
{
    public class PostContext : DbContext
    {
        public PostContext(DbContextOptions<PostContext> options)
            : base(options)
        {

        }

        public DbSet<PostEntity> Posts { get; set; }
    }
}


using Eticaret.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Eticaret.Data
{
	public class EticaretContext : DbContext
	{
		public EticaretContext(DbContextOptions<EticaretContext>options): base(options) 
		{ 
		}

		/// <summary>
		/// AppylUsers classını baz alarak ApplyUsers tablosunu veri tabanına eklesin diye yazdık.
		/// </summary>
        public DbSet<ApplyUser> ApplyUsers { get; set; }

		/// <summary>
		/// genre classındaki alanlara bak Genres adında sql tarafına bir tablo oluştur.
		/// </summary>
		public DbSet<Genre> Genres { get; set; }

		public DbSet<Artist> Artists { get; set; }
		public DbSet<Album> Albums { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestJwtApiApplication1.Data
{
	public class DemoDbContext : IdentityDbContext
	{
		public DemoDbContext(DbContextOptions options) : base(options)
		{
		}
	}
}

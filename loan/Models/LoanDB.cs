using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace loan.Models
{
	public class LoanDB : DbContext
	{
			public LoanDB(DbContextOptions<LoanDB> options)
				: base(options) { }

			public DbSet<Loan> Loans { get; set; }
	}
}

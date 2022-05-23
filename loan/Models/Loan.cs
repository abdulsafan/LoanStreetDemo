using System;

namespace loan.Models
{
	public class Loan
	{
			public Guid Id { get; set; }
			public Decimal Amount { get; set; }
			public int Loan_Length { get; set; }
			public Decimal Monthly_Payment { get; set; }

	}
}

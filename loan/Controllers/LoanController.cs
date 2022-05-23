using loan.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace loan.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoanController : ControllerBase
	{
		private readonly LoanDB _context;
	    public LoanController(LoanDB context)
		{
			_context = context;
			Random rnd = new Random();
			if (_context.Loans.Count() == 0)
			{
				_context.Loans.Add(new Loan { Id = new Guid(), Amount = (decimal)rnd.NextDouble(), Monthly_Payment = (decimal)rnd.NextDouble(), Loan_Length = (int)rnd.Next() });
				_context.SaveChanges();
			}
		}
		// GET: api/<LoanController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Loan>>> Get()
		{
			return await _context.Loans.ToListAsync();
		}
		// GET api/<LoanController>/5
		[HttpGet("{id}")]
	    [ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<Loan>> Get(Guid id)
		{
			var existingLoan = await _context.Loans.FindAsync(id);

			if (id == null)
			{
				return NotFound();
			}
			return existingLoan;

		}

		// POST api/<LoanController>
		[HttpPost]
		public async Task<ActionResult<Loan>> Post(Loan loan)
		{
			_context.Loans.Add(loan);
			await _context.SaveChangesAsync();

			return CreatedAtAction("Get", new { id = loan.Id }, loan);
		}

		// PUT api/<LoanController>/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(Guid id, Loan loan)
		{
			if (id != loan.Id)
			{
				return BadRequest();
			}

			_context.Entry(loan).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!LoanExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		private bool LoanExists(Guid id)
		{
			return _context.Loans.Any(e => e.Id == id);
		}
	}
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ProductService.Models;
using ProductService.Data;
using Common.DTOs;

namespace ProductService.Controllers;

[ApiController]
[Route("categories")]
public class CategoryController(AppDbContext _context) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
	{
		var categories = await _context.Categories.ToListAsync();
		return categories.Select(c => c.ToDto()).ToList();
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<CategoryDto>> GetCategory(int id)
	{
		var category = await _context.Categories.FindAsync(id);
		if (category == null)
		{
			return NotFound();
		}

		return category.ToDto();
	}

	[HttpPost]
	public async Task<ActionResult<CategoryDto>> PostCategory([FromBody] CategoryDto categoryDto)
	{
		var category = Category.FromDto(categoryDto);
		_context.Categories.Add(category);
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category.ToDto());
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> PutCategory(int id, [FromBody] CategoryDto categoryDto)
	{
		var category = await _context.Categories.FindAsync(id);
		if (category == null)
		{
			return NotFound();
		}

		category.Name = categoryDto.Name;
		category.Description = categoryDto.Description;

		await _context.SaveChangesAsync();

		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteCategory(int id)
	{
		var category = await _context.Categories.FindAsync(id);
		if (category == null)
		{
			return NotFound();
		}

		_context.Categories.Remove(category);
		await _context.SaveChangesAsync();

		return NoContent();
	}
}
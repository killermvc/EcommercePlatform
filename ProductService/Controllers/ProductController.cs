using Microsoft.AspNetCore.Mvc;

using ProductService.Models;
using Common.DTOs;
using ProductService.Data;
using Microsoft.EntityFrameworkCore;

namespace ProductService.Controllers;

[Route("products")]
[ApiController]
public class ProductController(AppDbContext _context) : ControllerBase
{
	[HttpGet("{id}")]
	public async Task<ActionResult<ProductDto>> GetProduct(int id)
	{
		var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
		if(product == null)
		{
			return NotFound();
		}

		return Ok(product.ToDto());
	}

	[HttpGet("all/{pageSize}/{page}")]
	public ActionResult<List<ProductDto>> GetAllProducts(int pageSize, int page)
	{
		var products = _context.Products
			.Where(p => p.Stock > 0)
			.Skip((page - 1) * pageSize)
			.Take(pageSize)
			.Select(p => p.ToDto());

		if(!products.Any())
		{
			return NotFound();
		}

		return Ok(products);

	}

	[HttpPost]
	public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
	{
		var product = Product.FromDto(productDto);
		await _context.Products.AddAsync(product);
		await _context.SaveChangesAsync();
		return Ok(new { product.Id});
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
	{
		var product = await _context.Products.FindAsync(id);
		if(product == null)
		{
			return NotFound();
		}

		product.Name = productDto.Name;
		product.Description = productDto.Description;
		product.Price = productDto.Price;
		product.Stock = productDto.Stock;

		await _context.SaveChangesAsync();

		return Ok();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteProduct(int id)
	{
		var product = await _context.Products.FindAsync(id);
		if(product == null)
		{
			return NotFound();
		}

		_context.Products.Remove(product);
		await _context.SaveChangesAsync();

		return Ok();
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_with_mongodb.Models;
using mongo_db_demo.Data;
using api_with_mongodb.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace api_with_mongodb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly MongoContext _context;

        public ProductsController(MongoContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductDto productDto)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'MongoContext.Products'  is null.");
            }
            var tempProduct = new Product()
            {
                Id = _context.Products.Count() + 1,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CreatedOn = DateTime.Now
            };

            _context.Products.Add(tempProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = tempProduct.Id }, tempProduct);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        // private string GenerateJSONWebToken(UserModel userInfo)
        // {
        //     var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //     var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //     var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //       _config["Jwt:Issuer"],
        //       null,
        //       expires: DateTime.Now.AddMinutes(120),
        //       signingCredentials: credentials);

        //     return new JwtSecurityTokenHandler().WriteToken(token);
        // }


        // private UserModel AuthenticateUser(UserModel login)
        // {
        //     UserModel user = null;

        //     //Validate the User Credentials
        //     //Demo Purpose, I have Passed HardCoded User Information
        //     if (login.USERNAME == "Jignesh")
        //     {
        //         user = new UserModel { USERNAME = "Jignesh Trivedi", EmailAddress = "test.btest@gmail.com" };
        //     }
        //     return user;
        // }
    }
}

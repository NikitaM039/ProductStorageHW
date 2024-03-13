using ProductStorageHW.DB;
using ProductStorageHW.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace ProductStorageHW.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        [HttpPost(template: "CreateProduct")]
        public IActionResult PostProduct(int Cost, string CategoryName, string ProductName, string description)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (!context.Products.Any(x => x.Name.ToLower().Equals(ProductName)))
                    {
                        var category = context.Categories.FirstOrDefault(e => e.Name.Equals(CategoryName));
                        int numberCategory = category.Id;
                        context.Add(new Product()
                        {
                            Cost = Cost,
                            CategoryId = numberCategory,
                            Name = ProductName,
                            Description = description

                        });

                        context.SaveChanges();

                        return Ok();
                    }
                    else
                    {
                        return StatusCode(409);
                    }
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpGet(template: "GetProduct")]
        public IActionResult GetProduct(string ProductName)
        {
            try
            {
                using (var context = new ProductContext())
                {

                    return Ok(context.Categories.FirstOrDefault(e => e.Name.Equals(ProductName)));

                }
            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpPatch(template: "UpdateProduct")]
        public IActionResult UpdateProduct(int Cost, string CategoryName, string ProductName, string description)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    var category = context.Categories.FirstOrDefault(e => e.Name.Equals(CategoryName));
                    int numberCategory = category.Id;

                    var entity = context.Products.FirstOrDefault(e => e.Name.Equals(ProductName));

                    if (entity == null)
                    {
                        return StatusCode(404);
                    }
                    else
                    {
                        entity.Cost = Cost;
                        entity.CategoryId = numberCategory;
                        entity.Name = ProductName;
                        entity.Description = description;

                        context.SaveChanges();
                        return Ok(entity);
                    }
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete(template: "DeleteProduct")]
        public IActionResult DeleteProduct(string ProductName)
        {
            try
            {


                using (var context = new ProductContext())
                {
                    var entity = context.Products.FirstOrDefault(x => x.Name == ProductName);
                    if (entity == null)
                    {
                        return StatusCode(404);
                    }
                    else
                    {
                        context.Products.Remove(entity);
                        context.SaveChanges();
                        return Ok();
                    }
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }


    }
}
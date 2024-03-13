using ProductStorageHW.DB;
using ProductStorageHW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Runtime.InteropServices;

namespace ProductStorageHW.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {

        [HttpPost(template: "CreateCategory")]
        public IActionResult PostCategory(string CategoryName, string description)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (!context.Categories.Any(x => x.Name.ToLower().Equals(CategoryName)))
                    {
                        context.Add(new Category()
                        {
                            Name = CategoryName,
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


        [HttpGet(template: "GetCategory")]
        public IActionResult GetCategory(int id)
        {
            try
            {
                using (var context = new ProductContext())
                {

                    return Ok(context.Categories.FirstOrDefault(e => e.Id == id));

                }
            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpPatch(template: "UpdateCategory")]
        public IActionResult UpdateCategory(string categoryName, string description, string newCategoryName)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    var entity = context.Categories.FirstOrDefault(e => e.Name.Equals(categoryName));

                    if (entity == null)
                    {
                        return StatusCode(404);
                    }
                    else
                    {
                        entity.Name = newCategoryName;
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

        [HttpDelete(template: "DeleteCategory")]
        public IActionResult DeleteCategory(string categoryName)
        {
            try
            {


                using (var context = new ProductContext())
                {
                    var entity = context.Categories.FirstOrDefault(x => x.Name == categoryName);
                    if (entity == null)
                    {
                        return StatusCode(404);
                    }
                    else
                    {
                        context.Categories.Remove(entity);
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
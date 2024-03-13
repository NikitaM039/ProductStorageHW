using ProductStorageHW.DB;
using ProductStorageHW.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace ProductStorageHW.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageController : ControllerBase
    {
        [HttpPost(template: "CreateStorage")]
        public IActionResult PostStorage(string StorageName, string description)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    if (!context.Storages.Any(x => x.Name.ToLower().Equals(StorageName)))
                    {

                        context.Add(new Storage()
                        {

                            Name = StorageName,
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


        [HttpGet(template: "GetStorage")]
        public IActionResult GetStorage(string StorageName)
        {
            try
            {
                using (var context = new ProductContext())
                {

                    return Ok(context.Storages.FirstOrDefault(e => e.Name.Equals(StorageName)));

                }
            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpPatch(template: "UpdateStorage")]
        public IActionResult UpdateStorage(string StorageName, string description)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    var entity = context.Storages.FirstOrDefault(e => e.Name.Equals(StorageName));

                    if (entity == null)
                    {
                        return StatusCode(404);
                    }
                    else
                    {
                        entity.Name = StorageName;
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

        [HttpDelete(template: "DeleteStorage")]
        public IActionResult DeleteStorage(string StorageName)
        {
            try
            {


                using (var context = new ProductContext())
                {
                    var entity = context.Storages.FirstOrDefault(x => x.Name == StorageName);
                    if (entity == null)
                    {
                        return StatusCode(404);
                    }
                    else
                    {
                        context.Storages.Remove(entity);
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
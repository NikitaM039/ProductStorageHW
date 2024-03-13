using ProductStorageHW.DB;
using ProductStorageHW.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace ProductStorageHW.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductStorageController : ControllerBase
    {
        [HttpGet(template: "GetProductInStorage")]
        public IActionResult GetProductInStorage(string StorageName)
        {
            try
            {
                using (var context = new ProductContext())
                {
                    var storage = context.Storages.FirstOrDefault(e => e.Name.Equals(StorageName));
                    int sstorageId = storage.Id;


                    return Ok(context.ProductStorages.FirstOrDefault(e => e.StorageId == sstorageId));

                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
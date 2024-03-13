namespace ProductStorageHW.Models
{
    public class Storage : BaseModel
    {
        public List<ProductStorage> ProductStorage { get; set; } = new List<ProductStorage>();
    }
}
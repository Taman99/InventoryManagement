
namespace ProductService.Entities
{
    //Sizes Entity
    public partial class Size
    {
        public int SizeId { get; set; }
        public int? ProductId { get; set; }
        public string? SizeName { get; set; }
        public decimal? SizePrice { get; set; }
    }
}

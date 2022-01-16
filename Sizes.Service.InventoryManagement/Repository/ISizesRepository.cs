using SizesService.Entities;

namespace SizesService.Repository
{
    public interface ISizesRepository
    {
        IEnumerable<Size> GetSizes();

        IEnumerable<Size> GetSizeByProductId(string productId);

        bool CreateSize(Size size);

        bool UpdateSize(Size size);

        bool DeleteSizeBySizeId(int sizeId);

        bool DeleteSizesByProductId(string productId);

        bool SizeExists(int sizeId);
    }
}

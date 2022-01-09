using SizesService.Entities;

namespace SizesService.Repository
{
    public interface ISizesRepository
    {
        IEnumerable<Size> GetSizes();

        IEnumerable<Size> GetSizeByProductId(int productId);

        bool CreateSize(Size size);

        bool UpdateSize(Size size);

        bool DeleteSizeBySizeId(int sizeId);

        bool DeleteSizesByProductId(int productId);

        bool SizeExists(int sizeId);
    }
}

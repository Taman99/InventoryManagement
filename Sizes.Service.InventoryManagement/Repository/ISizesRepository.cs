using SizesService.Entities;

namespace SizesService.Repository
{
    public interface ISizesRepository
    {
        Size GetSizeByProductId(int productId);

        bool CreateSize(Size size);

        bool UpdateSize(Size size);

        bool DeleteSize(int sizeId);

        bool DeleteSizesByProductId(int productId);

        bool SizeExists(int sizeId);
    }
}

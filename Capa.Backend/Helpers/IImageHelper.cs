namespace Capa.Backend.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile? imageFile, string folder);
    }
}

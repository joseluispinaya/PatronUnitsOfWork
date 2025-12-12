
namespace Capa.Backend.Helpers
{
    public class ImageHelper : IImageHelper
    {
        public async Task<string> UploadImageAsync(IFormFile? imageFile, string folder)
        {
            if (imageFile == null)
                return string.Empty;

            var guid = Guid.NewGuid().ToString();
            var fileName = $"{guid}{Path.GetExtension(imageFile.FileName)}";

            var directory = Path.Combine(Directory.GetCurrentDirectory(), folder);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var path = Path.Combine(directory, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"{folder}/{fileName}";
        }
    }
}

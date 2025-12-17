
namespace Capa.Backend.Helpers
{
    public class ImageHelper : IImageHelper
    {
        public Task DeleteImage(string imagePath, string folder)
        {
            try
            {
                // Seguridad: la imagen debe pertenecer a la carpeta indicada
                if (!imagePath.StartsWith($"{folder}/"))
                    return Task.CompletedTask;

                var fullPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    imagePath
                );

                if (File.Exists(fullPath))
                    File.Delete(fullPath);
            }
            catch
            {
                // log opcional
            }

            return Task.CompletedTask;
        }

        public async Task<string> UploadImageAsync(IFormFile? imageFile, string folder)
        {
            if (imageFile == null || imageFile.Length == 0)
                return string.Empty;

            try
            {
                var guid = Guid.NewGuid().ToString();
                var fileName = $"{guid}{Path.GetExtension(imageFile.FileName)}";

                var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var directory = Path.Combine(wwwRootPath, folder);

                // if (!Directory.Exists(directory))
                //     Directory.CreateDirectory(directory);

                var path = Path.Combine(directory, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Validación final: confirmar que el archivo existe
                if (File.Exists(path))
                {
                    return $"{folder}/{fileName}";
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}

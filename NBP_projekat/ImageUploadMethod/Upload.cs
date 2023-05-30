namespace NBP_projekat.ImageUploadMethod
{
    public class Upload
    {
        public static async Task<string> SaveFile(string root, IFormFile imageFile, string path)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(imageFile.Name).Take(10).ToArray()).Replace(' ', '-');

            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);

            var imagePath = Path.Combine(root, $"wwwroot/{path}", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }
    }
}

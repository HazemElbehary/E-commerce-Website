namespace DashBoard.Helper
{
	public static class FileIOHelper
	{
		public static async Task<string> UploadAsync(IFormFile file, string folderPath)
		{
			var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
			var rootPath = Directory.GetCurrentDirectory();
			var path = Path.Combine(rootPath, folderPath, fileName);

			// Ensure the directory exists
			Directory.CreateDirectory(Path.GetDirectoryName(path) ?? throw new InvalidOperationException());

			// Create and write the file
			using (var stream = new FileStream(path, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			return path;
		}

		public static async Task Delete(string filePath)
		{
			File.Delete(filePath);
		}
	}
}

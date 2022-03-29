using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Tewr.Blazor.FileReader;

namespace AchiveClubServer.Services
{
    public class ImageLoader
    {
        private const string _destFolder = "../StaticFiles";

        private IWebHostEnvironment _environment;

        public ImageLoader(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> CreateImage(IFileReference file, string folder)
        {
            bool _fileLoaded = false;

            string _fileName = Guid.NewGuid() + ".jpeg";
            string _fullDestFolder = Path.Combine(_environment.ContentRootPath, _destFolder, folder);
            string _path = Path.Combine(_fullDestFolder, _fileName);
            string _relativePath = $"StaticFiles/{folder}/" + _fileName;

            try
            {
                await using FileStream fs = new(_path, FileMode.Create);
                var stream = await file.OpenReadAsync();
                await stream.CopyToAsync(fs);
                //await _file.OpenReadStream(1024 * 1024 * 5).CopyToAsync(fs);
                _fileLoaded = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File: {_fileName} Error: {ex.Message}");
            }

            if (_fileLoaded)
            {
                try
                {
                    using (Image image = Image.Load(_path))
                    {
                        image.Mutate(i => i.Resize(new ResizeOptions
                        {
                            Size = new Size(600, 600),
                            Mode = ResizeMode.Crop
                        }));
                        image.SaveAsJpeg(Path.Combine(_path));
                    }
                }
                catch
                {
                    Console.WriteLine("Image mutation error!");
                }
            }
            return _relativePath;
        }
    }
}

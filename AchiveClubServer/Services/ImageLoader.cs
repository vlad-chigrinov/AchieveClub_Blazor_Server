using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Tewr.Blazor.FileReader;

namespace AchieveClubServer.Services
{
    public class ImageLoader
    {
        private const string _destFolder = "../StaticFiles";

        private IWebHostEnvironment _environment;

        public ImageLoader(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> CreateImage(IBrowserFile file, string folder)
        {
            string _fileName = Guid.NewGuid() + ".jpeg";
            string _fullDestFolder = Path.Combine(_environment.ContentRootPath, _destFolder, folder);
            string _path = Path.Combine(_fullDestFolder, _fileName);
            string _relativePath = $"StaticFiles/{folder}/" + _fileName;

            try
            {
                
                await using (FileStream fs = new(_path, FileMode.Create))
                {
                    await file.OpenReadStream(maxAllowedSize: 1024*1024*10).CopyToAsync(fs);
                    Task.WaitAll();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"File: {_fileName} Error: {ex.Message}");
            }

            
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
            
            return _relativePath;
        }
    }
}

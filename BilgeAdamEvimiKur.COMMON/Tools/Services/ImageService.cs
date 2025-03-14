using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using SkiaSharp;



namespace BilgeAdamEvimiKur.COMMON.Tools.Services
{
    public static class ImageService
    {
        public static async Task<string?> Upload(IFormFile formFile)
        {
            if (formFile != null && formFile.Length > 0)
            {
                Guid uniqueName = Guid.NewGuid();
                string extension = Path.GetExtension(formFile.FileName);
                string relativePath = $"/images/{uniqueName}{extension}";
                string fullPath = $"{Directory.GetCurrentDirectory()}/wwwroot/images/{uniqueName}{extension}";
                using (FileStream stream = new(fullPath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                return relativePath;
            }
            return null;
        }

        public static string RandomSaveImagePNG(int width = 100, int height = 100)
        {
            Guid uniqueName = Guid.NewGuid();
            string relativePath = $"/images/{uniqueName}.PNG";
            string fullPath = $"{Directory.GetCurrentDirectory()}/wwwroot{relativePath}";

            Random random = new Random();
            using (SKBitmap bitmap = new SKBitmap(width, height))
            using (SKCanvas canvas = new SKCanvas(bitmap))
            {
                canvas.Clear(SKColors.LightSeaGreen);

                // Rastgele daireler çizme
                for (int i = 0; i < 6; i++)
                {
                    SKPaint paint = new SKPaint
                    {
                        Color = new SKColor(
                            (byte)random.Next(256),
                            (byte)random.Next(256),
                            (byte)random.Next(256)),
                        IsAntialias = true,
                        Style = SKPaintStyle.StrokeAndFill,
                        StrokeWidth = random.Next(1, 10)
                    };

                    float x = random.Next(101);
                    float y = random.Next(101);
                    float radius = random.Next(1, 20);

                    canvas.DrawCircle(x, y, radius, paint);
                }

                // Rastgele metinler ekleme
                for (int i = 0; i < 6; i++)
                {
                    SKPaint paint = new SKPaint
                    {
                        Color = new SKColor(
                            (byte)random.Next(256),
                            (byte)random.Next(256),
                            (byte)random.Next(256)),
                        IsAntialias = true,
                        TextSize = random.Next(1, 20)
                    };

                    float x = random.Next(101);
                    float y = random.Next(101);
                    string text = $"Product {i + 1}";

                    canvas.DrawText(text, x, y, paint);
                }

                // Rastgele dikdörtgenler çizme
                for (int i = 0; i < 6; i++)
                {
                    SKPaint paint = new SKPaint
                    {
                        Color = new SKColor(
                            (byte)random.Next(256),
                            (byte)random.Next(256),
                            (byte)random.Next(256)),
                        IsAntialias = true,
                        Style = SKPaintStyle.StrokeAndFill,
                        StrokeWidth = random.Next(1, 10)
                    };

                    float left = random.Next(101);
                    float top = random.Next(101);
                    float right = left + random.Next(1, 20);
                    float bottom = top + random.Next(1, 20);

                    canvas.DrawRect(new SKRect(left, top, right, bottom), paint);
                }
                using (SKImage image = SKImage.FromBitmap(bitmap))
                using (SKData data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (FileStream stream = File.OpenWrite(fullPath))
                {
                    data.SaveTo(stream);
                }
                return relativePath;
            }

         }
    }
}

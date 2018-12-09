using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ajj.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ajj.Controllers
{
    public class ImageController : Controller
    {
        [HttpPost]
        [Route("UploadImageAsyn")]
        public async Task<IActionResult> UploadImageAsyn(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            if (size > 0)
            {
                using (Image img = Image.FromStream(files[0].OpenReadStream()))
                {
                    Stream ms = new MemoryStream(img.Resize(100, 100).ToByteArray());

                    return new FileStreamResult(ms, "image/jpg");
                }
            }

            return View();
        }
    }
}
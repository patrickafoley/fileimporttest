using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using aspbasic.service;
using aspbasic.service.poco;

namespace aspbasic.Controllers
{
    public class UploadController : Controller
    {

        private IAspBasicService aspBasicService;

        public UploadController(IAspBasicService _aspBasicService)
        {
            this.aspBasicService = _aspBasicService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["FileOptions"] = aspBasicService.GetFileTypes();
            return View();
        }


        // basic upload, ideally this would be streamed, cleanup temp files and have more error checking
        public async Task<IActionResult> UploadFile(List<IFormFile> files, string fileType)
        {
            // based on MSDN example
            long size = files.Sum(f => f.Length);
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            string[] fileLines = aspBasicService.ReadFile(filePath);
            if (aspBasicService.IsFileValid(fileLines, fileType))
            {
                var result = aspBasicService.ImportFile(fileLines, fileType);
                ViewData["UploadResult"] = result;
                return View();
            }
            else
            {
                ViewData["UploadResult"] = new ImportResult()
                {
                    isError = true,
                    errorText = "File is in invalid format"
                };
                return View();
            }

        }
    }
}

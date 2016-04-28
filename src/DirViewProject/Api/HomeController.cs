using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.StaticFiles;

namespace DirViewProject.Api
{
    public class HomeController : Controller
    {
        public ActionResult Open(string path)
        {
            var file = System.IO.File.ReadAllBytes(path);
            var contentType = GetContentType(path);
            Response.Headers.Add("Content-Disposition", "inline; filename=" + path);
            return File(file, contentType);
        }

        public string GetContentType(string path)
        {
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(path, out contentType);
            return contentType ?? "application/octet-stream";
        }

        public ActionResult Index()
        {
            var filepath = Request.Query["path"].ToString();
            if (!string.Equals(filepath, string.Empty))
            {
                return Open(filepath);
            }
            ViewData["Title"] = "Home Page";
            return View(new Models.Directory());
        }
    }
}
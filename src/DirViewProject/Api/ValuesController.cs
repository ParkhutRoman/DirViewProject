using DirViewProject.Enum;
using Microsoft.AspNet.Mvc;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace DirViewProject.Api
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private const int large = 100 * 1024 * 1024;
        private const int medium = 50 * 1024 * 1024;
        private const int small = 10 * 1024 * 1024;

        [HttpGet]
        public Models.Directory GetAll()
        {
            return SeeDrives();
        }

        [HttpPost]
        public Models.Directory Get([FromBody]Models.Item item)
        {
            if (item == null || item.Path == "")
            {
                return SeeDrives();
            }
            var directory = SeeDir(item.Path);
            if (directory == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return directory;
        }

        private Models.Directory SeeDrives()
        {
            var drives = from drive in DriveInfo.GetDrives()
                         select new Models.Item
                         {
                             Name = drive.Name,
                             Path = drive.Name
                         };
            var directory = new Models.Directory
            {
                Path = "",
                Directories = drives.ToList()
            };
            return directory;
        }

        private Models.Directory SeeDir(string path)
        {
            var dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
                return null;
            var files = dirInfo.GetFiles();
            var dirs = dirInfo.GetDirectories();
            var directory = new Models.Directory();
            directory.Files = (from file in files
                               select new Models.Item
                               {
                                   Name = file.Name,
                                   Path = file.FullName
                               }).ToList();
            directory.Directories = (from dir in dirs
                                     select new Models.Item
                                     {
                                         Name = dir.Name,
                                         Path = dir.FullName
                                     }).ToList();
            directory.Directories.Insert(0, new Models.Item
            {
                Name = "..",
                Path = dirInfo.Parent?.FullName
            });

            directory.Path = path;
            //try
            //{
            //    var sfiles = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
            var counts = files.GroupBy(f =>
            {
                return f.Length < small ? FileSize.SMALL :
                    f.Length < medium ? FileSize.MEDIUM :
                    f.Length < large ? FileSize.LARGE :
                    FileSize.LARGE;
            }).ToDictionary(
            group => group.Key,
            group => group.Count()
        );
            if (counts.ContainsKey(FileSize.SMALL))
                directory.SmallFilesCount = counts[FileSize.SMALL];
            if (counts.ContainsKey(FileSize.MEDIUM))
                directory.MediumFilesCount = counts[FileSize.MEDIUM];
            if (counts.ContainsKey(FileSize.LARGE))
                directory.LargeFilesCount = counts[FileSize.LARGE];
            //}
            //catch (Exception x)
            //{
            //    Debug.WriteLine($"Exception = {x}");
            //}

            return directory;
        }
    }
}
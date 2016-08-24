using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Permissions;

using System.Web.Http;
using FileBrowsing.Models;
using Newtonsoft.Json;

namespace FileBrowsing.Controllers
{

    public class DataController : ApiController
    {
        public FileBrowsingModels Get()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            List<string> drivesName = new List<string>();
            foreach (var driveInfo in drives)
            {
                drivesName.Add(driveInfo.Name);
            }
            return new FileBrowsingModels {Directories = drivesName};
        }

        
        public FileBrowsingModels Get(string path)
        {
            var dirName = @path;
            if (Directory.Exists(dirName))
            {
                DirectoryInfo dir = new DirectoryInfo(dirName);
                
                List<string> dirs = dir.GetDirectories().Where(sd => !sd.Attributes.ToString().Contains(FileAttributes.System.ToString())).Select(d => d.Name).ToList();
                List<string> files = dir.GetFiles().Select(f => f.Name).ToList();

                var parent = (dir.Parent != null) ? dir.Parent.Name : null;

                IEnumerable<FileInfo> allFiles = GetAllFiles(dir, "*");
                var sizeLessTenMb = allFiles.Count(f => (f.Length) / 1024 / 1024 <= 10);
                var sizeMoreTenMb = allFiles.Count(f => (f.Length) / 1024 / 1024 > 10 && (f.Length) / 1024 / 1024 <= 50);
                var sizeMoreHundredMb = allFiles.Count(f => (f.Length) / 1024 / 1024 >= 100);
                
                return new FileBrowsingModels
                {
                    Directories = dirs,
                    Files = files,
                    CoutnFilesWithSizeLessTenMb = sizeLessTenMb,
                    CoutnFilesWithSizeMoreTenMb = sizeMoreTenMb,
                    CoutnFilesWithSizeMoreHundredMb = sizeMoreHundredMb,
                    CurrentDirectory = dir.FullName,
                    ParentDirectory = parent
                };
            }
            return new FileBrowsingModels
            {
                ErrorMessage = "This directory doesn't exists or you can't access to it"
            };
        }
        
        private List<FileInfo> GetAllFiles(DirectoryInfo dir, string pattern)
        {
            var files = new List<FileInfo>();

            try
            {

                files.AddRange(dir.GetFiles(pattern, SearchOption.TopDirectoryOnly));
                foreach (var directory in dir.GetDirectories())
                    files.AddRange(GetAllFiles(directory, pattern));
            }
            catch (Exception ex) { }

            return files;
        }

        
    }
}

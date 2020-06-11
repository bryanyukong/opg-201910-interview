using Microsoft.AspNetCore.Hosting;
using opg_201910_interview.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace opg_201910_interview.Services
{
    public class UploadFileService : IUploadFileService
    {
        #region Fields

        private readonly IHostingEnvironment _hostingEnvironment;

        #endregion

        #region Ctor

        public UploadFileService(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        #endregion

        #region Utilities

        private UploadFile MapFiles(string path, string name)
        {
            var uploadFile = new UploadFile();

            Enum.TryParse(name, out Client clientId);

            var splitter = (clientId == Client.ClientA) ? "-" : "_";
            var dateFormat = (clientId == Client.ClientA) ? "yyyy-MM-dd" : "yyyyMMdd";
            var fileOrder = (clientId == Client.ClientA) ? new[] { "shovel", "waghor", "blaze", "discus" } : new[] { "orca", "widget", "eclair", "talon" };

            uploadFile.ClientId = ((int)clientId).ToString();
            uploadFile.FileDirectoryPath = path.Replace(_hostingEnvironment.ContentRootPath, "")
                                           .Replace("\\", "/");

            var rawFiles = Directory.EnumerateFiles(path)
                .Where(m => Path.GetExtension(m) == ".xml" && Path.GetFileName(m).Contains(splitter))
                .Select(m => new RawFile
                {
                    FileName = Path.GetFileName(m),
                    FileDateTitle = GetExactParsedDate(Path.GetFileNameWithoutExtension(m)
                                    .Substring(Path.GetFileNameWithoutExtension(m).IndexOf(splitter) + 1),
                                    dateFormat),
                    Index = Array.IndexOf(fileOrder, Path.GetFileNameWithoutExtension(m)
                                    .Substring(0, Path.GetFileNameWithoutExtension(m).IndexOf(splitter)))

                })
                .OrderBy(m => m.Index)
                .ThenBy(m => m.FileDateTitle);

            foreach (var file in rawFiles)
            {
                uploadFile.FileNames.Add(Path.GetFileName(file.FileName));
            }

            return uploadFile;
        }

        private IEnumerable<UploadFile> MapDir(string path)
        {
            var uploadFiles = new List<UploadFile>();

            IEnumerable<string> dirList = Directory.EnumerateDirectories(path);
            foreach (string dir in dirList)
            {
                var dirInfo = new DirectoryInfo(dir);

                uploadFiles.Add(MapFiles(dir, dirInfo.Name));
            }

            return uploadFiles;
        }

        private DateTime GetExactParsedDate(string date, string format)
        {
            return DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
        }

        #endregion

        #region Methods

        public IEnumerable<UploadFile> GetUploadFiles()
        {
            var folderPath = $"{_hostingEnvironment.ContentRootPath}\\UploadFiles";

            if (Directory.Exists(folderPath))
            {
                var directories = MapDir(folderPath);
                return directories;
            }
            else
            {
                return new List<UploadFile>();
            }
        }

        public UploadFile GetUploadFileByName(string name)
        {
            var folderPath = $"{_hostingEnvironment.ContentRootPath}\\UploadFiles\\{name}";

            if (Directory.Exists(folderPath))
            {
                var files = MapFiles(folderPath, name);
                return files;
            }
            else
            {
                return new UploadFile();
            }

        }

        #endregion
    }
}

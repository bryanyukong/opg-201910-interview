using System.Collections.Generic;

namespace opg_201910_interview.Models
{
    public partial class UploadFile
    {
        #region Ctor

        public UploadFile()
        {
            FileNames = new List<string>();
        }

        #endregion

        public string ClientId { get; set; }
        public string FileDirectoryPath { get; set; }
        public List<string> FileNames { get; set; }
    }
}

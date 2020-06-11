using opg_201910_interview.Models;
using System.Collections.Generic;

namespace opg_201910_interview.Services
{
    public interface IUploadFileService
    {
        IEnumerable<UploadFile> GetUploadFiles();
        UploadFile GetUploadFileByName(string name);
    }
}

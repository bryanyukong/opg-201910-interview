using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using opg_201910_interview.Models;
using opg_201910_interview.Services;

namespace opg_201910_interview.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UploadFilesController : ControllerBase
    {
        #region Fields

        private readonly IUploadFileService _uploadFileService;

        #endregion

        #region Ctor

        public UploadFilesController(IUploadFileService uploadFileService)
        {
            this._uploadFileService = uploadFileService;
        }

        #endregion

        #region Methods

        // GET: api/<UploadFileController>
        [HttpGet]
        public IEnumerable<UploadFile> Get()
        {
            return _uploadFileService.GetUploadFiles();
        }

        // GET api/<UploadFileController>/5
        [HttpGet("{name}")]
        public UploadFile Get(string name)
        {
            return _uploadFileService.GetUploadFileByName(name);
        }

        #endregion
    }
}

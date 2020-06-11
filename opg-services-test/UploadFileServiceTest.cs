using Microsoft.AspNetCore.Hosting;
using Moq;
using NUnit.Framework;
using opg_201910_interview.Models;
using opg_201910_interview.Services;
using System.IO;
using System.Linq;

namespace opg_services_test
{
    public class UploadFileServiceTest
    {
        private Mock<IHostingEnvironment> _hostingEnvironment;
        private IUploadFileService _uploadFileService;

        [SetUp]
        public void Setup()
        {
            _hostingEnvironment = new Mock<IHostingEnvironment>();
            _hostingEnvironment.Setup(m => m.ContentRootPath).Returns(Directory.GetCurrentDirectory());

            _uploadFileService = new UploadFileService(_hostingEnvironment.Object);
        }

        [Test]
        public void Can_get_uploadFile_by_name()
        {
            var uploadFile = _uploadFileService.GetUploadFileByName(name: "ClientA");
            Assert.IsTrue(uploadFile.ClientId == ((int)Client.ClientA).ToString());

            uploadFile = _uploadFileService.GetUploadFileByName(name: "ClientB");
            Assert.IsTrue(uploadFile.ClientId == ((int)Client.ClientB).ToString());

            uploadFile = _uploadFileService.GetUploadFileByName(name: "ClientC");
            Assert.IsTrue(string.IsNullOrEmpty(uploadFile.ClientId));
        }

        [Test]
        public void Can_get_uploadFiles()
        {
            var uploadFile = _uploadFileService.GetUploadFiles();
            Assert.IsTrue(uploadFile.Count() > 0);
        }
    }
}
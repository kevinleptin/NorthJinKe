using System;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Moq;
using NorthJinKe.Controllers.api;
using NorthJinKe.Services;
using NUnit.Framework;

namespace NorthJinKe.Tests.Controllers.api
{
    [TestFixture]
    public class WxFileControllerTests
    {
        [Test]
        public async Task ConvertAysnc_Impletemented_NoImplementException()
        {
            var _fileDownloaderService = new Mock<IFileDownloaderService>();
            _fileDownloaderService.Setup(c => c.DownloadWxFileAsync("1")).Returns(Task.FromResult(new byte[] { 0x1 }));
            _fileDownloaderService.Setup(c => c.SaveFileAsync(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(Task.FromResult(""));

            var _audioFileService = new Mock<IAudioFileService>();
            _audioFileService.Setup(c => c.ConvertAmr2Mp3Async(It.IsAny<string>())).Returns(Task.FromResult(""));

            var _hostEnvironmentService = new Mock<IHostingEnvironmentService>();
            _hostEnvironmentService.Setup(c => c.AppMapPath(It.IsAny<string>())).Returns("");
            _hostEnvironmentService.Setup(c => c.AppPhysicalPath()).Returns(@"c:\");

            var controller = new WxFileController(_fileDownloaderService.Object, 
                                                   null,
                                                    //_audioFileService.Object, 
                                                    _hostEnvironmentService.Object);

            var result = await controller.ConvertAsync("1") as OkNegotiatedContentResult<string>;
            
            _fileDownloaderService.Verify(c => c.DownloadWxFileAsync("1"));
            Assert.IsEmpty(result.Content);

        }
    }
}

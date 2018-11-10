using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using NorthJinKe.Services;

namespace NorthJinKe.Controllers.api
{
    public class WxFileController : ApiController
    {
        private IFileDownloaderService _fileDownloaderService;
        private IAudioFileService _audioFileService;
        private IHostingEnvironmentService _hostingEnvironmentService;

        public WxFileController(IFileDownloaderService fileDownloaderService = null, 
                                IAudioFileService audioFileService = null,
                                IHostingEnvironmentService hostingEnvironmentService = null)
        {
            _fileDownloaderService = fileDownloaderService ?? new FileDownloaderService();
            _audioFileService = audioFileService ?? new AudioFileService();
            _hostingEnvironmentService = hostingEnvironmentService ?? new HostingEnvironmentService();
        }

        [HttpPost, Route("api/wxfile/convert/{id}")]
        public async Task<IHttpActionResult> ConvertAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                var error = new {errorCode = 402, errorMsg = "id should not empty"};
                return Ok(error);
            }

            //get the file downloader 
            byte[] armFileData = await _fileDownloaderService.DownloadWxFileAsync(id);
            string absAmrFilePath = await _fileDownloaderService.SaveFileAsync(_hostingEnvironmentService.AppMapPath("~/uploads/"), armFileData);

            //get the converter
            string absMp3FilePath = await _audioFileService.ConvertAmr2Mp3Async(absAmrFilePath);

            //return the access path to user
            string relFilePath = absMp3FilePath.Replace(_hostingEnvironmentService.AppPhysicalPath(), string.Empty);
            return Ok(relFilePath);
        }
    }
}

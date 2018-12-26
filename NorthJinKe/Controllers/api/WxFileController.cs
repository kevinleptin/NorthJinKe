using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using Newtonsoft.Json;
using NorthJinKe.Models.dto;
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

        public WxFileController()
        {
            _fileDownloaderService = new FileDownloaderService();
            _audioFileService = new AudioFileService();
            _hostingEnvironmentService = new HostingEnvironmentService();
        }

        public string Options()
        {
            return null;
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
            if(relFilePath.StartsWith("\\") == false)
            {
                relFilePath = "\\" + relFilePath;
            }
            relFilePath = relFilePath.Replace("\\", "/");
            return Ok(relFilePath);
        }

        [HttpPost, Route("api/baiduai/gesture")]
        public async Task<IHttpActionResult> GestureAsync(GestureDto dto)
        {
            string baiduUrlFmt = @"https://aip.baidubce.com/rest/2.0/image-classify/v1/gesture?access_token={0}";
            string baiduToken = ConfigurationManager.AppSettings["baiduAIToken"];
            string requestUrl = string.Format(baiduUrlFmt, baiduToken);
            HttpClient client = new HttpClient();
            //todo: access_token 应该放到一个独立，定时刷新的地方存取
            var dict = new Dictionary<string, string>();
            dict.Add("image", dto.image);
            var stringContent = new StringContent("image=" + HttpUtility.UrlEncode(dto.image), System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");


            var response = await client.PostAsync(requestUrl, stringContent);
            var resultJsonStr = await response.Content.ReadAsStringAsync();
            return Ok(JsonConvert.DeserializeObject(resultJsonStr));

        }
    }
}

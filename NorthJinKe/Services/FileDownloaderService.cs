using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NorthJinKe.Services
{
    public interface IFileDownloaderService
    {
       Task<byte[]> DownloadWxFileAsync(string id);
       Task<string> SaveFile(string rootFolderPath, byte[] fileData);
    }

    public class FileDownloaderService : IFileDownloaderService
    {
        public async Task<byte[]> DownloadWxFileAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveFile(string rootFolderPath, byte[] fileData)
        {
            throw new NotImplementedException();
        }
    }
}
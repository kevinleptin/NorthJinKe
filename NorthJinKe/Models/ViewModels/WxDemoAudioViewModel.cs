using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthJinKe.Models.ViewModels
{
    public class WxDemoAudioViewModel
    {
        public string AppId { get; set; }
        public string TimeStamp { get; set; }
        public string NonceStr { get; set; }
        public string Signature { get; set; }
    }
}
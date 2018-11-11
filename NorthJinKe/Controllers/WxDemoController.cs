using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthJinKe.Models.ViewModels;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Helpers;

namespace NorthJinKe.Controllers
{
    public class WxDemoController : Controller
    {
        // GET: WxDemo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Audio()
        {
            WxDemoAudioViewModel vm = new WxDemoAudioViewModel();
            string appId = ConfigurationManager.AppSettings["appId"];
            string appSecret = ConfigurationManager.AppSettings["appSecret"];
            vm.AppId = appId;
            vm.NonceStr = JSSDKHelper.GetNoncestr();
            vm.TimeStamp = JSSDKHelper.GetTimestamp();
            string ticket = JsApiTicketContainer.TryGetJsApiTicket(appId, appSecret, true);
            string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString();
            vm.Signature = JSSDKHelper.GetSignature(ticket, vm.NonceStr, vm.TimeStamp, url);

            return View(vm);
        }
    }
}
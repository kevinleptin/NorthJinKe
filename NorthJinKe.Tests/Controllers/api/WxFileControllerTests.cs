using System;
using NorthJinKe.Controllers.api;
using NUnit.Framework;

namespace NorthJinKe.Tests.Controllers.api
{
    [TestFixture]
    public class WxFileControllerTests
    {
        [Test]
        public void ConvertAysnc_Impletemented_NoImplementException()
        {
            var controller = new WxFileController();
            Assert.That(async ()=> await controller.ConvertAsync(""), Throws.TypeOf<NotImplementedException>());
        }
    }
}

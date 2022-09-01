using GeoLocationByIp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using ZXing.QrCode;

namespace GeoLocationByIp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoLocation : ControllerBase
    {
        GeoRepository geoRepository = new GeoRepository();
        [HttpGet]
        public IActionResult GetLocationUser(string ip)
        {
            var data = geoRepository.GepLocationById(ip);
            return Ok(data.countryCode);
        }

        [HttpGet("GenerateQrCode")]
        public async Task<ActionResult> GenerateQrCode(string qrCodeText)
        {
            Bitmap qrCodeImage = Utilites.ExMethod.GenerateQR(600, 600, qrCodeText);
            var bytes = ImageToArray(qrCodeImage);
            return File(bytes,"image/bmp");
        }
        [NonAction]
        private byte[] ImageToArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

    }
}

using GeoLocationByIp.Service;
using GeoLocationByIp.Utilites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
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
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", "527-0-0-0-10000-10000-410.jpg");
            Bitmap overlay = new Bitmap(imagePath);

            QRCodeGenerator _qrCode = new QRCodeGenerator();
            QRCodeData _qRCodeData = _qrCode.CreateQrCode(qrCodeText, QRCodeGenerator.ECCLevel.H);
            QRTEST qRCode = new QRTEST(_qRCodeData);
            Image qrCodeImage = qRCode.GetGraphic(200,Color.Blue,Color.White,overlay);
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

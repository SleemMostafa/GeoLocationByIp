using System.Drawing;
using ZXing.QrCode.Internal;
using ZXing;
using ZXing.Rendering;
using Microsoft.Extensions.Hosting.Internal;

namespace GeoLocationByIp.Utilites
{
    public  class ExMethod
    {
        private readonly IWebHostEnvironment hostEnvironment;
        public ExMethod(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public static Bitmap GenerateQR(int width, int height, string text)
        {
            var bw = new ZXing.BarcodeWriter();

            var encOptions = new ZXing.Common.EncodingOptions
            {
                Width = width,
                Height = height,
                Margin = 5,
                PureBarcode = true,
                GS1Format = true,
            };

            encOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);            
            bw.Encoder = default;
            bw.Renderer = new BitmapRenderer { Foreground = Color.FromArgb(30, 144, 255),DpiY=22};
            bw.Options = encOptions;
            bw.Format = ZXing.BarcodeFormat.UPC_EAN_EXTENSION;
            Bitmap bm = bw.Write(text);
            //Bitmap overlay = new Bitmap("~/Content/Images/" + "facebook-symbol-png-logo.jpg");
            //int deltaHeigth = bm.Height - overlay.Height;
            //int deltaWidth = bm.Width - overlay.Width;
            Graphics g = Graphics.FromImage(bm);
            //g.DrawImage(overlay, new Point(deltaWidth / 2, deltaHeigth / 2));

            return bm;
        }
    }
}

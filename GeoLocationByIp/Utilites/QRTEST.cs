using QRCoder;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace GeoLocationByIp.Utilites
{
    public class QRTEST : AbstractQRCode, IDisposable
    {
        private readonly IWebHostEnvironment hostEnvironment;
        public QRTEST(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }
        public QRTEST(QRCodeData data):base(data)
        {
        }
        public Bitmap GetGraphic(int pixelsPerModule, Color color, Color lightColor, Bitmap icon = null, int iconSizePercent = 15, int iconBorderWidth = 6, bool drawQuietZones = true)
        {

            int num = (base.QrCodeData.ModuleMatrix.Count - ((!drawQuietZones) ? 8 : 0)) * pixelsPerModule;
            int num2 = (!drawQuietZones) ? (4 * pixelsPerModule) : 0;
            Bitmap bitmap = new Bitmap(num, num, PixelFormat.Format32bppPArgb);
            bitmap.MakeTransparent(); ;
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {  
                using (SolidBrush brush2 = new SolidBrush(lightColor))
                {
                    using (SolidBrush brush = new SolidBrush(color))
                    {
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        graphics.CompositingQuality = CompositingQuality.GammaCorrected;
                        graphics.Clear(lightColor);
                        bool flag = icon != null && iconSizePercent > 0 && iconSizePercent <= 100;
                        float num3 = 0f;
                        float num4 = 0f;
                        float num5 = 0f;
                        float num6 = 0f;
                        if (flag)
                        {
                            num3 = (float)(iconSizePercent * bitmap.Width) / 60f;
                            num4 = (flag ? (num3 * (float)icon.Height / (float)icon.Width) : 0f);
                            num5 = ((float)bitmap.Width - num3) / 2f;
                            num6 = ((float)bitmap.Height - num4) / 2f;
                            RectangleF rect = new RectangleF(num5 - (float)iconBorderWidth, num6 - (float)iconBorderWidth, num3 + (float)(iconBorderWidth * 2), num4 + (float)(iconBorderWidth * 2));
                            CreateRoundedRectanglePath(rect, iconBorderWidth * 2);
                        }
                        for (int i = 0; i < num + num2; i += pixelsPerModule)
                        {
                            for (int j = 0; j < num + num2; j += pixelsPerModule)
                            {
                                if (base.QrCodeData.ModuleMatrix[(j + pixelsPerModule) / pixelsPerModule - 1][(i + pixelsPerModule) / pixelsPerModule - 1])
                                {
                                    Rectangle rect2 = new Rectangle(i - num2, j - num2, pixelsPerModule, pixelsPerModule);
                                    graphics.FillEllipse(brush, rect2);
                                }
                                else
                                {
                                    graphics.FillRectangle(brush2, new Rectangle(i - num2, j - num2, pixelsPerModule, pixelsPerModule));
                                }
                            }
                        }
                        if (flag)
                        {
                            RectangleF destRect = new RectangleF(num5, num6, num3, num4);
                            graphics.DrawImage(icon, destRect, new RectangleF(0f, 0f, icon.Width, icon.Height), GraphicsUnit.Pixel);
                        }

                        graphics.Save();
                        return bitmap;
                    }
                }
            }
        }
        internal GraphicsPath CreateRoundedRectanglePath(RectangleF rect, int cornerRadius)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180f, 90f);
            graphicsPath.AddLine(rect.X + (float)cornerRadius, rect.Y, rect.Right - (float)(cornerRadius * 2), rect.Y);
            graphicsPath.AddArc(rect.X + rect.Width - (float)(cornerRadius * 2), rect.Y, cornerRadius * 2, cornerRadius * 2, 270f, 90f);
            graphicsPath.AddLine(rect.Right, rect.Y + (float)(cornerRadius * 2), rect.Right, rect.Y + rect.Height - (float)(cornerRadius * 2));
            graphicsPath.AddArc(rect.X + rect.Width - (float)(cornerRadius * 2), rect.Y + rect.Height - (float)(cornerRadius * 2), cornerRadius * 2, cornerRadius * 2, 0f, 90f);
            graphicsPath.AddLine(rect.Right - (float)(cornerRadius * 2), rect.Bottom, rect.X + (float)(cornerRadius * 2), rect.Bottom);
            graphicsPath.AddArc(rect.X, rect.Bottom - (float)(cornerRadius * 2), cornerRadius * 2, cornerRadius * 2, 90f, 90f);
            graphicsPath.AddLine(rect.X, rect.Bottom - (float)(cornerRadius * 2), rect.X, rect.Y + (float)(cornerRadius * 2));
            graphicsPath.CloseFigure();
            return graphicsPath;
        }
    }
}

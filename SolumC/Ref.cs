using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace SolumC
{
    class Ref
    {
        public static Bitmap bitEtiqueta;


        public static void btnGenerar(String direccion, String cantidad, String version, String ano, String semana, String rutaCarpeta, String refe)
        {
            bitEtiqueta = new Bitmap(direccion);
            for (int i = 0; i < Convert.ToInt64(cantidad); i++)
            {
                merge(i, version, ano, semana, rutaCarpeta,refe);
            }
        }

        public static void merge(int i, String version, String ano, String semana, String rutaCarpeta,String refe)
        {

            // Crea un nuevo objeto Bitmap para almacenar las dos imágenes combinadas
            Bitmap combinedImage = new Bitmap(bitEtiqueta.Width, bitEtiqueta.Height);


            // Dibuja las dos imágenes en el objeto Bitmap combinado
            using (var g = Graphics.FromImage(combinedImage))
            {
                g.DrawImage(bitEtiqueta, 0, 0, 265, 265);
                g.DrawImage(barcode(version, ano, semana, i, refe), 0, 140, 265, 50);
            }

            // Crea un objeto BitmapSource a partir del objeto Bitmap
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                combinedImage.GetHbitmap(),
                IntPtr.Zero,
                System.Windows.Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            // guarda nueva pegatina 
            combinedImage.Save(rutaCarpeta + "\\" + "SOL-AR-REF" + refe + "-" + version + "-" + ano + semana + "-" + i + ".png");

        }

        // crea codigos de barra
        public static Bitmap barcode(String version, String ano, String semana, int indice, String refe)
        {
            BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();
            codigo.IncludeLabel = true;
            codigo.LabelFont = new Font("Gotham", 8);


            // poner el largo del archivo y las coordenadas en x a 0
            System.Drawing.Image co = codigo.Encode(BarcodeLib.TYPE.CODE128, "SOL-AR-REF" + refe + "-" + version + "-" + ano + semana + "-" + indice, System.Drawing.Color.Black, System.Drawing.Color.Transparent, 265, 50);

            Bitmap bitmapCo = new Bitmap(co);

            return bitmapCo;
        }
    }
}

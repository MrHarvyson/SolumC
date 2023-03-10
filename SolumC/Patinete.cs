using ImageMagick;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using BarcodeLib;
using System.Windows.Documents;

namespace SolumC
{
    class Patinete
    {

        public static Bitmap bitEtiqueta;


        public static void btnGenerar(String direccion, String cantidad, String version, String ano, String semana, String rutaCarpeta)
        {
            bitEtiqueta = new Bitmap(direccion);
            for (int i = 1; i <= Convert.ToInt64(cantidad); i++)
            {
                merge(i, version, ano, semana, rutaCarpeta);
            }
        }


        public static void merge(int i, String version, String ano, String semana, String rutaCarpeta)
        {

            // Crea un nuevo objeto Bitmap para almacenar las dos imágenes combinadas
            Bitmap combinedImage = new Bitmap(bitEtiqueta.Width, bitEtiqueta.Height);


            // Dibuja las dos imágenes en el objeto Bitmap combinado
            using (var g = Graphics.FromImage(combinedImage))
            {
                g.DrawImage(bitEtiqueta, 0, 0, 945, 472);
                g.DrawImage(barcode(version, ano, semana, i), 0, 150, 945, 150);
            }

            // Crea un objeto BitmapSource a partir del objeto Bitmap
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                combinedImage.GetHbitmap(),
                IntPtr.Zero,
                System.Windows.Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());


            // guarda nueva pegatina 

            String ind = "";

            if (i < 10)
            {
                ind = "0" + Convert.ToString(i);
            }
            else
            {
                ind = Convert.ToString(i);
            }
            combinedImage.Save(rutaCarpeta + "\\" + "SOL-MOD-ES-" + version + "-" + ano + semana + "-" + ind + ".png");

        }

        // crea codigos de barra
        public static Bitmap barcode(String version, String ano, String semana, int indice)
        {
            BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();
            codigo.IncludeLabel = true;
            codigo.LabelFont = new Font("Gotham", 16);

            String ind = "";

            if (indice < 10)
            {
                ind = "0" + Convert.ToString(indice);
            }
            else
            {
                ind = Convert.ToString(indice);
            }

            // poner el largo del archivo y las coordenadas en x a 0
            System.Drawing.Image co = codigo.Encode(BarcodeLib.TYPE.CODE128, "SOL-MOD-ES-" + version + "-" + ano + semana + "-" + ind, System.Drawing.Color.Black, System.Drawing.Color.Transparent, 945, 150);

            Bitmap bitmapCo = new Bitmap(co);

            return bitmapCo;
        }


    }

}

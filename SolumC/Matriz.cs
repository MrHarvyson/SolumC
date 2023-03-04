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
    class Matriz
    {
        public static Bitmap bitEtiquetaSup, bitEtiquetaInf;

        

        public static void btnGenerarInf(String direccion,String cantidad, String version, String ano, String semana, String rutaCarpeta)
        {
            bitEtiquetaInf = new Bitmap(direccion);

            for (int i = 0; i < Convert.ToInt64(cantidad); i++)
            {
                mergeInf(i,version,ano, semana, rutaCarpeta);
            }
        }

        public static void btnGenerarSup(String direccion, String cantidad, String version, String ano, String semana, String rutaCarpeta)
        {
            bitEtiquetaSup = new Bitmap(direccion);
            for (int i = 0; i < Convert.ToInt64(cantidad); i++)
            {
                mergeSup(i, version, ano, semana, rutaCarpeta);
            }
        }


        public static void mergeInf(int i, String version, String ano, String semana, String rutaCarpeta)
        {
            // Crea un nuevo objeto Bitmap para almacenar las dos imágenes combinadas
            Bitmap combinedImage = new Bitmap(bitEtiquetaInf.Width, bitEtiquetaInf.Height);


            // Dibuja las dos imágenes en el objeto Bitmap combinado
            using (var g = Graphics.FromImage(combinedImage))
            {
                g.DrawImage(bitEtiquetaInf, 0, 0, 591, 886);
                g.DrawImage(barcodeInf(version, ano, semana, i), -5, 500, 600, 100);
            }

            // Crea un objeto BitmapSource a partir del objeto Bitmap
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                combinedImage.GetHbitmap(),
                IntPtr.Zero,
                System.Windows.Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());


            // guarda nueva pegatina 
            combinedImage.Save(rutaCarpeta + "\\" + "SOL-AR-M-INF-" + version + "-" + ano + semana + "-" + i + ".png");

        }

        // crea codigos de barra
        public static Bitmap barcodeInf(String version, String ano, String semana, int indice)
        {
            BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();
            codigo.IncludeLabel = true;
            codigo.LabelFont = new Font("Gotham", 15);


            // poner el largo del archivo y las coordenadas en x a 0
            System.Drawing.Image co = codigo.Encode(BarcodeLib.TYPE.CODE128, "SOL-AR-M-" + version + "-" + ano + semana + "-" + indice, System.Drawing.Color.Black, System.Drawing.Color.Transparent, 600, 100);

            Bitmap bitmapCo = new Bitmap(co);

            return bitmapCo;
        }

        public static void mergeSup(int i, String version, String ano, String semana, String rutaCarpeta)
        {
            // Crea un nuevo objeto Bitmap para almacenar las dos imágenes combinadas
            Bitmap combinedImage = new Bitmap(bitEtiquetaSup.Width, bitEtiquetaSup.Height);


            // Dibuja las dos imágenes en el objeto Bitmap combinado
            using (var g = Graphics.FromImage(combinedImage))
            {
                g.DrawImage(bitEtiquetaSup, 0, 0, 302, 189);
                g.DrawImage(barcodeSup(version, ano, semana, i), 0, 100, 302, 75);
            }

            // Crea un objeto BitmapSource a partir del objeto Bitmap
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                combinedImage.GetHbitmap(),
                IntPtr.Zero,
                System.Windows.Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());


            // guarda nueva pegatina 
            combinedImage.Save(rutaCarpeta + "\\" + "SOL-AR-M-SUP-" + version + "-" + ano + semana + "-" + i + ".png");

        }

        // crea codigos de barra
        public static Bitmap barcodeSup(String version, String ano, String semana, int indice)
        {
            BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();
            codigo.IncludeLabel = true;
            codigo.LabelFont = new Font("Gotham", 10);


            // poner el largo del archivo y las coordenadas en x a 0
            System.Drawing.Image co = codigo.Encode(BarcodeLib.TYPE.CODE128, "SOL-AR-M-" + version + "-" + ano + semana + "-" + indice, System.Drawing.Color.Black, System.Drawing.Color.Transparent, 302, 75);

            Bitmap bitmapCo = new Bitmap(co);

            return bitmapCo;
        }

    }
}

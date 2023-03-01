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
        public static Bitmap bitEtiqueta =  new Bitmap("../../../img/Matriz_inferior.png");

        public static void btnEtiqueta()
        {
            // Crear y configurar el control OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar etiqueta";
            openFileDialog.Filter = "Archivos de imagen (*.png)|*.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            // Mostrar el cuadro de diálogo OpenFileDialog
            if (openFileDialog.ShowDialog() == true)
            {
                // Cargar la imagen en el control Image
                bitEtiqueta = new Bitmap(openFileDialog.FileName);
            }
        }

        public static void btnGenerar(String cantidad, String version, String ano, String semana, String rutaCarpeta)
        {

            for (int i = 0; i < Convert.ToInt64(cantidad); i++)
            {
                merge(i,version,ano, semana, rutaCarpeta);
            }
        }


        public static void merge(int i, String version, String ano, String semana, String rutaCarpeta)
        {
            // Crea un nuevo objeto Bitmap para almacenar las dos imágenes combinadas
            Bitmap combinedImage = new Bitmap(bitEtiqueta.Width, bitEtiqueta.Height);


            // Dibuja las dos imágenes en el objeto Bitmap combinado
            using (var g = Graphics.FromImage(combinedImage))
            {
                g.DrawImage(bitEtiqueta, 0, 0, 591, 886);
                g.DrawImage(barcode(version, ano, semana, i), -5, 490, 600, 100);
            }

            // Crea un objeto BitmapSource a partir del objeto Bitmap
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                combinedImage.GetHbitmap(),
                IntPtr.Zero,
                System.Windows.Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());


            // guarda nueva pegatina 
            combinedImage.Save(rutaCarpeta + "\\" + "SOL-AR-M-" + version + "-" + ano + semana + "-" + i + ".png");

        }

        // crea codigos de barra
        public static Bitmap barcode(String version, String ano, String semana, int indice)
        {
            BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();
            codigo.IncludeLabel = true;
            codigo.LabelFont = new Font("Gotham", 15);


            // poner el largo del archivo y las coordenadas en x a 0
            System.Drawing.Image co = codigo.Encode(BarcodeLib.TYPE.CODE128, "SOL-AR-M-" + version + "-" + ano + semana + "-" + indice, System.Drawing.Color.Black, System.Drawing.Color.Transparent, 600, 100);

            Bitmap bitmapCo = new Bitmap(co);

            return bitmapCo;
        }

    }
}

using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Windows;

namespace SolumC
{
    class Bicicleta

    {

        public static string rutaCarpeta;
        public static Bitmap bitEtiqueta =  new Bitmap("../../../img/bicicleta.png");
        //public static Bitmap bitEtiqueta;
        public static Bitmap[] bitCodigo;
        public static string[] nombreCodigo;


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

        public static void btnCodigos()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar códigos";
            openFileDialog.Filter = "Archivos de imagen (*.png)|*.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                bitCodigo = new Bitmap[openFileDialog.FileNames.Length];
                nombreCodigo = new String[openFileDialog.FileNames.Length];

                for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                {
                    // obtencion del nombre y cmabio de los archivos, en el caso de no tener est dara error
                    nombreCodigo[i] = openFileDialog.SafeFileNames[i];
                    bitCodigo[i] = new Bitmap(openFileDialog.FileNames[i]);

                    //cambiar escala del los codigos
                    /*
                    using (Bitmap redimensionadoBitmap = new Bitmap(ancho/2, largo/2))
                    {
                        using (Graphics graphics = Graphics.FromImage(redimensionadoBitmap))
                        {
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.DrawImage(bitCodigo[i], new System.Drawing.Rectangle(0, 0, ancho/2, largo/2));
                        }
                        bitCodigo[i] = new Bitmap (redimensionadoBitmap);
                    }
                    */

                }
            }

        }


        public static void btnCarpeta()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar carpeta";
            openFileDialog.Filter = "Carpeta|*.";
            openFileDialog.CheckFileExists = false;
            openFileDialog.CheckPathExists = true;
            openFileDialog.FileName = "Seleccionar carpeta";
            if (openFileDialog.ShowDialog() == true)
            {
                rutaCarpeta = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
            }

        }

        public static void btnGenerar()
        {
            //barcode();
            if (bitEtiqueta != null)
            {
                if (bitCodigo != null)
                {
                    if (rutaCarpeta != null)
                    {
                        for (int i = 0; i < bitCodigo.Length; i++)
                        {
                            //
                        }
                        MessageBox.Show("Imagenes creadas correctamente en:" + rutaCarpeta);
                    }
                    else
                    {
                        MessageBox.Show("Seleccione la ruta para guardar");
                    }

                }
                else
                {
                    MessageBox.Show("Seleccione los códigos");
                }

            }
            else
            {
                MessageBox.Show("Seleccione la etiqueta");
            }

        }


        public static void merge(int i)
        {
            // Crea un nuevo objeto Bitmap para almacenar las dos imágenes combinadas
            Bitmap combinedImage = new Bitmap(bitEtiqueta.Width, bitEtiqueta.Height);

            // Crear y configurar el control SaveFileDialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos de imagen PNG (*.png)|*.png";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            saveFileDialog.FileName = rutaCarpeta + i + ".png";


            // Dibuja las dos imágenes en el objeto Bitmap combinado
            using (var g = Graphics.FromImage(combinedImage))
            {
                g.DrawImage(bitEtiqueta, 0, 0, 591, 886);
                g.DrawImage(bitCodigo[i], 50, 500, 488, 100);
            }

            // Crea un objeto BitmapSource a partir del objeto Bitmap
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                combinedImage.GetHbitmap(),
                IntPtr.Zero,
                System.Windows.Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            // Crea un objeto PngBitmapEncoder y añade un frame con el BitmapSource
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

            // cambia el nombre del codigo
            string[] nuevoCodigo = new string[nombreCodigo.Length];

            nuevoCodigo[i] = Regex.Replace(nombreCodigo[i], @"^\d+_", "");

            // guarda nueva pegatina 
            combinedImage.Save(rutaCarpeta + "\\" + nuevoCodigo[i]);

            // en caso de convertir a otro formato como svg
            //svg(combinedImage,i);
        }


        // png to svg
        public void svg(Bitmap bitmap, int j)
        {
            // Cargar imagen PNG como arreglo de bytes
            // byte[] imageBytes = File.ReadAllBytes(saveFileDialog.FileName);
            byte[] imageBytes = BitmapToByteArray(bitmap);
            // Crear objeto MagickImage a partir de la imagen PNG
            using (var magickImage = new MagickImage(imageBytes))
            {
                // Convertir imagen a SVG
                magickImage.Format = MagickFormat.Svg;
                //resolucion svg
                //magickImage.AdaptiveResize(100, 886);
                byte[] svgBytes = magickImage.ToByteArray();

                // Guardar objeto Svg como archivo SVG
                File.WriteAllBytes(rutaCarpeta + "\\" + nombreCodigo[j] + ".svg", svgBytes);
            }
        }

        public byte[] BitmapToByteArray(Bitmap bitmap)
        {
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }


        // crea codigos de barra
        public void barcode()
        {
            BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();
            codigo.IncludeLabel = true;
            codigo.LabelFont = new Font("Gotham", 15);


            // poner el largo del archivo y las coordenadas en x a 0
            System.Drawing.Image co = codigo.Encode(BarcodeLib.TYPE.CODE128, "SOL-AR-M-V1-2308-00", System.Drawing.Color.Black, System.Drawing.Color.White, 488, 100);


            Bitmap bitmapCo = new Bitmap(co);
            bitmapCo.Save("C:\\Users\\josec\\Desktop\\SOL-AR-M-V1-2308-00.png");
        }


    }


}

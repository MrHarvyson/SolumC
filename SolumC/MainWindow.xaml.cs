using ImageMagick;
using Microsoft.Win32;
using SvgLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Interop;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media.Imaging;
using BarcodeLib;
using System.Data.SqlTypes;

namespace SolumC
{
    public partial class MainWindow : Window
    {
        string rutaCarpeta;
        Bitmap bitEtiqueta ;
        Bitmap[] bitCodigo;
        string[] nombreCodigo;

        public MainWindow()
        {
            InitializeComponent();
            //bitEtiqueta = new Bitmap("../../../img/Matriz_inferior.png");
        }

        private void btnEtiqueta_Click(object sender, RoutedEventArgs e)
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

        private void btnCodigos_Click(object sender, RoutedEventArgs e)
        {
            // Crear y configurar el control OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar códigos";
            openFileDialog.Filter = "Archivos de imagen (*.png)|*.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.Multiselect = true;

            // Mostrar el cuadro de diálogo OpenFileDialog
            if (openFileDialog.ShowDialog() == true)
            {
                bitCodigo = new Bitmap[openFileDialog.FileNames.Length];
                nombreCodigo = new String[openFileDialog.FileNames.Length];
                
                
                // Cargar la imagen en el control Image
                for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                {
                    // obtencion del nombre y cmabio de los archivos, en el caso de no tener est dara error
                    nombreCodigo[i]= openFileDialog.SafeFileNames[i].Replace("1_","");
                    bitCodigo[i] = new Bitmap(openFileDialog.FileNames[i]);

                    //cambiar escala
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

        private void btnCarpeta_Click(object sender, RoutedEventArgs e)
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

        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            //barcode();
            if(bitEtiqueta !=null)
            {
                if (bitCodigo != null)
                {
                    if (rutaCarpeta != null)
                    {
                        for (int i = 0; i < bitCodigo.Length; i++)
                        {
                            merge(i);
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

        public void merge(int i)
        {
            // Crea un nuevo objeto Bitmap para almacenar las dos imágenes combinadas
            //Bitmap combinedImage = new Bitmap(bitEtiqueta.Width , bitEtiqueta.Height);
            Bitmap combinedImage = new Bitmap(bitEtiqueta.Width , bitEtiqueta.Height);

            // Crear y configurar el control SaveFileDialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos de imagen PNG (*.png)|*.png";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            saveFileDialog.FileName = rutaCarpeta + i + ".png";


            // Dibuja las dos imágenes en el objeto Bitmap combinado
            using (var g = Graphics.FromImage(combinedImage))
            {
                g.DrawImage(bitEtiqueta, 0, 0, 591,886);
                g.DrawImage(bitCodigo[i],50, 500, 488, 100);
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

            combinedImage.Save(rutaCarpeta + "\\" + nombreCodigo[i] );

            // Guarda la imagen en un archivo PNG
            /*
            using (var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
            {
                encoder.Save(fileStream);
            }
            */
            //svg(combinedImage,i);
        }

        public void svg(Bitmap bitmap, int j)
        {
            // Cargar imagen PNG como arreglo de bytes
            //byte[] imageBytes = File.ReadAllBytes(saveFileDialog.FileName);
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

        public void barcode()
        {
            BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();
            codigo.IncludeLabel = true;
            codigo.LabelFont = new Font("Gotham",15);


            // poner el largo del archivo y las coordenadas en x a 0
            System.Drawing.Image co = codigo.Encode(BarcodeLib.TYPE.CODE128, "SOL-AR-M-V1-2308-00", System.Drawing.Color.Black, System.Drawing.Color.White, 488, 100);
            

            Bitmap bitmapCo = new Bitmap(co);
            bitmapCo.Save("C:\\Users\\josec\\Desktop\\codigo0.png");
        }

    }
}

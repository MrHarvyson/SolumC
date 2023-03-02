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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Interop;
using BarcodeLib;
using System.Data.SqlTypes;

namespace SolumC
{
    public partial class MainWindow : Window
    {
        Bitmap bitEtiqueta ;
        public static string rutaSalida, rutaMatriz, rutaBaldosa, rutaBicicleta, rutaPatinete;

        public MainWindow()
        {
            
            InitializeComponent();

            rutaSalida = Properties.Settings.Default.direccion;
            rutaMatriz = Properties.Settings.Default.direccionMatriz;
            rutaBaldosa = Properties.Settings.Default.direccionBaldosa;
            rutaBicicleta = Properties.Settings.Default.direccionBicicleta;
            rutaPatinete = Properties.Settings.Default.direccionPatinete;


            txtCarpeta.Text = rutaSalida;
            txtCarpetaBaldosa.Text = rutaBaldosa;
            txtCarpetaBicicleta.Text = rutaBicicleta;
            txtCarpetaPatinete.Text = rutaPatinete;
            txtCarpetaMatriz.Text = rutaMatriz;

            txtCarpetaBaldosa.Visibility = Visibility.Collapsed;
            txtCarpetaBicicleta.Visibility = Visibility.Collapsed;
            txtCarpetaPatinete.Visibility = Visibility.Collapsed;
            txtCarpetaMatriz.Visibility = Visibility.Visible;

            imgBaldosa.Visibility = Visibility.Collapsed;
            imgBicicleta.Visibility = Visibility.Collapsed;
            imgPatinete.Visibility = Visibility.Collapsed;
            imgMatriz.Visibility = Visibility.Visible;

        }



        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            if (RbtMatriz.IsChecked == true)
            {
                Matriz.btnGenerar(txtCarpetaMatriz.Text,txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaSalida);
                imgBaldosa.Visibility = Visibility.Collapsed;
                imgBicicleta.Visibility = Visibility.Collapsed;
                imgPatinete.Visibility = Visibility.Collapsed;
                imgMatriz.Visibility = Visibility.Visible;
            }
            if(RbtBicicleta.IsChecked== true)
            {
                Bicicleta.btnGenerar(txtCarpetaBicicleta.Text,txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaSalida);
                imgBaldosa.Visibility = Visibility.Collapsed;
                imgBicicleta.Visibility = Visibility.Visible;
                imgPatinete.Visibility = Visibility.Collapsed;
                imgMatriz.Visibility = Visibility.Collapsed;
            }
            if (RbtBaldosa.IsChecked == true) {
                imgBaldosa.Visibility = Visibility.Visible;
                imgBicicleta.Visibility = Visibility.Collapsed;
                imgPatinete.Visibility = Visibility.Collapsed;
                imgMatriz.Visibility = Visibility.Collapsed;

            }
            if (RbtPatinete.IsChecked == true) {
                Patinete.btnGenerar(txtCarpetaPatinete.Text,txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaSalida);
                imgBaldosa.Visibility = Visibility.Collapsed;
                imgBicicleta.Visibility = Visibility.Collapsed;
                imgPatinete.Visibility = Visibility.Visible;
                imgMatriz.Visibility = Visibility.Collapsed;
            }

            txtCantidad.Text = "";
            txtVersion.Text = "";
            txtAno.Text = "";
            txtSemana.Text = "";
            //txtCarpeta.Text = "";

        }

        private void Carpeta_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar carpeta";
            openFileDialog.Filter = "Carpeta|*.";
            openFileDialog.CheckFileExists = false;
            openFileDialog.CheckPathExists = true;
            openFileDialog.FileName = "Seleccionar carpeta";
            if (openFileDialog.ShowDialog() == true)
            {
                rutaSalida = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
                Properties.Settings.Default.direccion = rutaSalida;
                Properties.Settings.Default.Save();
                txtCarpeta.Text = rutaSalida;
            }
            
        }

        private void Matriz_MouseDown(object sender, MouseButtonEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar etiqueta";
            openFileDialog.Filter = "Archivos de imagen (*.png)|*.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            // Mostrar el cuadro de diálogo OpenFileDialog
            if (openFileDialog.ShowDialog() == true)
            {
                rutaMatriz = openFileDialog.FileName;
                Properties.Settings.Default.direccionMatriz = rutaMatriz;
                Properties.Settings.Default.Save();
                // Cargar la imagen en el control Image
                bitEtiqueta = new Bitmap(openFileDialog.FileName);
                Matriz.btnEtiqueta(bitEtiqueta);
                txtCarpetaMatriz.Text = rutaMatriz;
            }
            
        }

        private void Baldosa_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Bicicleta_MouseDown(object sender, MouseButtonEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar etiqueta";
            openFileDialog.Filter = "Archivos de imagen (*.png)|*.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            // Mostrar el cuadro de diálogo OpenFileDialog
            if (openFileDialog.ShowDialog() == true)
            {
                rutaBicicleta = openFileDialog.FileName;
                Properties.Settings.Default.direccionBicicleta = rutaBicicleta;
                Properties.Settings.Default.Save();
                // Cargar la imagen en el control Image
                bitEtiqueta = new Bitmap(openFileDialog.FileName);
                Bicicleta.btnEtiqueta(bitEtiqueta);
                txtCarpetaBicicleta.Text = rutaBicicleta;
            }
            
        }

        private void Patinete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar etiqueta";
            openFileDialog.Filter = "Archivos de imagen (*.png)|*.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            // Mostrar el cuadro de diálogo OpenFileDialog
            if (openFileDialog.ShowDialog() == true)
            {
                rutaPatinete = openFileDialog.FileName;
                Properties.Settings.Default.direccionPatinete = rutaPatinete;
                Properties.Settings.Default.Save();
                // Cargar la imagen en el control Image
                bitEtiqueta = new Bitmap(openFileDialog.FileName);
                Patinete.btnEtiqueta(bitEtiqueta);
                txtCarpetaPatinete.Text = rutaPatinete;
            }
            
        }

        private void RbtBicicleta_Click(object sender, RoutedEventArgs e)
        {
            imgBaldosa.Visibility = Visibility.Collapsed;
            imgBicicleta.Visibility = Visibility.Visible;
            imgPatinete.Visibility = Visibility.Collapsed;
            imgMatriz.Visibility = Visibility.Collapsed;

            txtCarpetaBaldosa.Visibility = Visibility.Collapsed;
            txtCarpetaBicicleta.Visibility = Visibility.Visible;
            txtCarpetaPatinete.Visibility = Visibility.Collapsed;
            txtCarpetaMatriz.Visibility = Visibility.Collapsed;

        }

        private void RbtMatriz_Click(object sender, RoutedEventArgs e)
        {
            imgBaldosa.Visibility = Visibility.Collapsed;
            imgBicicleta.Visibility = Visibility.Collapsed;
            imgPatinete.Visibility = Visibility.Collapsed;
            imgMatriz.Visibility = Visibility.Visible;

            txtCarpetaBaldosa.Visibility = Visibility.Collapsed;
            txtCarpetaBicicleta.Visibility = Visibility.Collapsed;
            txtCarpetaPatinete.Visibility = Visibility.Collapsed;
            txtCarpetaMatriz.Visibility = Visibility.Visible;

        }

        private void RbtPatinete_Click(object sender, RoutedEventArgs e)
        {
            imgBaldosa.Visibility = Visibility.Collapsed;
            imgBicicleta.Visibility = Visibility.Collapsed;
            imgPatinete.Visibility = Visibility.Visible;
            imgMatriz.Visibility = Visibility.Collapsed;


            txtCarpetaBaldosa.Visibility = Visibility.Collapsed;
            txtCarpetaBicicleta.Visibility = Visibility.Collapsed;
            txtCarpetaPatinete.Visibility = Visibility.Visible;
            txtCarpetaMatriz.Visibility = Visibility.Collapsed;

        }
    }
}

using ImageMagick;
using Microsoft.Win32;
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
        Bitmap bitEtiqueta, bitEtiquetaSup, bitEtiquetaInf;
        public static string rutaSalida, rutaMatrizSup, rutaMatrizInf, rutaBaldosa, rutaBicicleta, rutaPatinete, preCodigo;

        public MainWindow()
        {
            
            InitializeComponent();

            rutaSalida = Properties.Settings.Default.direccion;
            rutaMatrizSup = Properties.Settings.Default.direccionMatrizSup;
            rutaMatrizInf = Properties.Settings.Default.direccionMatrizInf;
            rutaBaldosa = Properties.Settings.Default.direccionBaldosa;
            rutaBicicleta = Properties.Settings.Default.direccionBicicleta;
            rutaPatinete = Properties.Settings.Default.direccionPatinete;


            txtCarpeta.Text = rutaSalida;
            txtCarpetaBaldosa.Text = rutaBaldosa;
            txtCarpetaBicicleta.Text = rutaBicicleta;
            txtCarpetaPatinete.Text = rutaPatinete;
            txtCarpetaMatrizInf.Text = rutaMatrizInf;
            txtCarpetaMatrizSup.Text = rutaMatrizSup;

            preCodigo = "SOL - AR - M -";
            txtPrecodigo.Text = preCodigo;

            panelPrincipal.Visibility= Visibility.Visible;
            panelAjustes.Visibility = Visibility.Collapsed;

        }



        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            if (RbtMatriz.IsChecked == true)
            {
                Matriz.btnGenerarInf(Properties.Settings.Default.direccionMatrizInf, txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaSalida);
                Matriz.btnGenerarSup(Properties.Settings.Default.direccionMatrizSup, txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaSalida);
            }
            if(RbtBicicleta.IsChecked== true)
            {
                Bicicleta.btnGenerar(Properties.Settings.Default.direccionBicicleta, txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaSalida);
            }
            if (RbtBaldosa.IsChecked == true) 
            {
                Baldosa.btnGenerar(Properties.Settings.Default.direccionBaldosa, txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaSalida);
            }
            if (RbtPatinete.IsChecked == true) 
            {
                Patinete.btnGenerar(Properties.Settings.Default.direccionPatinete, txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaSalida);
            }

            txtCantidad.Text = "";
            txtVersion.Text = "";
            txtAno.Text = "";
            txtSemana.Text = "";

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

        private void MatrizSup_MouseDown(object sender, MouseButtonEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar etiqueta";
            openFileDialog.Filter = "Archivos de imagen (*.png)|*.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            // Mostrar el cuadro de diálogo OpenFileDialog
            if (openFileDialog.ShowDialog() == true)
            {
                rutaMatrizSup = openFileDialog.FileName;
                Properties.Settings.Default.direccionMatrizSup = rutaMatrizSup;
                Properties.Settings.Default.Save();
                // Cargar la imagen en el control Image
                bitEtiquetaSup = new Bitmap(openFileDialog.FileName);
                txtCarpetaMatrizSup.Text = rutaMatrizSup;
            }
            
        }

        private void MatrizInf_MouseDown(object sender, MouseButtonEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar etiqueta";
            openFileDialog.Filter = "Archivos de imagen (*.png)|*.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            // Mostrar el cuadro de diálogo OpenFileDialog
            if (openFileDialog.ShowDialog() == true)
            {
                rutaMatrizInf = openFileDialog.FileName;
                Properties.Settings.Default.direccionMatrizInf = rutaMatrizInf;
                Properties.Settings.Default.Save();
                // Cargar la imagen en el control Image
                bitEtiquetaInf = new Bitmap(openFileDialog.FileName);
                txtCarpetaMatrizInf.Text = rutaMatrizInf;
            }

        }

        private void Baldosa_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar etiqueta";
            openFileDialog.Filter = "Archivos de imagen (*.png)|*.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            // Mostrar el cuadro de diálogo OpenFileDialog
            if (openFileDialog.ShowDialog() == true)
            {
                rutaBaldosa = openFileDialog.FileName;
                Properties.Settings.Default.direccionBaldosa = rutaBaldosa;
                Properties.Settings.Default.Save();
                // Cargar la imagen en el control Image
                bitEtiqueta = new Bitmap(openFileDialog.FileName);
                //Baldosa.btnEtiqueta(bitEtiqueta);
                txtCarpetaBaldosa.Text = rutaBaldosa;
            }
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
                txtCarpetaBicicleta.Text = rutaBicicleta;
            }
            
        }

        private void Ajustes_MouseDown(object sender, MouseButtonEventArgs e)
        {
            panelPrincipal.Visibility = Visibility.Collapsed;
            panelAjustes.Visibility = Visibility.Visible;
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
                txtCarpetaPatinete.Text = rutaPatinete;
            }
            
        }

        private void RbtBicicleta_Click(object sender, RoutedEventArgs e)
        {

            txtPrecodigo.Text = "SOL - MOD - EB -";

            panelPrincipal.Visibility = Visibility.Visible;

        }

        private void RbtMatriz_Click(object sender, RoutedEventArgs e)
        {

            txtPrecodigo.Text = "SOL - AR - M -";

            panelPrincipal.Visibility = Visibility.Visible;
            panelAjustes.Visibility = Visibility.Collapsed;

        }

        private void RbtPatinete_Click(object sender, RoutedEventArgs e)
        {

            txtPrecodigo.Text = "SOL - MOD - ES -";

            panelPrincipal.Visibility = Visibility.Visible;
            panelAjustes.Visibility = Visibility.Collapsed;

        }

        private void RbtBaldosa_Click(object sender, RoutedEventArgs e)
        {

            txtPrecodigo.Text = "SOL - AR - B -";

            panelPrincipal.Visibility = Visibility.Visible;
            panelAjustes.Visibility = Visibility.Collapsed;
        }
    }
}

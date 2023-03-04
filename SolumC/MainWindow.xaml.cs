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
        public static string rutaSalida, rutaMatrizSup, rutaMatrizInf, rutaBaldosa, rutaBicicleta, rutaPatinete, preCodigo,
            rutaRef1, rutaRef2, rutaRef3, rutaRef4, rutaRef5;

        public MainWindow()
        {
            
            InitializeComponent();

            rutaSalida = Properties.Settings.Default.direccion;
            rutaMatrizSup = Properties.Settings.Default.direccionMatrizSup;
            rutaMatrizInf = Properties.Settings.Default.direccionMatrizInf;
            rutaBaldosa = Properties.Settings.Default.direccionBaldosa;
            rutaBicicleta = Properties.Settings.Default.direccionBicicleta;
            rutaPatinete = Properties.Settings.Default.direccionPatinete;
            rutaRef1 = Properties.Settings.Default.direccionRef1;
            rutaRef2 = Properties.Settings.Default.direccionRef2;
            rutaRef3 = Properties.Settings.Default.direccionRef3;
            rutaRef4 = Properties.Settings.Default.direccionRef4;
            rutaRef5 = Properties.Settings.Default.direccionRef5;


            txtCarpeta.Text = rutaSalida;
            txtCarpetaBaldosa.Text = rutaBaldosa;
            txtCarpetaBicicleta.Text = rutaBicicleta;
            txtCarpetaPatinete.Text = rutaPatinete;
            txtCarpetaMatrizInf.Text = rutaMatrizInf;
            txtCarpetaMatrizSup.Text = rutaMatrizSup;
            txtCarpetaRef1.Text= rutaRef1;
            txtCarpetaRef2.Text= rutaRef2;
            txtCarpetaRef3.Text= rutaRef3;
            txtCarpetaRef4.Text= rutaRef4;
            txtCarpetaRef5.Text= rutaRef5;

            preCodigo = "SOL - AR - M -";
            txtPrecodigo.Text = preCodigo;

            panelPrincipal.Visibility= Visibility.Visible;
            panelAjustes.Visibility = Visibility.Collapsed;
            gridPrecodigoPerimetro.Visibility = Visibility.Collapsed;

        }

        

        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            if (RbtMatriz.IsChecked == true)
            {
                if(txtCantidad.Text == "" | txtVersion.Text == "" | txtAno.Text == "" | txtSemana.Text == "" | Properties.Settings.Default.direccionMatrizInf == "" | Properties.Settings.Default.direccionMatrizSup == "" | rutaSalida == "")
                {
                    MessageBox.Show("Faltan campos por completar.");
                }
                else
                {
                    Matriz.btnGenerarInf(Properties.Settings.Default.direccionMatrizInf, txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaSalida);
                    Matriz.btnGenerarSup(Properties.Settings.Default.direccionMatrizSup, txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaSalida);
                }
            }
            if(RbtBicicleta.IsChecked== true)
            {

                if (txtCantidad.Text == "" | txtVersion.Text == "" | txtAno.Text == "" | txtSemana.Text == "" | Properties.Settings.Default.direccionBicicleta == "" | rutaSalida == "")
                {
                    MessageBox.Show("Faltan campos por completar.");
                }
                else
                {
                    Bicicleta.btnGenerar(Properties.Settings.Default.direccionBicicleta, txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaSalida);
                }
            }
            if (RbtBaldosa.IsChecked == true) 
            {
                if (txtCantidad.Text == "" | txtVersion.Text == "" | txtAno.Text == "" | txtSemana.Text == "" | Properties.Settings.Default.direccionBaldosa == "" | rutaSalida == "")
                {
                    MessageBox.Show("Faltan campos por completar.");
                }
                else
                {
                    Baldosa.btnGenerar(Properties.Settings.Default.direccionBaldosa, txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaSalida);
                }
            }
            if (RbtPatinete.IsChecked == true) 
            {
                if (txtCantidad.Text == "" | txtVersion.Text == "" | txtAno.Text == "" | txtSemana.Text == "" | Properties.Settings.Default.direccionPatinete == "" | rutaSalida == "")
                {
                    MessageBox.Show("Faltan campos por completar.");
                }
                else
                {
                    Patinete.btnGenerar(Properties.Settings.Default.direccionPatinete, txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaSalida);
                }
            }
            if(RbtRef.IsChecked == true)
            {
                if (txtCantidad.Text == "" | txtVersion.Text == "" | txtAno.Text == "" | txtSemana.Text == "" | Properties.Settings.Default.direccionRef1 == "" 
                    | Properties.Settings.Default.direccionRef2 == ""| Properties.Settings.Default.direccionRef3 == "" | Properties.Settings.Default.direccionRef4 == "" | 
                    Properties.Settings.Default.direccionRef5 == "" | rutaSalida == "" | txtRef.Text == "")
                {
                    MessageBox.Show("Faltan campos por completar.");
                }
                else
                {
                    if (txtRef.Text == "1")
                    {
                        Ref.btnGenerar(Properties.Settings.Default.direccionRef1, txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaSalida, txtRef.Text);
                    }
                    if (txtRef.Text == "2")
                    {
                        Ref.btnGenerar(Properties.Settings.Default.direccionRef2, txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaSalida, txtRef.Text);
                    }
                    if (txtRef.Text == "3")
                    {
                        Ref.btnGenerar(Properties.Settings.Default.direccionRef3, txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaSalida, txtRef.Text);
                    }
                    if (txtRef.Text == "4")
                    {
                        Ref.btnGenerar(Properties.Settings.Default.direccionRef4, txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaSalida, txtRef.Text);
                    }
                    if (txtRef.Text == "5")
                    {
                        Ref.btnGenerar(Properties.Settings.Default.direccionRef5, txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaSalida, txtRef.Text);
                    }
                }
                
            }

            txtCantidad.Text = "";
            txtVersion.Text = "";
            txtAno.Text = "";
            txtSemana.Text = "";
            txtRef.Text = "";

        }

        private void Carpeta_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar carpeta de salida";
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
            openFileDialog.Title = "Seleccionar etiqueta de matriz superior";
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
            openFileDialog.Title = "Seleccionar etiqueta de matriz inferior";
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
            openFileDialog.Title = "Seleccionar etiqueta de baldosa";
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
            openFileDialog.Title = "Seleccionar etiqueta de bicicleta";
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
            openFileDialog.Title = "Seleccionar etiqueta de patinete";
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

        private void imgRef1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar etiqueta de perimetro 1";
            openFileDialog.Filter = "Archivos de imagen (*.png)|*.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            // Mostrar el cuadro de diálogo OpenFileDialog
            if (openFileDialog.ShowDialog() == true)
            {
                rutaRef1 = openFileDialog.FileName;
                Properties.Settings.Default.direccionRef1 = rutaRef1;
                Properties.Settings.Default.Save();
                // Cargar la imagen en el control Image
                bitEtiqueta = new Bitmap(openFileDialog.FileName);
                txtCarpetaRef1.Text = rutaRef1;
            }
        }

        private void imgRef2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar etiqueta de perimetro 2";
            openFileDialog.Filter = "Archivos de imagen (*.png)|*.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            // Mostrar el cuadro de diálogo OpenFileDialog
            if (openFileDialog.ShowDialog() == true)
            {
                rutaRef2 = openFileDialog.FileName;
                Properties.Settings.Default.direccionRef2 = rutaRef2;
                Properties.Settings.Default.Save();
                // Cargar la imagen en el control Image
                bitEtiqueta = new Bitmap(openFileDialog.FileName);
                txtCarpetaRef2.Text = rutaRef2;
            }
        }

        private void imgRef3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar etiqueta de perimetro 3";
            openFileDialog.Filter = "Archivos de imagen (*.png)|*.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            // Mostrar el cuadro de diálogo OpenFileDialog
            if (openFileDialog.ShowDialog() == true)
            {
                rutaRef3 = openFileDialog.FileName;
                Properties.Settings.Default.direccionRef3 = rutaRef3;
                Properties.Settings.Default.Save();
                // Cargar la imagen en el control Image
                bitEtiqueta = new Bitmap(openFileDialog.FileName);
                txtCarpetaRef3.Text = rutaRef3;
            }
        }

        private void imgRef4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar etiqueta de perimetro 4";
            openFileDialog.Filter = "Archivos de imagen (*.png)|*.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            // Mostrar el cuadro de diálogo OpenFileDialog
            if (openFileDialog.ShowDialog() == true)
            {
                rutaRef4 = openFileDialog.FileName;
                Properties.Settings.Default.direccionRef4 = rutaRef4;
                Properties.Settings.Default.Save();
                // Cargar la imagen en el control Image
                bitEtiqueta = new Bitmap(openFileDialog.FileName);
                txtCarpetaRef4.Text = rutaRef4;
            }
        }

        private void imgRef5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar etiqueta de perimetro 5";
            openFileDialog.Filter = "Archivos de imagen (*.png)|*.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            // Mostrar el cuadro de diálogo OpenFileDialog
            if (openFileDialog.ShowDialog() == true)
            {
                rutaRef5 = openFileDialog.FileName;
                Properties.Settings.Default.direccionRef5 = rutaRef5;
                Properties.Settings.Default.Save();
                // Cargar la imagen en el control Image
                bitEtiqueta = new Bitmap(openFileDialog.FileName);
                txtCarpetaRef5.Text = rutaRef5;
            }
        }

        private void RbtBicicleta_Click(object sender, RoutedEventArgs e)
        {

            txtPrecodigo.Text = "SOL - MOD - EB -";

            panelPrincipal.Visibility = Visibility.Visible;
            gridPrecodigoPerimetro.Visibility = Visibility.Collapsed;
            txtPrecodigo.Visibility = Visibility.Visible;

        }

        private void RbtMatriz_Click(object sender, RoutedEventArgs e)
        {

            txtPrecodigo.Text = "SOL - AR - M -";

            panelPrincipal.Visibility = Visibility.Visible;
            panelAjustes.Visibility = Visibility.Collapsed;
            gridPrecodigoPerimetro.Visibility = Visibility.Collapsed;
            txtPrecodigo.Visibility = Visibility.Visible;

        }

        private void RbtPatinete_Click(object sender, RoutedEventArgs e)
        {

            txtPrecodigo.Text = "SOL - MOD - ES -";

            panelPrincipal.Visibility = Visibility.Visible;
            panelAjustes.Visibility = Visibility.Collapsed;
            gridPrecodigoPerimetro.Visibility = Visibility.Collapsed;
            txtPrecodigo.Visibility = Visibility.Visible;

        }

        private void RbtBaldosa_Click(object sender, RoutedEventArgs e)
        {

            txtPrecodigo.Text = "SOL - AR - B -";

            panelPrincipal.Visibility = Visibility.Visible;
            panelAjustes.Visibility = Visibility.Collapsed;
            gridPrecodigoPerimetro.Visibility = Visibility.Collapsed;
            txtPrecodigo.Visibility = Visibility.Visible;
        }

        private void RbtRef_Click(object sender, RoutedEventArgs e)
        {
            txtPrecodigoPerimetro.Text = "SOL - AR - REF";

            gridPrecodigoPerimetro.Visibility = Visibility.Visible;
            txtPrecodigo.Visibility = Visibility.Collapsed;
            panelPrincipal.Visibility = Visibility.Visible;
            panelAjustes.Visibility = Visibility.Collapsed;
        }
    }
}

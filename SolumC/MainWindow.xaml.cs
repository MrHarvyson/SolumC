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
        public static string rutaCarpeta;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEtiqueta_Click(object sender, RoutedEventArgs e)
        {
            Matriz.btnEtiqueta();
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
            if (RbtMatriz.IsChecked == true)
            {
                Matriz.btnGenerar(txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaCarpeta);
            }
            if(RbtBicicleta.IsChecked== true)
            {
                Bicicleta.btnGenerar(txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaCarpeta);
            }
            if (RbtBaldosa.IsChecked == true) {
            
            }
            if (RbtPatinete.IsChecked == true) {
                Patinete.btnGenerar(txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text, rutaCarpeta);
            }
            
            
        }

        
    }
}

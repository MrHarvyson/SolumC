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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEtiqueta_Click(object sender, RoutedEventArgs e)
        {
            Matriz.btnEtiqueta();
        }

        private void btnCodigos_Click(object sender, RoutedEventArgs e)
        {
            Matriz.btnCodigos();
        }

        private void btnCarpeta_Click(object sender, RoutedEventArgs e)
        {

            Matriz.btnCarpeta();
        }

        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            if (RbtMatriz.IsChecked == true)
            {
                Matriz.btnGenerar(txtCantidad.Text, txtVersion.Text, txtAno.Text, txtSemana.Text);
            }
            if(RbtBicicleta.IsChecked== true)
            {
                Bicicleta.btnGenerar();
            }
            
            
        }



        private void RbtMatriz_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RbtBaldosa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RbtBicicleta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RbtPatinete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

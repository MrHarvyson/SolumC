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
            Matriz.btnGenerar();
            
        }


       

        public void barcode()
        {
            BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();
            codigo.IncludeLabel = true;
            codigo.LabelFont = new Font("Gotham",15);


            // poner el largo del archivo y las coordenadas en x a 0
            System.Drawing.Image co = codigo.Encode(BarcodeLib.TYPE.CODE128, "SOL-AR-M-V1-2308-00", System.Drawing.Color.Black, System.Drawing.Color.White, 488, 100);
            

            Bitmap bitmapCo = new Bitmap(co);
            bitmapCo.Save("C:\\Users\\josec\\Desktop\\SOL-AR-M-V1-2308-00.png");
        }

    }
}

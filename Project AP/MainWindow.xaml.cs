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

namespace Project_AP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private void Refresh_Method()
        {
            FirstName.Text = string.Empty;
            LastName.Text = string.Empty;
            NationalCode.Text = string.Empty;
            CarBrand.Text = string.Empty;
            CarDate.Text = string.Empty;
            CarPlate.Text = string.Empty;
            NumofPassenger.SelectedIndex = 0;
            Pass_Age.Text = string.Empty;
        }

        public MainWindow()
        {
            InitializeComponent();

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Refresh_Method();
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            Refresh_Method();
        }

    }

}

using Project_AP.Class;
using System;
using System.IO;
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
        string path = @"c:C:\Users\hosei\source\repos\Project AP\Files\Files.txt";
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
        private void Save_Method()
        {
            var user = new UserInfo();
            user.FirstName = FirstName.ToString();
            user.LastName = LastName.ToString();
            user.NationalCode = NationalCode.ToString();
            user.CarBrand = CarBrand.ToString();
            user.CarDate = CarDate.ToString();
            user.CarPlate = CarPlate.ToString();
            user.NumPass = NumofPassenger.ToString();
            user.PassAge = Pass_Age.ToString();
            File.AppendAllText(path, user.FirstName);

            
            
        }

        public MainWindow()
        {
            InitializeComponent();

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Save_Method();
            Refresh_Method();
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            Refresh_Method();
        }

    }

}

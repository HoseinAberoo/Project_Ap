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
        string path = @"C:\Users\hosei\source\repos\Project AP\Files\Files.txt";
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
        private void Append_Method(string text)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(text);
                
            }
        }
        private void Save_Method()
        {
            var user = new UserInfo();
            user.FirstName = FirstName.ToString().Remove(0, 33);
            user.LastName = LastName.ToString().Remove(0, 33);
            user.NationalCode = NationalCode.ToString().Remove(0, 33);
            user.CarBrand = CarBrand.ToString().Remove(0, 33);
            user.CarDate = CarDate.ToString().Remove(0, 33);
            user.CarPlate = CarPlate.ToString().Remove(0, 33);
            user.NumPass = NumofPassenger.ToString().Remove(0, 45);
            user.PassAge = Pass_Age.ToString().Remove(0, 33);
            Append_Method(user.FirstName);
            Append_Method(user.LastName);
            Append_Method(user.NationalCode);
            Append_Method(user.CarBrand);
            Append_Method(user.CarDate);
            Append_Method(user.CarPlate);
            Append_Method(user.NumPass);
            Append_Method(user.PassAge);

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

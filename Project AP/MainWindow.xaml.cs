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
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;

namespace Project_AP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        string path = @"C:\Users\hosei\source\repos\Project AP\Files\Files.csv";
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
            // TODO: Ensure headers.

            var user = new List<UserInfo>
            {
                new UserInfo {
                    FirstName    = FirstName.Text.ToString(),
                    LastName     = LastName.Text.ToString(),
                    NationalCode = NationalCode.Text.ToString(),
                    CarBrand     = CarBrand.Text.ToString(),
                    CarDate      = CarDate.Text.ToString(),
                    CarPlate     = CarPlate.Text.ToString(),
                    NumPass      = NumofPassenger.Text.ToString(),
                    PassAge      = Pass_Age.Text.ToString()
                },
            };
           
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header again.
                HasHeaderRecord = false,
            };
            using (var stream = File.Open(path, FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(user);
            }
            //user.FirstName = FirstName.ToString().Remove(0, 33);
            //user.LastName = LastName.ToString().Remove(0, 33);
            //user.NationalCode = NationalCode.ToString().Remove(0, 33);
            //user.CarBrand = CarBrand.ToString().Remove(0, 33);
            //user.CarDate = CarDate.ToString().Remove(0, 33);
            //user.CarPlate = CarPlate.ToString().Remove(0, 33);
            //user.NumPass = NumofPassenger.ToString().Remove(0, 45);
            ////user.PassAge = Pass_Age.ToString().Remove(0, 33);

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

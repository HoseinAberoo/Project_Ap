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
using System.Text.RegularExpressions;
using System.Data;

namespace Project_AP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        string path = @"C:\Users\hosei\source\repos\Project AP\Files\Files.csv";
        DataTable csvData;
        
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
        private bool PlateAndName()
        {
            return true;
        }
        private DataTable Load_Method()
        {

            // NOTE: This means that "headers" are required
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = (
                    string header,
                    int index
                ) => header.ToLower()
            };
            
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, config))
            {
                using (var dr = new CsvDataReader(csv))
                {
                    var dt = new DataTable();
                    dt.Load(dr);
                    dataGrid.DataContext = dt.DefaultView;
                    return dt;
                }
                
            }
        }
        private void Save_Method()
        {
            
            // TODO: Ensure headers.

            var user = new List<UserInfo>
            {
                new UserInfo {
                    FirstName    = FirstName.Text,
                    LastName     = LastName.Text,
                    NationalCode = NationalCode.Text,
                    CarBrand     = CarBrand.Text,
                    CarDate      = CarDate.Text,
                    CarPlate     = CarPlate.Text,
                    NumPass      = NumofPassenger.Text,
                    PassAge      = Pass_Age.Text,
                    Date         = DateTime.Now.Date                 
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
            Refresh_Method();

        }
        private bool Validate_Input()
        {
            if(          FirstName.Text.Trim()==""||  LastName.Text.Trim()==""
               ||  NationalCode.Text.Trim() == "" || CarBrand.Text.Trim() == ""
               ||       CarDate.Text.Trim() == "" || CarPlate.Text.Trim() == ""
               ||NumofPassenger.Text.Trim() == "" || Pass_Age.Text.Trim() == "")
            {
                Message_Box("all fields are required");
                return false;
            }
            return true;
        }
        private void Message_Box(string Error)
        {
            MessageBox.Show(Error);
        }
        private bool Plate_valid(string Plate)
        {
            if (Plate.Length!=8)
            {
                Message_Box("Plate is under 8 char !!!!");
                return false; ;
            }
            Regex alpha = new Regex("^[A-z]*$");
            if(!alpha.IsMatch(Plate[2].ToString()))
            {
                Message_Box("third char is not alphabet!!!!");
                return false;
            }
            Regex numeric = new Regex("^[1-9]*$");
            if (!numeric.IsMatch(Plate.Substring(0,1)) || !numeric.IsMatch(Plate.Substring(3))) 
            {
                Message_Box("Invalid input (using char instead of number !!!!)");
                return false;
            }
            return true;
        }
        public MainWindow()
        {
            InitializeComponent();
            csvData = Load_Method();

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
           
            var res2 = Validate_Input();
            var res1 = Plate_valid(CarPlate.Text);
            if (res1 == true && res2 == true)
            {
                Save_Method();
            }
          
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            Refresh_Method();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}

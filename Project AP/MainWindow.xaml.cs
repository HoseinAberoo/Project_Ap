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
        string nativeplatesquery = "";
        string nonnativeplatesquery = "";
        int count = 0;


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
                    Date         = DateTime.Now.Date,
                    IsOdd        = true
                    
                },
            };
            if (Convert.ToInt32(CarPlate.Text[5])%2==0)
            {
                user[0].IsOdd = false;
            }
           
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
            Regex numeric = new Regex("^[0-9]*$");
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
            FirstDate.SelectedDate = DateTime.Now;
            LastDate.SelectedDate = DateTime.Now;

        }
        private void GenerateNativePlates()
        {
            string qstring = "";
            List<int> platenumbers = new List<int>() { 11, 22, 33, 44, 55, 66, 77, 88, 99, 10, 20, 30};

            foreach (var pn in platenumbers)
            {
                nativeplatesquery += $"CarPlate LIKE '%{pn}' OR ";
            }
            int place = nativeplatesquery.LastIndexOf(" OR ");

            if (place == -1)
                return;

            nativeplatesquery = nativeplatesquery.Remove(place, " OR ".Length).Insert(place, "");
        }
        private void GenericNotNative()
        {
            string qstring = "";
            for (int pn=10; pn <100; pn++)
            {
                if (pn % 11 != 0 && pn %10!=0)
                {
                    nonnativeplatesquery += $"CarPlate  LIKE '%{pn}' OR ";

                }
            }
            int place = nonnativeplatesquery.LastIndexOf(" OR ");

            if (place == -1)
                return;

            nonnativeplatesquery = nonnativeplatesquery.Remove(place, " OR ".Length).Insert(place, "");
        }
        private void Count_Method(ref int count)
        {
            count++;
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
           
            var res2 = Validate_Input();
            var res1 = Plate_valid(CarPlate.Text);
            if (res1 == true && res2 == true)
            {
                Save_Method();
                Load_Method();
                Count_Method(ref count);
            }
          
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            Refresh_Method();
        }
        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nativeplatesquery == "")
                GenerateNativePlates();
            if (nonnativeplatesquery == "")
                GenericNotNative();

            // Note: Basically a search operation on the column 'CarBrand'
            //csvData.DefaultView.RowFilter = string.Format(
            //    "CarBrand LIKE '%{0}%' AND NumPass >={1} AND NumPass<={2}" +
            //    "AND Date >= #{3}# AND Date <= #{4}#" +
            //    (SNative.Text == "Yes" ? "AND CarPlate LIKE '%99' " : ""),
            //    SCarBrand.Text,
            //    Convert.ToInt32(SMinPass.Text),
            //    Convert.ToInt32(SMaxPass.Text),
            //    FirstDate.SelectedDate,
            //    LastDate.SelectedDate

            //);
            string q = $"" +
                $"CarBrand LIKE '%{SCarBrand.Text}%' AND " +

                $"NumPass >= {Convert.ToInt32(SMinPass.Text)} AND " +
                $"NumPass <= {Convert.ToInt32(SMaxPass.Text)} AND " +

                $"Date >= #{FirstDate.SelectedDate}# AND Date <= #{LastDate.SelectedDate}# " +

                $"{(SNative.Text == "Yes" ? $"AND ({nativeplatesquery})" : "")} " +
                $"{(SNative.Text == "No" ? $"AND ({nonnativeplatesquery})" : "")} " +

                $"{(Soddoreven.Text == "Odd" ? "AND IsOdd = 'true'" : "")}" +
                $"{(Soddoreven.Text == "Even" ? "AND IsOdd = 'false'" : "")}";


            csvData.DefaultView.RowFilter = q;

        }

        private void CountInBtn_Click(object sender, RoutedEventArgs e)
        {
            int countin = 2 * count / 3;
            MessageBox.Show("Count In = {0}",countin.ToString());
        }

        private void CountOutBtn_Click(object sender, RoutedEventArgs e)
        {
            int countin =  count / 3;
            MessageBox.Show("Count Out = {0}", countin.ToString());

        }
    }

}

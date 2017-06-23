using ElectronicRaffle;
using ElectronicRaffle.CommonUi;
using ElectronicRaffle.CommonUi.Controls;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace ElectronicRaffle.MainApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var fullAccess = Convert.ToBoolean(ConfigurationManager.AppSettings["FullAccess"]);

            Header.Visibility = fullAccess ? Visibility.Visible : Visibility.Collapsed;
        }

        private void DisplaySelectedMenuItem()
        {
            tbtnMenu.IsChecked = false;

            var SelectedMenuItem = (MenuItem)lbxMenu.SelectedItem;

            if (SelectedMenuItem == RegisterNewMenuItem)
            {
                DialogHost.Show(new RegisterNewTeacherControl(), "RootDialog");
            }
            if (SelectedMenuItem == TagApplicationMenuItem)
            {
                DialogHost.Show(new ApplicantTaggingControl(), "RootDialog");
            }
        }

        private void lbxMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplaySelectedMenuItem();
        }

        private void lbxMenu_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DisplaySelectedMenuItem();
        }
    }
}

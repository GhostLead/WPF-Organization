using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfAppOrganization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Organization> szervezetek = new List<Organization>();
        private void Betoltes(string filename)
        {
            foreach (var sor in File.ReadAllLines(filename).Skip(1))
            {
                szervezetek.Add(new Organization(sor.Split(';')));
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            Betoltes("C:\\Users\\barizs.marton.daniel\\source\\repos\\WpfAppOrganization\\WpfAppOrganization\\organizations-100000.csv");
            dgAdatok.ItemsSource = szervezetek;

            cbOrszag.ItemsSource = szervezetek.Select(x => x.Country).OrderBy(x => x).Distinct().ToList();
            cbEv.ItemsSource = szervezetek.Select(x => x.Founded).OrderBy(x => x).Distinct().ToList();
            lblLetszam.Content = szervezetek.Sum(x => x.EmployeesNumber);
        }

        private void dgAdatok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAdatok.SelectedItem is Organization)
            {
                Organization selectedObject = dgAdatok.SelectedItem as Organization;
                lblID.Content ="Azonosito: " + selectedObject.Id.ToString();
                lblWEB.Content = "Webhely: " + selectedObject.Website;
                lblISM.Content = "Leiras: " + selectedObject.Description;
            }
            else
            {
                MessageBox.Show("hiba!");
            }
        }

        private void cbOrszagSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var szurtlista = szervezetek.Where(x => x.Country == cbOrszag.SelectedItem.ToString());

            if (cbOrszag.SelectedIndex == -1)
            {
                dgAdatok.ItemsSource = szervezetek;
            }
            else if (cbEv.SelectedIndex != -1 || cbEv.SelectedIndex == cbEv.Items.Count - 1)
            {
                szurtlista = szervezetek.Where(x => x.Founded.ToString() == cbEv.SelectedItem.ToString() && x.Country.ToString() == cbOrszag.SelectedItem.ToString()).ToList();
                dgAdatok.ItemsSource = szurtlista;
                lblLetszam.Content = szurtlista.Sum(x => x.EmployeesNumber);
            }
            else
            {
                
                dgAdatok.ItemsSource = szurtlista;
                lblLetszam.Content = szurtlista.Sum(x => x.EmployeesNumber);
            }


        }

        private void cbEvSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var szurtlista = szervezetek.Where(x => x.Founded.ToString() == cbEv.SelectedItem.ToString()).ToList();

            if (cbEv.SelectedIndex == -1)
            {
                dgAdatok.ItemsSource = szervezetek;
            }
            else if(cbOrszag.SelectedIndex != -1 || cbOrszag.SelectedIndex == cbOrszag.Items.Count - 1)
            {
                szurtlista = szervezetek.Where(x => x.Founded.ToString() == cbEv.SelectedItem.ToString() && x.Country.ToString() == cbOrszag.SelectedItem.ToString()).ToList();
                dgAdatok.ItemsSource = szurtlista;
                lblLetszam.Content = szurtlista.Sum(x => x.EmployeesNumber);
            }
            else
            {
                
                dgAdatok.ItemsSource = szurtlista;
                lblLetszam.Content = szurtlista.Sum(x => x.EmployeesNumber);
            }
        }

    }
}

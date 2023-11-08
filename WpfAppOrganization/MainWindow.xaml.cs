using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        List<string> szerkLista1 = new List<string>();
        List<string> szerkLista2 = new List<string>();
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
            Betoltes("organizations-100000.csv");
            dgAdatok.ItemsSource = szervezetek;


            szerkLista1 = szervezetek.Select(x => x.Country).OrderBy(x => x).Distinct().ToList();
            szerkLista1.Add("--");
            cbOrszag.ItemsSource = szerkLista1;
            szerkLista2 = szervezetek.Select(x => x.Founded.ToString()).OrderBy(x => x).Distinct().ToList();
            szerkLista2.Add("--");
            cbEv.ItemsSource = szerkLista2;
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
                szurtlista = szerkLista1.Where(x => x.Founded.ToString() == cbEv.SelectedItem.ToString() && x.Country.ToString() == cbOrszag.SelectedItem.ToString()).ToList();
                dgAdatok.ItemsSource = szurtlista;
                lblLetszam.Content = szurtlista.Sum(x => x.EmployeesNumber);
            }
            else if (cbOrszag.SelectedItem == "--" && cbEv.SelectedItem != "--")
            {
                szurtlista = szervezetek.Where(x => x.Founded.ToString() == cbEv.SelectedItem.ToString()).ToList();
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
            else if (cbEv.SelectedItem == "--" && cbOrszag.SelectedItem != "--")
            {
                szurtlista = szervezetek.Where(x => x.Country.ToString() == cbOrszag.SelectedItem.ToString()).ToList();
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

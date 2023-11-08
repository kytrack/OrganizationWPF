﻿using System;
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

namespace WpfApp1
{
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
            Betoltes("C:\\Users\\vedran.krisztian\\Desktop\\Teljesítmény\\organizations-100000.csv");
            dgDatagrid.ItemsSource = szervezetek;

            cbCountry.ItemsSource = szervezetek.Select(x => x.Country).OrderBy(x => x).Distinct().ToList();
            cbFounded.ItemsSource = szervezetek.Select(x => x.Founded).OrderBy(x => x).Distinct().ToList();
            labTotalEmpl.Content = szervezetek.Sum(x => x.EmployeesNumber);
        }

        private void dgDatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDatagrid.SelectedItem is Organization)
            {
                Organization valasztottObjektum = dgDatagrid.SelectedItem as Organization;
                labID.Content = valasztottObjektum.Id.ToString();
                labWEB.Content = valasztottObjektum.Website;
                labISM.Content = valasztottObjektum.Description;
            }
        }

        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var szurtlista = szervezetek.Where(x => cbFounded.SelectedItem != null ? x.Founded.ToString() == cbFounded.SelectedItem.ToString() : x.Founded==x.Founded).
                Where(x=> cbCountry.SelectedItem!=null ? x.Country == cbCountry.SelectedItem.ToString():x.Country==x.Country);

            dgDatagrid.ItemsSource= szurtlista;
            labTotalEmpl.Content= szurtlista.Sum(x=>x.EmployeesNumber).ToString();
        }
    }
}

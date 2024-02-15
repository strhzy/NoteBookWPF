using Newtonsoft.Json;
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

namespace Ezhednevnik
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<zametka> zametki = new List<zametka>();
        public MainWindow()
        {
            InitializeComponent();
            DateTime date_ = DateTime.Today;
            datepick.SelectedDate = date_;
            deserial("zametki.json");
            change_notes();
        }
        private void change_notes()
        {
            var notes = zametki.Where(z => z.Date == datepick.SelectedDate.ToString());
            notelist.ItemsSource = null;
            notelist.ItemsSource = notes;
        }
        private void datepick_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            change_notes();
        }
        private void deserial(string path)
        {
            string text = File.ReadAllText(path);
            zametki = JsonConvert.DeserializeObject<List<zametka>>(text);
            
        }
        private void serial()
        {
            string json = JsonConvert.SerializeObject(zametki);
            File.WriteAllText("zametki.json", json);
        }

        private void create_zam(object sender, RoutedEventArgs e)
        {
            zametka zametka = new zametka(datepick.SelectedDate.ToString(), title_zam.Text, text_zam.Text);
            zametki.Add(zametka);
            change_notes();
            serial();
        }
        private void delete_zam(object sender, RoutedEventArgs e)
        {
            try
            {
                var note = notelist.SelectedItems[0] as zametka;
                int index = zametki.FindIndex(item => item == note);
                zametki.RemoveAt(index);
                change_notes();
                serial();
            }
            catch
            {
                return;
            }   
        }
        
        private void save_zam(object sender, RoutedEventArgs e)
        {
            try
            {
                var zametka = notelist.SelectedItems[0] as zametka;
                int index = zametki.FindIndex(item => item == zametka);
                zametki[index] = new zametka(datepick.SelectedDate.ToString(), title_zam.Text, text_zam.Text);
                serial();
                change_notes();
            }
            catch
            {
                return;
            }
        }

        private void notelist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var select = notelist.SelectedItem;
            if (select != null)
            {
                text_zam.Text = (select as zametka).Text;
                title_zam.Text = (select as zametka).Title;
            }
        }
    }
}

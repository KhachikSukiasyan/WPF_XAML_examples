using System;
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
using FamousPeople.Classes;

namespace FamousPeople
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Person _currentPerson = null;
        StoreDB _db = new StoreDB();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonGetPerson_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int personID = Convert.ToInt32(textBoxID.Text);
                _currentPerson = _db.GetPerson(personID);
                gridMain.DataContext = _currentPerson;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при запросе к базе данных.");
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int personID = Convert.ToInt32(textBoxID.Text);
                _db.UpdatePerson(_currentPerson, personID);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при обновлении записи в базе данных.");
            }
        }

        bool isCollapsed = true;
        private void buttonShowList_Click(object sender, RoutedEventArgs e)
        {
            if (isCollapsed)
            {
                listPersons.Visibility = Visibility.Visible;
                try
                {
                    listPersons.ItemsSource = _db.GetAllPersons();
                    listPersons.DisplayMemberPath = "Name";
                }
                catch
                {
                    MessageBox.Show("Ошибка при запросе к базе данных.");
                }
            }
            else
            {
                listPersons.Visibility = Visibility.Collapsed;
            }
            isCollapsed = !isCollapsed;
        }

        private void listPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gridMain.DataContext = listPersons.SelectedItem;
        }
    }
}

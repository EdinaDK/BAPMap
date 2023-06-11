using BAPMap.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
namespace BAPMap
{
    public partial class DatabasePage : Page
    {
        public ObservableCollection<City> currentList;
        public DatabasePage()
        {
            InitializeComponent();
            currentList = new ObservableCollection<City>(Repository.db.Cities.ToList());
            DataGrid.ItemsSource = currentList;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Window.GetWindow(this).DragMove();
            }
        }
        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void BtnMain_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
        private void BtnMap_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MapPage());
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var newCity = new City(currentList.Last().ID + 1);
            var result = Repository.AddCity(newCity!);
            if (result != null)
            {
                currentList.Add(result);
            }
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Repository.SaveUpdates(currentList);
            MessageBox.Show("База данных успешно обновлена.", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = DataGrid.SelectedItems.Cast<City>().ToList();
            if (selectedItems.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите город, который вы хотите удалить.",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }
            if (selectedItems.Count == 1)
            {
                RemoveSingleCity(selectedItems[0]);
            }
            else
            {
                var result = MessageBox.Show($"Вы действительно хотите удалить выбранные города?",
                                "Удалить?",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    foreach (var item in selectedItems)
                    {
                        Repository.RemoveCity(item);
                        currentList.Remove(item);
                    }
                }
            }
        }
        private void RemoveSingleCity(City city)
        {
            var result = MessageBox.Show($"Вы действительно хотите удалить город {city}?",
                            "Удалить?",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Repository.RemoveCity(city);
                currentList.Remove(city);
            }
        }
    }
}
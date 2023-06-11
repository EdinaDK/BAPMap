using BAPMap.Model;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
namespace BAPMap
{
    public partial class MapPage : Page
    {
        List<City> cityList;
        City? selectedCity;
        public MapPage()
        {
            InitializeComponent();
            cityList = Repository.db.Cities.ToList();
            ComboBox.ItemsSource = cityList;
            ComboBox.SelectedIndex = -1;
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
        private void BtnData_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DatabasePage());
        }
        private void ShowCityOnMap()
        {
            if (selectedCity == null)
            {
                return;
            }
            Map.Children.Clear();
            var pushpin = new Pushpin()
            {
                Location = new Location(selectedCity.Latitude, selectedCity.Longitude),
                ToolTip = selectedCity.Name,
                Background = new SolidColorBrush(Colors.Transparent)
            };
            var pushpinImage = new Image()
            {
                Source = new BitmapImage(new Uri("/Images/Pushpin-512x512.png", UriKind.RelativeOrAbsolute)),
                MaxWidth = 18,
                MaxHeight = 18
            };
            pushpin.Content = pushpinImage;
            Map.Children.Add(pushpin);
            Map.SetView(new Location(selectedCity.Latitude, selectedCity.Longitude), 8);
        }
        private void BtnShow_Click(object sender, RoutedEventArgs e)
        {
            ShowCityOnMap();
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Rect bounds = VisualTreeHelper.GetDescendantBounds(Map);
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, 96, 96, PixelFormats.Pbgra32);
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                VisualBrush visualBrush = new VisualBrush(Map);
                drawingContext.DrawRectangle(visualBrush, null, new Rect(new Point(), bounds.Size));
            }
            renderTargetBitmap.Render(drawingVisual);
            BitmapEncoder bitmapEncoder = new PngBitmapEncoder();
            bitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Files (*.png)|*.png";
            saveFileDialog.FileName = "BAPMap.png";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (Stream stream = File.Create(saveFileDialog.FileName))
                {
                    bitmapEncoder.Save(stream);
                }
            }
        }
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            Map.Children.Clear();
            ComboBox.SelectedIndex = -1;
            Map.Center = new Location(62.15, 89.15);
            Map.ZoomLevel = 5;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox.SelectedIndex >= 0)
            {
                selectedCity = (ComboBox.SelectedItem as City)!;
            }
            else selectedCity = null;
        }
    }
}
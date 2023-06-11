using System.Windows;
namespace BAPMap
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Repository.LoadBase();
            MainFrame.Content = new MainPage();
        }
    }
}
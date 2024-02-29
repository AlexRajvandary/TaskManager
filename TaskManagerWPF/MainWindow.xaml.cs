using System.Windows;
using TaskManagerLib.Data;
using TaskManagerLib.ViewModel;

namespace TaskManagerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            using var taskContext = new TaskContext();
            DataContext = new TaskViewModel(taskContext);
            InitializeComponent();
        }
    }
}
using System.Windows;
using System.Windows.Controls;
using TaskManagerLib.Data;
using TaskManagerLib.Models;
using TaskManagerLib.ViewModel;

namespace TaskManagerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TaskViewModel taskViewModel;

        public MainWindow()
        {
            var taskContext = new TaskContext();
            taskViewModel = new TaskViewModel(taskContext);
            DataContext = taskViewModel;
            InitializeComponent();
            taskViewModel.PropertyChanged += TaskViewModel_PropertyChanged;
        }

        private void TaskViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(TaskViewModel.SearchResult))
            {
                SearchResult.IsSelected = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(NewTaskTitle.Text) ||
               NewTaskCategory.SelectedItem is Category)
            {
                var category = (Category)NewTaskCategory.SelectedItem;
                taskViewModel.AddNewTaskCommand.Execute(new TaskItem() { Title = NewTaskTitle.Text, Category = category, CreationTime = DateTimeOffset.UtcNow, Description = NewTaskDescription.Text});
            }
        }
    }
}
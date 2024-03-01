using System.ComponentModel;
using System.Windows.Input;
using TaskManagerLib.Command;
using TaskManagerLib.Data;
using TaskManagerLib.Models;

namespace TaskManagerLib.ViewModel
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        private readonly TaskContext taskContext;

        private List<Category> categories;
        private List<TaskItem> tasks;
        private List<TaskItem> searchResult;
        private TaskItem? currentItem;

        public TaskViewModel(TaskContext taskContext)
        {
            this.taskContext = taskContext ?? throw new ArgumentNullException(nameof(taskContext));

            AddNewTaskCommand = new RelayCommand<TaskItem>(AddNewTask);
            DeleteTaskCommand = new RelayCommand<TaskItem>(DeleteTask);
            UpdateTasksCommand = new RelayCommand(UpdateAllTasks);
            SearchTaskCommand = new RelayCommand<string>(SearchTask);
            UpdateTaskCommand = new RelayCommand<TaskItem>(UpdateTask);

            UpdateAllCategories();
            UpdateAllTasks();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand AddNewTaskCommand { get; }

        public ICommand DeleteTaskCommand { get; }

        public ICommand UpdateTasksCommand { get; }

        public ICommand SearchTaskCommand { get; }

        public ICommand UpdateTaskCommand { get; }

        public List<Category> Categories
        {
            get { return categories; }
            set
            {
                if (categories != value)
                {
                    categories = value;
                    OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Categories)));
                }
            }
        }

        public List<TaskItem> Tasks
        {
            get { return tasks; }
            private set
            {
                if (tasks != value)
                {
                    tasks = value;
                    OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Tasks)));
                }
            }
        }

        public List<TaskItem> SearchResult
        {
            get { return searchResult; }
            private set
            {
                if (searchResult != value)
                {
                    searchResult = value;
                    OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Tasks)));
                }
            }
        }

        public TaskItem? CurrentTask
        {
            get { return currentItem; }
            set
            {
                if (currentItem != value)
                {
                    currentItem = value;
                    OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(CurrentTask)));
                }
            }
        }

        private void AddNewTask(TaskItem item)
        {
            try
            {
                taskContext.Tasks.Add(item);
                taskContext.SaveChanges();

                //Для того, чтобы обновить view
                UpdateAllTasks();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void DeleteTask(TaskItem item)
        {
            taskContext.Tasks.Remove(item);
            taskContext.SaveChanges();

            //Для того, чтобы обновить view
            UpdateAllTasks();
        }

        private void UpdateAllTasks()
        {
            try
            {
                Tasks = taskContext.Tasks.ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void UpdateAllCategories()
        {
            try
            {
                Categories = taskContext.Categories.ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void SearchTask(string quary)
        {
            try
            {
                SearchResult = taskContext.Tasks.Where(task => task.Title.Contains(quary) || task.Description.Contains(quary)).ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void UpdateTask(TaskItem item)
        {
            taskContext.Tasks.Update(item);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}

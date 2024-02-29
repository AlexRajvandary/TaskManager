using TaskManagerLib.Data;
using TaskManagerLib.Models;
using TaskManagerLib.ViewModel;

namespace TaskManager
{
    internal class Program
    {
        private static TaskViewModel taskViewModel;

        private static string menu = "1. Add task\n" +
            "2. Show all tasks\n" +
            "3. Search task\n";

        static void Main(string[] args)
        {
            var taskContext = new TaskContext();
            taskViewModel = new TaskViewModel(taskContext);
            
            string userInput;
            do
            {
                Console.WriteLine("Choose command:");
                ShowMenu();
                userInput = Console.ReadLine();
                ReadUserInput(userInput);
            } while (userInput.ToLower() != "e");
        }

        private static void ShowMenu()
        {
            Console.WriteLine(menu);
            Console.WriteLine("To exit press Escape.");
        }

        private static TaskItem AskDataForNewTask()
        {
            if(taskViewModel.Categories is null)
            {
                Console.WriteLine("Categories list is empty");
            }

            Console.WriteLine("Enter a title for the new task:");
            var name = Console.ReadLine();

            Console.WriteLine("Enter description for the new task:");
            var description = Console.ReadLine();

            Console.WriteLine("Select a category for the new task:");

            for (int i = 0; i < taskViewModel.Categories?.Count; i++)
            {
                var num = i + 1;
                Console.WriteLine($"{num} {taskViewModel.Categories[i].Name}");
            }

            var categoryNum = SelectFromAList(taskViewModel.Categories.Count);

            var category = taskViewModel.Categories[categoryNum];

            return new TaskItem() { Title = name, Description = description, CreationTime = new DateTimeOffset(DateTime.UtcNow), Category = category };
        }

        private static int SelectFromAList(int listLength)
        {
            bool inputSuccess;
            int categoryNum;

            do
            {
                var userInput = Console.ReadLine();
                inputSuccess = int.TryParse(userInput, out categoryNum) && categoryNum > 0 && categoryNum < listLength;

                if (!inputSuccess) { Console.WriteLine("Input incorrect!"); }
            } while (!inputSuccess);

            return categoryNum;
        }

        private static void ReadUserInput(string userInput)
        {
            if (int.TryParse(userInput, out var commandNumber))
            {
                switch (commandNumber)
                {
                    case 1:
                        var task = AskDataForNewTask();
                        taskViewModel.AddNewTaskCommand.Execute(task);
                        Console.WriteLine("Task has been added!");
                        break;
                    case 2:
                        Console.WriteLine("All tasks list:");
                        
                        taskViewModel.UpdateTasksCommand.Execute(null);
                        for (int i = 0; i < taskViewModel.Tasks.Count; i++)
                        {
                            var num = i + 1;
                            Console.WriteLine($"{num}. {taskViewModel.Tasks[i]}");
                        }

                        Console.WriteLine("To choose a task enter the number of the task:");
                        var selectedItemNum = SelectFromAList(taskViewModel.Tasks.Count);
                        taskViewModel.CurrentTask = taskViewModel.Tasks[selectedItemNum - 1];
                        break;
                    case 3:
                        Console.WriteLine("Enter a quary for search:");
                        var quary = Console.ReadLine();
                        taskViewModel.SearchTaskCommand.Execute(quary);

                        for (int i = 0; i < taskViewModel.Tasks.Count; i++)
                        {
                            var num = i + 1;
                            Console.WriteLine($"{num}. {taskViewModel.Tasks[i]}");
                        }

                        Console.WriteLine("To choose a task enter the number of the task:");
                        selectedItemNum = SelectFromAList(taskViewModel.Tasks.Count);
                        taskViewModel.CurrentTask = taskViewModel.Tasks[selectedItemNum - 1];
                        break;
                }
            }
        }
    }
}

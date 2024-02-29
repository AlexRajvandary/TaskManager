namespace TaskManagerLib.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreationTime { get; set; }

        public bool IsCompleted { get; set; }

        public Category Category { get; set; }

        public override string ToString()
        {
            var completionStatus = IsCompleted ? "Done" : "Undone";

            return $"Title: {Title}\nDescription: {Description}\nCategory: {Category}\nCreated at: {CreationTime}\nCompletion status: {completionStatus}\n";
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace TaskTrackerApp
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }

    class Program
    {
        private static readonly string FilePath = "tasks.json";

        static void Main(string[] args)
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("    C# TASK TRACKER - CLI VERIFICATION RUN       ");
            Console.WriteLine("=================================================");

            List<TaskItem> tasks = LoadTasks();

            // 1. Programmatic Self-Check
            bool selfCheck = RunSelfChecks();
            Console.WriteLine($"[VERIFICATION CHECK] Programmatic Self-Test Passed: {selfCheck}");
            Console.WriteLine("-------------------------------------------------\n");

            // 2. Add Sample Tasks
            tasks.Add(new TaskItem { Id = 1, Title = "Set up .NET environment", IsCompleted = true });
            tasks.Add(new TaskItem { Id = 2, Title = "Implement JSON storage", IsCompleted = true });
            tasks.Add(new TaskItem { Id = 3, Title = "Publish GitHub repository", IsCompleted = false });

            SaveTasks(tasks);

            // 3. Render Formatted Output
            Console.WriteLine("+----+----------------------------------+-----------+");
            Console.WriteLine("| ID | Task Description                 | Status    |");
            Console.WriteLine("+----+----------------------------------+-----------+");

            foreach (var t in tasks)
            {
                string status = t.IsCompleted ? "Completed" : "Pending";
                Console.WriteLine($"| {t.Id,-2} | {t.Title,-32} | {status,-9} |");
            }

            Console.WriteLine("+----+----------------------------------+-----------+");
            Console.WriteLine($"\nTotal Tasks: {tasks.Count} | Completed: {tasks.Count(t => t.IsCompleted)} | Pending: {tasks.Count(t => !t.IsCompleted)}");
            Console.WriteLine("=================================================");
        }

        public static bool RunSelfChecks()
        {
            var dummy = new List<TaskItem>
            {
                new TaskItem { Id = 1, Title = "Test Task", IsCompleted = false }
            };
            string json = JsonSerializer.Serialize(dummy);
            var deserialized = JsonSerializer.Deserialize<List<TaskItem>>(json);

            return deserialized != null && deserialized.Count == 1 && deserialized[0].Title == "Test Task";
        }

        private static List<TaskItem> LoadTasks()
        {
            if (!File.Exists(FilePath)) return new List<TaskItem>();
            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        }

        private static void SaveTasks(List<TaskItem> tasks)
        {
            string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
    }
}

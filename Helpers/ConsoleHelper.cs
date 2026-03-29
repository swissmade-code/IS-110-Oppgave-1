namespace UniversitetSystem.Helpers
{
    public static class ConsoleHelper
    {
        public static void PrintHeader(string title)
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine($"  {title}");
            Console.WriteLine("========================================");
        }

        public static string? Prompt(string label)
        {
            Console.Write($"{label}: ");
            return Console.ReadLine()?.Trim();
        }

        public static int? PromptInt(string label)
        {
            Console.Write($"{label}: ");
            return int.TryParse(Console.ReadLine(), out int val) ? val : null;
        }

        public static void PrintError(string msg)
            => Console.WriteLine($"\n[ERROR] {msg}");

        public static void Pause()
        {
            Console.Write("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
}
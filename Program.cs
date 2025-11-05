using System;
using System.Text;
using System.Threading.Tasks;
using Google.GenAI;
using Microsoft.Extensions.Configuration;

class Program
{
    static async Task Main(string[] args)
    {
        // 1. Load the API key from user secrets
        var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        string? apiKey = config["GEMINI_API_KEY"];

        if (string.IsNullOrEmpty(apiKey))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: GEMINI_API_KEY not found.");
            Console.WriteLine("Please set it using: dotnet user-secrets set \"GEMINI_API_KEY\" \"YOUR_KEY\"");
            Console.ResetColor();
            return;
        }

        // 2. Read input from the console (piped from 'git diff')
        string diffContent = await ReadPipedInputAsync();

        if (string.IsNullOrWhiteSpace(diffContent))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("No diff content provided. Did you forget to pipe it?");
            Console.WriteLine("Usage: git diff --staged | dotnet run");
            Console.ResetColor();
            return;
        }

        try
        {
            // 3. Initialize the Gemini client
            var client = new Client(apiKey: apiKey);

            // 4. Create a specific prompt for the AI
            string prompt = $@"
                You are an expert software developer who writes perfect Git commit messages.
                Based on the following 'git diff' output, generate a concise and descriptive
                commit message following the Conventional Commits specification.

                The commit message should have a 'type' (e.g., feat, fix, docs, chore, refactor)
                followed by a short description, all in lowercase.
                Do not include a message body or footer, just the single-line header.

                Example: feat: add user authentication endpoint

                Here is the diff:
                ```diff
                {diffContent}
                ```

                Commit Message:
            ";
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("🤖 Generating commit message based on your diff...");
            Console.ResetColor();
            
            // 5. Call the Gemini API model
            var response = await client.Models.GenerateContentAsync(
                model: "gemini-2.5-flash", 
                contents: prompt            
            );

            // 6. Print the clean response (CORRECTED SYNTAX)
            // Access the text through Candidates -> Content -> Parts
            string commitMessage = response.Candidates[0].Content.Parts[0].Text.Trim();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nSuggested Commit Message:");
            Console.ResetColor();
            Console.WriteLine(commitMessage);

        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nAn error occurred: {ex.Message}");
            Console.ResetColor();
        }
    }

    // Helper method to read all text piped into the console
    private static async Task<string> ReadPipedInputAsync()
    {
        if (!Console.IsInputRedirected)
        {
            return string.Empty;
        }

        var stringBuilder = new StringBuilder();
        string? line;
        while ((line = await Console.In.ReadLineAsync()) != null)
        {
            stringBuilder.AppendLine(line);
        }
        return stringBuilder.ToString();
    }
}
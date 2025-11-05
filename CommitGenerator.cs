namespace GitCommitGen;

/// <summary>
/// Contains core logic for generating simplified commit messages.
/// </summary>
public class CommitGenerator
{
    /// <summary>
    /// Creates a formatted commit message string based on a type and a description.
    /// Example: "feat: add new commit generation class"
    /// </summary>
    /// <param name="type">The type of change (e.g., feat, fix, chore).</param>
    /// <param name="description">A brief description of the change.</param>
    /// <returns>The standardized commit message.</returns>
    public string GenerateCommitMessage(string type, string description)
    {
        if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Type and description cannot be empty.");
        }

        // Standardize the output format
        return $"{type.Trim().ToLower()}: {description.Trim()}";
    }

    /// <summary>
    /// Capitalizes the first letter of a string.
    /// </summary>
    /// <param name="s">The input string.</param>
    /// <returns>The string with the first letter capitalized.</returns>
    public static string CapitalizeFirstLetter(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return s;
        }
        // Use culture-invariant conversion for simplicity in a utility
        return char.ToUpperInvariant(s[0]) + s.Substring(1);
    }
}
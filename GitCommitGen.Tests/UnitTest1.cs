using Xunit; // Provides [Fact] and Assert class
using GitCommitGen; // Allows access to the CommitGenerator class
using System; // Needed for ArgumentException

namespace GitCommitGen.Tests;

public class CommitGeneratorTests
{
    // A test to ensure a standard feature message is correctly formatted.
    [Fact]
    public void GenerateCommitMessage_ShouldReturnStandardFormat_ForFeature()
    {
        // Arrange
        var generator = new CommitGenerator();
        string expected = "feat: add new test functionality";
        
        // Act
        string actual = generator.GenerateCommitMessage("feat", "add new test functionality");
        
        // Assert
        Assert.Equal(expected, actual);
    }

    // A test to ensure input type casing is automatically lowercased.
    [Fact]
    public void GenerateCommitMessage_ShouldLowercaseType()
    {
        // Arrange
        var generator = new CommitGenerator();
        string expected = "fix: address critical bug";
        
        // Act
        string actual = generator.GenerateCommitMessage("FIX", "address critical bug");
        
        // Assert
        Assert.Equal(expected, actual);
    }

    // A test to ensure that the method throws an exception for invalid input.
    [Fact]
    public void GenerateCommitMessage_ShouldThrowException_OnEmptyInput()
    {
        // Arrange
        var generator = new CommitGenerator();
        
        // Act & Assert
        // We assert that calling the method with empty strings throws an ArgumentException
        Assert.Throws<ArgumentException>(() => generator.GenerateCommitMessage("fix", ""));
        Assert.Throws<ArgumentException>(() => generator.GenerateCommitMessage("", "description"));
    }

    // A test for the utility method to capitalize a string.
    [Fact]
    public void CapitalizeFirstLetter_ShouldCapitalizeInput()
    {
        // Arrange
        string input = "hello world";
        string expected = "Hello world";

        // Act
        string actual = CommitGenerator.CapitalizeFirstLetter(input);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}
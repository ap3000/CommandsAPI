using System;
using Xunit;
using CommandAPI.Models;

namespace CommandAPI.Tests
{
    public class CommandTests : IDisposable
    {
        Command testCommand;

        public CommandTests()
        {
            testCommand = new Command
            {
                Howto = "Do something aesome",
                Platform = "Some platform",
                CommandLine = "Some commandline"
            };
        }
        [Fact]
        public void CanChangeHaowTo()
        {
            // Arrange            

            // Act
            testCommand.Howto = "Execute Unit Tests";
            
            // Assert
            Assert.Equal("Execute Unit Tests", testCommand.Howto);
        }

        [Fact]
        public void CanChangePlatform()
        {
            // Arrange

            // Act
            testCommand.Platform = "xUnit";
            
            // Assert
            Assert.Equal("xUnit", testCommand.Platform);
        }

        [Fact]
        public void CanChangeCommandLine()
        {
            // Arrange

            // Act
            testCommand.CommandLine = "dotnet test";
            
            // Assert
            Assert.Equal("dotnet test", testCommand.CommandLine);
        }

        public void Dispose()
        {
            testCommand = null;
        }
    }
}
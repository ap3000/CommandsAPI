using System.Collections.Generic;
using CommandAPI.Models;

namespace CommandAPI.Data
{
    public class MockCommandAPIRepo : ICommandAPIRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command{
                    Id = 0,
                    Howto = "How to generate Migration",
                    CommandLine = "dotnet ef migration add <Name of Migration>",
                    Platform = ".Net Core EF"
                },
                new Command{
                    Id = 2,
                    Howto = "Run Migration",
                    CommandLine = "dotnet ef database update>",
                    Platform = ".Net Core EF"
                },
                new Command{
                    Id = 2,
                    Howto = "List active Migration",
                    CommandLine = "dotnet ef migrations list>",
                    Platform = ".Net Core EF"
                }
            };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command
            {
                Id = 0,
                Howto = "How to generate Migration",
                CommandLine = "dotnet ef migration add <Name of Migration>",
                Platform = ".Net Core EF"
            };
        }
        
        public bool saveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}
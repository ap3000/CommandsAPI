using System;
using System.Collections.Generic;
using Moq;
using AutoMapper;
using CommandAPI.Models;
using CommandAPI.Data;
using CommandAPI.Profiles;
using Xunit;
using CommandAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Dtos;

namespace CommandAPI.Tests
{
    public class CommandsControllerTests : IDisposable
    {
        Mock<ICommandAPIRepo> mockRepo;
        CommandsProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;

        public CommandsControllerTests()
        {
            mockRepo = new Mock<ICommandAPIRepo>();
            realProfile = new CommandsProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }

        public void Dispose()
        {
            mockRepo = null;
            mapper = null;
            configuration = null;
            realProfile = null;
        }

        //**************************************************
        //*
        //GET   /api/commands Unit Tests
        //*
        //**************************************************        

        //TEST 1.1
        [Fact]
        public void GetAllCommands_ReturnsZeroResources_WhenDBIsEmpty()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetAllCommands()).Returns(GetCommands(0));

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllCommands();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        //TEST 1.2
        [Fact]
        public void GetAllCommands_ReturnsOneItem_WhenDBHasOneResource()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetAllCommands()).Returns(GetCommands(1));

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllCommands();

            //Assert
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<CommandReadDto>;

            Assert.Single(commands);
        }

        //TEST 1.3
        [Fact]
        public void GetAllCommands_Returns200OK_WhenDBHasOneResourde()
        {
            // Arrange
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(mockRepo.Object, mapper);

            // Act
            var result = controller.GetAllCommands();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        //TEST 1.4
        [Fact]
        public void GetAllCommands_ReturnsCorrectType_WhenDBHasOneResourde()
        {
            // Arrange
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(mockRepo.Object, mapper);

            // Act
            var result = controller.GetAllCommands();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(result);
        }

        //**************************************************
        //*
        //GET   /api/commands/{id} Unit Tests
        //*
        //**************************************************

        //TEST 2.1
        [Fact]
        public void GetCommandById_Returns404NotFound_WhenNonExistentIDProvided()
        {
            // Arrange
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(() => null);
            var controller = new CommandsController(mockRepo.Object, mapper);

            // Act
            var result = controller.GetCommandById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        //TEST 2.1
        [Fact]
        public void GetCommandById_Returns200OK_WhenValidIDProvided()
        {
            // Arrange
            mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command 
            {
                Id = 1,
                HowTo = "Mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });
            var controller = new CommandsController(mockRepo.Object, mapper);

            // Act
            var result = controller.GetCommandById(1);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        //TEST 2.2
        [Fact]
        public void GetCommandByID_ReturnsCorrectResouceType_WhenValidIDProvided()
        {
            // Arrange
            mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command 
            {
                Id = 1,
                HowTo = "Mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });
            
            var controller = new CommandsController(mockRepo.Object, mapper);

            // Act
            var result = controller.GetCommandById(1);

            // Assert
            Assert.IsType<ActionResult<CommandReadDto>>(result);
        }

        //**************************************************
        //*
        //POST   /api/commands/ Unit Tests
        //*
        //**************************************************

        //TEST 3.1
        [Fact]
        public void CreateCommand_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange 
            mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command 
            { 
                Id = 1, 
                HowTo = "mock", 
                Platform = "Mock", 
                CommandLine = "Mock" 
            });

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.CreateCommand(new CommandCreateDto { });

            //Assert
            Assert.IsType<ActionResult<CommandReadDto>>(result);
        }

        //TEST 3.2
        [Fact]
        public void CreateCommand_Returns201Created_WhenValidObjectSubmitted()
        {
            //Arrange 
            mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command 
            { 
                Id = 1, 
                HowTo = "mock", 
                Platform = "Mock", 
                CommandLine = "Mock" 
            });

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.CreateCommand(new CommandCreateDto { });

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }


        private List<Command> GetCommands(int num)
        {
            var commands = new List<Command>();
            if (num > 0)
            {
                commands.Add(new Command
                {
                    Id = 0,
                    HowTo = "How to genrate a migration",
                    CommandLine = "dotnet ef migrations add <Name of Migration>",
                    Platform = ".Net Core EF"
                });
            }
            return commands;
        }
    }
}
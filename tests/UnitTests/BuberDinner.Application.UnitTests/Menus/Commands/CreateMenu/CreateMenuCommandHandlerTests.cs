using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Application.UnitTests.Menus.Commands.TestUtils;
using BuberDinner.Application.UnitTests.TestUtils.Menus.Extensions;

using FluentAssertions;

using Moq;

namespace BuberDinner.Application.UnitTests.Menus.Commands.CreateMenu;

public class CreateMenuCommandHandlerTests
{
    private readonly CreateMenuCommandHandler _handler;
    private readonly Mock<IMenuRepository> _mockMenuRepository;

    public CreateMenuCommandHandlerTests()
    {
        _mockMenuRepository = new Mock<IMenuRepository>();
        _handler = new CreateMenuCommandHandler(_mockMenuRepository.Object);
    }

    // T1: SUT - logical component what we are testing
    // T2: Scenario - what we are testing
    // T3: Expected outcome - what we expect the logical component to do
    [Theory]
    [MemberData(nameof(ValidateCreateMenuCommands))]
    public async Task HandleCreateMenuCommand_whenMenuIsValid_ShouldCreateAndReturnMenu(CreateMenuCommand createMenuCommand)
    {

        // Act
        // Invoke the handler
        var result = await _handler.Handle(createMenuCommand, default);

        // Assert
        // 1. No errors
        result.IsError.Should().BeFalse();
        // 2. Validate correct menu created based on command
        result.Value.ValidateCreatedFrom(createMenuCommand);
        // 3. Menu added to repository
        _mockMenuRepository.Verify(m => m.AddAsync(result.Value), Times.Once);
    }

    public static IEnumerable<object[]> ValidateCreateMenuCommands()
    {
        yield return new[] { CreateMenuCommandUtils.CreateCommand() };
        
        yield return new[] 
        { 
            CreateMenuCommandUtils.CreateCommand(
                sections: CreateMenuCommandUtils.CreateSectionsCommand(sectionCount: 3)) 
        };

        yield return new[] 
        { 
            CreateMenuCommandUtils.CreateCommand(
                sections: CreateMenuCommandUtils.CreateSectionsCommand(
                    sectionCount: 3,
                    items: CreateMenuCommandUtils.CreateItemsCommand(itemCount: 3))) 
        };
    }
}
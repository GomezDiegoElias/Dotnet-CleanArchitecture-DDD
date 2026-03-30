using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Application.UnitTests.TestUtils.Constants;

namespace BuberDinner.Application.UnitTests.Menus.Commands.TestUtils;

public static class CreateMenuCommandUtils
{
    // name
    // description
    // list of sections
    public static CreateMenuCommand CreateCommand(
        List<MenuSectionCommand>? sections = null) => 
        new CreateMenuCommand(
            Constants.Host.Id.ToString()!,
            Constants.Menu.Name,
            Constants.Menu.Description,
            sections ?? CreateSectionsCommand());

    // sections
    // name
    // description
    // list of items
    public static List<MenuSectionCommand> CreateSectionsCommand(
        int sectionCount = 1,
        List<MenuItemCommand>? items = null) => 
        Enumerable.Range(1, sectionCount)
            .Select(i => new MenuSectionCommand(
                Constants.Menu.SectionNameFromIndex(i),
                Constants.Menu.SectionDescriptionFromIndex(i),
                items ?? CreateItemsCommand()))
            .ToList();


    // items
    // name
    // description
    public static List<MenuItemCommand> CreateItemsCommand(int itemCount = 1) =>
        Enumerable.Range(0, itemCount)
            .Select(i => new MenuItemCommand(
                Constants.Menu.ItemNameFromIndex(i),
                Constants.Menu.ItemDescriptionFromIndex(i)))
            .ToList();
}
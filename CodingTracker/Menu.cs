using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;

namespace CodingTracker
{
    public class Menu
    {

        internal void DisplayMenu()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold green]Welcome to the Coding Tracker![/]");
            AnsiConsole.MarkupLine("[bold yellow]Please select an option:[/]");
            
            var selection = AnsiConsole.Prompt(
                  new SelectionPrompt<MenuOptions.MenuOption>()
                  .AddChoices(Enum.GetValues<MenuOptions.MenuOption>())
                  );

            
            switch (selection)
            {
                case MenuOptions.MenuOption.StartNewSession:
                    Console.WriteLine("Starting a new coding session...");
                    break;
                case MenuOptions.MenuOption.ViewSessions:
                    Console.WriteLine("View coding session...");
                    break;
                case MenuOptions.MenuOption.Exit:
                    ExitApplication();
                    break;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid choice. Please try again.[/]");
                    break;
            }
        }

        internal void ExitApplication()
        {
            AnsiConsole.MarkupLine("[bold red]Exiting the application...[/]");
            Environment.Exit(0);
        }
    }
}
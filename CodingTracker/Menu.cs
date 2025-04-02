using Spectre.Console;
using CodingTracker.Repository;
using CodingTracker.Models;
using System.Collections;

namespace CodingTracker
{
    public class Menu
    {

        private readonly CodingTrackerRepository _repository;

        public Menu(CodingTrackerRepository repository)
        {
            _repository = repository;
        }

        internal void DisplayMenu()
        {
            while (true)
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
                        CreateSession();
                        break;
                    case MenuOptions.MenuOption.ViewSessions:
                        ViewSessions();
                        break;
                    case MenuOptions.MenuOption.EditSession:
                        ViewSessions();
                        break;
                    case MenuOptions.MenuOption.DeleteSession:
                        ViewSessions();
                        break;
                    case MenuOptions.MenuOption.Exit:
                        ExitApplication();
                        break;
                    default:
                        AnsiConsole.MarkupLine("[red]Invalid choice. Please try again.[/]");
                        break;
                }
            }

        }

        internal void ExitApplication()
        {
            AnsiConsole.MarkupLine("[bold red]Exiting the application...[/]");
            Environment.Exit(0);
        }


        internal void ViewSessions()
        {
            var sessions = _repository.GetSessions();

            if (sessions.Count == 0)
            {
                AnsiConsole.MarkupLine("[bold red]No coding sessions found.[/]");
                return;
            }

            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Start Time");
            table.AddColumn("End Time");
            table.AddColumn("Duration (minutes)");

            foreach (var session in sessions)
            {
                table.AddRow(session.Id.ToString(), session.StartTime.ToString(), session.EndTime.ToString(), session.Duration.ToString());
            }

            AnsiConsole.Write(table);
            Console.ReadLine();
        }

        internal void CreateSession()
        {

            var createSessionsSelection = AnsiConsole.Prompt(
                      new SelectionPrompt<MenuOptions.CreateSessionOption>()
                      .AddChoices(Enum.GetValues<MenuOptions.CreateSessionOption>())
                      );

            switch (createSessionsSelection)
            {
                case MenuOptions.CreateSessionOption.Automatic:
                    StartAutomaticSession();
                    break;
                case MenuOptions.CreateSessionOption.Manual:
                    StartManualSession();
                    break;
                case MenuOptions.CreateSessionOption.Return:
                    return;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid choice. Please try again.[/]");
                    break;
            }


        }

        internal void DeleteSession()
        {
            var sessions = _repository.GetSessions();

            if (sessions.Count == 0)
            {
                AnsiConsole.MarkupLine("[bold red]No coding sessions found.[/]");
                return;
            }

            var sessionToDelete = AnsiConsole.Prompt(
            new SelectionPrompt<CodingSession>()
            .Title("What coding session do you want to delete?")
            .UseConverter(p => $"{p.Id} || {p.StartTime} || {p.EndTime} || {p.Duration}")
            .AddChoices(sessions)
            );

            _repository.DeleteSession(sessionToDelete.Id);
            AnsiConsole.MarkupLine($"[bold red]Session {sessionToDelete.Id} deleted successfully![/]");
        }

        internal void StartAutomaticSession()
        {
            var startTime = DateTime.Now;
            AnsiConsole.MarkupLine($"[green]Session started at: {startTime}[/]");
            Console.WriteLine("Press any key to end the session...");
            Console.ReadKey();
            var endTime = DateTime.Now;
            AnsiConsole.MarkupLine($"[red]Session ended at: {endTime}[/]");
            var session = new CodingSession(startTime, endTime);
            _repository.AddSession(session);
            AnsiConsole.MarkupLine("[bold green]Session saved successfully![/]");
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadLine();
        }


        internal void StartManualSession()
        {
            var startTime = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter the start time (yyyy-MM-dd HH:mm:ss):")
                .PromptStyle("green")
                .Validate(Validation.DateTimeValidate));

            DateTime parsedStartTime = DateTime.ParseExact(startTime, "yyyy-MM-dd HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture);

            AnsiConsole.MarkupLine($"[green]You entered a valid datetime: {parsedStartTime}[/]");


            var endTime = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter the end time (yyyy-MM-dd HH:mm:ss):")
                .PromptStyle("red")
                .Validate(Validation.DateTimeValidate));

            DateTime parsedEndTime = DateTime.ParseExact(endTime, "yyyy-MM-dd HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture);
            AnsiConsole.MarkupLine($"[red]You entered a valid datetime: {parsedEndTime}[/]");



            var session = new CodingSession(parsedStartTime, parsedEndTime);
            _repository.AddSession(session);
            AnsiConsole.MarkupLine("[bold green]Session saved successfully![/]");
        }

    }
}
using Lab3_3_2.BLL.EntityServices;
using Lab3_3_2.BLL.Models;
using Lab3_3_2.Core;
using Lab3_3_2.DAL;
using Lab3_3_2.DAL.Entities;

namespace Lab3_3_2.PL;

public sealed class Menu
{
    private static readonly Lazy<Menu> _instance = new (() => new Menu());

    private string _dataFolderPath = "Data";
    private string _fileType = "json";

    private Menu() { }

    public static Menu Instance => _instance.Value;

    public void MainMenu()
    {
        ConfigureSettings();

        while (true)
        {
            Console.Clear();
            PrintSettings();
            Console.WriteLine("1. Students");
            Console.WriteLine("2. Librarians");
            Console.WriteLine("3. Software developers");

            Console.WriteLine("4. Change settings");
            Console.WriteLine("0. Exit");
            Console.Write("Select action: ");

            var action = Console.ReadLine();

            try
            {
                switch (action)
                {
                    case "1": StudentsMenu(); break;
                    case "2": LibrariansMenu(); break;
                    case "3": SoftwareDevelopersMenu(); break;
                    case "4": ConfigureSettings(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid action."); break;
                }
            }
            catch (UnknownFileTypeException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[Invalid file format]: {ex.Message}");
                Console.ResetColor();
            }
            catch (EntityValidationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[Validation error]: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"[Critical error]: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    private void ConfigureSettings()
    {
        Console.Clear();
        _fileType = string.Empty;
        Console.Write("Input data folder path: ");
        var inputPath = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(inputPath))
            _dataFolderPath = inputPath;

        Console.Clear();

        while (true)
        {
            Console.WriteLine("Select serialization format:");
            Console.WriteLine("1. JSON");
            Console.WriteLine("2. XML");
            Console.Write("Your choice: ");

            _fileType = Console.ReadLine() switch
            {
                "1" => "json",
                "2" => "xml",
                _ => string.Empty
            };

            if (string.IsNullOrWhiteSpace(_fileType))
            {
                Console.Clear();
                Console.WriteLine("Invalid choice. Try again.");
            }
            else
                break;
        }
    }

    private void StudentsMenu()
    {
        var entityService = new StudentService(EntityContext.Create<StudentEntity>(_dataFolderPath, _fileType));

        while (true)
        {
            Console.Clear();
            PrintSettings();
            Console.WriteLine("===== Students =====");

            Console.WriteLine("1. Show all");
            Console.WriteLine("2. Show at");
            Console.WriteLine("3. Add");
            Console.WriteLine("4. Remove");
            Console.WriteLine("5. Show with healthy weight");
            Console.WriteLine("0. Back");

            Console.Write("Select action: ");
            var action = Console.ReadLine();

            switch (action)
            {
                case "1": ShowAll(entityService); break;
                case "2": ShowAt(entityService); break;
                case "3": AddStudent(entityService); break;
                case "4": RemoveAt(entityService); break;
                case "5": ShowStudentsWithHealthyWeight(entityService); break;
                case "0": return;
                default: Console.WriteLine("Invalid action."); break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    private void LibrariansMenu()
    {
        var entityService = new LibrarianService(EntityContext.Create<LibrarianEntity>(_dataFolderPath, _fileType));

        while (true)
        {
            Console.Clear();
            PrintSettings();
            Console.WriteLine("===== Librarians =====");

            Console.WriteLine("1. Show all");
            Console.WriteLine("2. Show at");
            Console.WriteLine("3. Add");
            Console.WriteLine("4. Remove");
            Console.WriteLine("0. Back");

            Console.Write("Select action: ");
            var action = Console.ReadLine();

            switch (action)
            {
                case "1": ShowAll(entityService); break;
                case "2": ShowAt(entityService); break;
                case "3": AddLibrarian(entityService); break;
                case "4": RemoveAt(entityService); break;
                case "0": return;
                default: Console.WriteLine("Invalid action."); break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    private void SoftwareDevelopersMenu()
    {
        var entityService = new SoftwareDeveloperService(EntityContext.Create<SoftwareDeveloperEntity>(_dataFolderPath, _fileType));

        while (true)
        {
            Console.Clear();
            PrintSettings();
            Console.WriteLine("===== Software developers =====");

            Console.WriteLine("1. Show all");
            Console.WriteLine("2. Show at");
            Console.WriteLine("3. Add");
            Console.WriteLine("4. Remove");
            Console.WriteLine("0. Back");

            Console.Write("Select action: ");
            var action = Console.ReadLine();

            switch (action)
            {
                case "1": ShowAll(entityService); break;
                case "2": ShowAt(entityService); break;
                case "3": AddSoftwareDeveloper(entityService); break;
                case "4": RemoveAt(entityService); break;
                case "0": return;
                default: Console.WriteLine("Invalid action."); break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    private void ShowAll<TEntity, TModel>(EntityService<TEntity, TModel> entityService) where TEntity: PersonEntity where TModel : Person
    {
        var persons = entityService.GetAll();

        if (persons.Count == 0)
        {
            Console.WriteLine("No data");
            return;
        }

        foreach (var (item, index) in persons.Select((value, i) => (value, i)))
            Console.WriteLine($"{index} {item.ToString()}");
    }

    private void ShowAt<TEntity, TModel>(EntityService<TEntity, TModel> entityService) where TEntity : PersonEntity where TModel : Person
    {
        Console.Write("Input index: ");
        var index = int.Parse(Console.ReadLine()!);
        var persons = entityService.GetAll();
        Console.WriteLine(persons[index].ToString());
        Console.WriteLine("Make him ride a bike? (y/n): ");
        var rideBikeAnswer = Console.ReadLine();

        if (rideBikeAnswer == "y")
            persons[index].RideBike(str => Console.WriteLine(str));
    }

    private void RemoveAt<TEntity, TModel>(EntityService<TEntity, TModel> entityService) where TEntity : PersonEntity where TModel : Person
    {
        Console.Write("Input index: ");
        var index = int.Parse(Console.ReadLine()!);
        entityService.RemoveAt(index);
        Console.WriteLine("Item removed");
    }

    private void AskPersonFields(out string firstName, out string lastName, out string passport)
    {
        Console.Write("FirstName: ");
        firstName = Console.ReadLine()!;
        Console.Write("LastName: ");
        lastName = Console.ReadLine()!;
        Console.Write("Passport in format AA000000: ");
        passport = Console.ReadLine()!;
    }

    private void AddStudent(StudentService service)
    {
        AskPersonFields(out var firstName, out var lastName, out var passport);
        Console.Write("Hight: ");
        var height = int.Parse(Console.ReadLine()!);
        Console.Write("Weight: ");
        var weight = int.Parse(Console.ReadLine()!);
        Console.Write("Student ID card in format AA00000000: ");
        var studentIdCard = Console.ReadLine()!;

        var newStudent = new Student { FirstName = firstName, LastName = lastName, Passport = passport, Height = height, Weight = weight, StudentIdCard = studentIdCard };
        service.Add(newStudent);
        Console.WriteLine("Student added");
    }

    private void AddLibrarian(LibrarianService service)
    {
        AskPersonFields(out var firstName, out var lastName, out var passport);
        Console.Write("Department: ");
        var department = Console.ReadLine()!;

        var newLibrarian = new Librarian { FirstName = firstName, LastName = lastName, Passport = passport, Department = department };
        service.Add(newLibrarian);
        Console.WriteLine("Librarian added");
    }

    private void AddSoftwareDeveloper(SoftwareDeveloperService service)
    {
        AskPersonFields(out var firstName, out var lastName, out var passport);
        Console.Write("Experience: ");
        var experience = int.Parse(Console.ReadLine()!);
        Console.Write("Knowledge of programming languages ​​(separated by commas): ");
        var pgLanguages = Console.ReadLine()?.Split(",").Select(x => x.Trim()).ToList() ?? [];

        var newSoftwareDeveloper = new SoftwareDeveloper { FirstName = firstName, LastName = lastName, Passport = passport, Experience = experience, PgLanguages = pgLanguages };
        service.Add(newSoftwareDeveloper);
        Console.WriteLine("Software developer added");
    }

    private void ShowStudentsWithHealthyWeight(StudentService service)
    {
        var students = service.GetStudentsWithHealthyWeight();

        if (students.Count == 0)
        {
            Console.WriteLine("No data");
            return;
        }

        foreach (var (item, index) in students.Select((value, i) => (value, i)))
            Console.WriteLine($"{index} {item.ToString()}");
    }

    private void PrintSettings()
    {
        Console.WriteLine($"===== DataFolderPath: {_dataFolderPath} | Serrialization type: {_fileType} =====");
    }
}

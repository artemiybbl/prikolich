public class ContactsBook
{
    private readonly ContactDatabase _contactDatabase = new ContactDatabase("contacts.db");
    private readonly List<Contact> _contacts = new List<Contact>();

    public void AddContact(Contact contact)
    {
        _contacts.Add(contact);
        Console.WriteLine("Контакт создан.");
    }

    public void ViewAllContacts()
    {
        if (_contacts.Count == 0)
        {
            Console.WriteLine("Список контактов пуст.");
            return;
        }

        foreach (var contact in _contacts)
        {
            Console.WriteLine(contact);
        }
    }

    public void SearchContacts()
    {
        Console.WriteLine("Поиск по:");
        Console.WriteLine("1. Имени");
        Console.WriteLine("2. Фамилии");
        Console.WriteLine("3. Имени и фамилии");
        Console.WriteLine("4. Телефону");
        Console.WriteLine("5. E-mail");

        Console.Write("> ");
        int searchOption;
        while (!int.TryParse(Console.ReadLine(), out searchOption) || searchOption < 1 || searchOption > 5)
        {
            Console.WriteLine("Неверный выбор. Пожалуйста, введите корректный номер.");
            Console.Write("> ");
        }

        Console.Write("Запрос: ");
        string query = Console.ReadLine() ?? "";

        Console.WriteLine("Идет поиск…");
        List<Contact> results = new List<Contact>();

        foreach (var contact in _contacts)
        {
            switch (searchOption)
            {
                case 1 when contact.Name.ToLower().Contains(query.ToLower()):
                case 2 when contact.Surname.ToLower().Contains(query.ToLower()):
                case 3 when (contact.Name.ToLower() + " " + contact.Surname.ToLower()).Contains(query.ToLower()):
                case 4 when contact.Phone.ToLower().Contains(query.ToLower()):
                case 5 when contact.Email.ToLower().Contains(query.ToLower()):
                    results.Add(contact);
                    break;
            }
        }

        Console.WriteLine($"Результаты ({results.Count}) :");
        int count = 1;
        foreach (var contact in results)
        {
            Console.WriteLine($"#{count++}\n{contact}");
        }
    }

    public void SaveContactsToFile(string filename)
    {
        string extension = Path.GetExtension(filename)?.ToLower();

        if (!string.IsNullOrWhiteSpace(filename))
        {
            switch (extension)
            {
                case ".db":
                case ".json":
                case ".xml":
                    _contactDatabase.SaveContacts(_contacts);
                    Console.WriteLine("Контакты сохранены в файл.");
                    break;
                default:
                    Console.WriteLine("Неподдерживаемое расширение файла.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Неверное имя файла.");
        }
    }

    public void LoadContactsFromFile(string filename)
    {
        string extension = Path.GetExtension(filename)?.ToLower();

        if (!string.IsNullOrWhiteSpace(filename))
        {
            switch (extension)
            {
                case ".db":
                case ".json":
                case ".xml":
                    _contacts.Clear();
                    _contacts.AddRange(_contactDatabase.LoadContacts(filename));
                    Console.WriteLine("Контакты загружены из файла.");
                    break;
                default:
                    Console.WriteLine("Неподдерживаемое расширение файла.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Неверное имя файла.");
        }
    }
}

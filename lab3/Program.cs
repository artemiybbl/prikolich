public class Program
{
    private static int GetValidInput()
    {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine("Неверный ввод. Пожалуйста, введите число.");
        }
        return choice;
    }

    public static void Main(string[] args)
    {
        ContactsBook contactsBook = new ContactsBook();
        int choice;

        do
        {
            Console.WriteLine("Выберите действие, введите номер и нажмите [Enter]. Затем следуйте инструкциям.");
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Просмотр всех контактов");
            Console.WriteLine("2. Поиск контактов");
            Console.WriteLine("3. Добавить новый контакт");
            Console.WriteLine("4. Сохранить контакты в файл");
            Console.WriteLine("5. Загрузить контакты из файла");
            Console.WriteLine("6. Выход");

            Console.Write("> ");
            choice = GetValidInput();

            switch (choice)
            {
                case 1:
                    contactsBook.ViewAllContacts();
                    break;
                case 2:
                    contactsBook.SearchContacts();
                    break;
                case 3:
                    Console.Write("Введите имя: ");
                    string имя = Console.ReadLine();

                    Console.Write("Введите фамилию: ");
                    string фамилия = Console.ReadLine();

                    Console.Write("Введите номер телефона: ");
                    string телефон = Console.ReadLine();

                    Console.Write("Введите адрес электронной почты: ");
                    string электроннаяПочта = Console.ReadLine();

                    Contact новыйКонтакт = new Contact
                    {
                        Name = имя,
                        Surname = фамилия,
                        Phone = телефон,
                        Email = электроннаяПочта
                    };

                    contactsBook.AddContact(новыйКонтакт);
                    break;
                case 4:
                    Console.Write("Введите имя файла для сохранения контактов: ");
                    string saveFileName = Console.ReadLine();
                    contactsBook.SaveContactsToFile(saveFileName);
                    break;
                case 5:
                    Console.Write("Введите имя файла для загрузки контактов: ");
                    string loadFileName = Console.ReadLine();
                    contactsBook.LoadContactsFromFile(loadFileName);
                    break;
                case 6:
                    Console.WriteLine("Выход...");
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, введите корректный номер.");
                    break;
            }
        } while (choice != 6);
    }
}
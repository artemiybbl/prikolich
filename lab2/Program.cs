public class Program
    {
        // Точка входа
        public static void Main(string[] args)
        {
            ContactsBook contactsBook = new ContactsBook();
            int choice;

            do
            {
                Console.WriteLine("Enter the number of action and press [Enter]. Then follow instructions.");
                Console.WriteLine("Menu:");
                Console.WriteLine("1. View all contacts");
                Console.WriteLine("2. Search");
                Console.WriteLine("3. New contact");
                Console.WriteLine("4. Exit");

                Console.Write("> ");
                choice = int.Parse(Console.ReadLine() ?? "0");

                switch (choice)
                {
                    case 1:
                        contactsBook.ViewAllContacts();
                        break;
                    case 2:
                        contactsBook.SearchContacts();
                        break;
                    case 3:
                        Console.WriteLine("New contact");
                        Console.Write("Name: ");
                        string name = Console.ReadLine() ?? "";
                        Console.Write("Surname: ");
                        string surname = Console.ReadLine() ?? "";
                        Console.Write("Phone: ");
                        string phone = Console.ReadLine() ?? "";
                        Console.Write("E-mail: ");
                        string email = Console.ReadLine() ?? "";

                        Contact newContact = new Contact
                        {
                            Name = name,
                            Surname = surname,
                            Phone = phone,
                            Email = email
                        };

                        contactsBook.AddContact(newContact);
                        break;
                    case 4:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            } while (choice != 4);
        }
    }

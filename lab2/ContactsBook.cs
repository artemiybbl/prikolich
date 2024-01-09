public class ContactsBook
    {
        private readonly List<Contact> contacts = new List<Contact>();

        // Добавление нового контакта
        public void AddContact(Contact contact)
        {
            contacts.Add(contact);
            Console.WriteLine("Contact created.");
        }

        // Просмотр всех контактов
        public void ViewAllContacts()
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("Contacts list is empty.");
                return;
            }

            foreach (var contact in contacts)
            {
                Console.WriteLine(contact);
            }
        }

        // Поиск контактов по критериям
        public void SearchContacts()
        {
            Console.WriteLine("Search by");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Surname");
            Console.WriteLine("3. Name and Surname");
            Console.WriteLine("4. Phone");
            Console.WriteLine("5. E-mail");

            Console.Write("> ");
            int searchOption = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Request: ");
            string query = Console.ReadLine() ?? "";

            Console.WriteLine("Searching…");
            List<Contact> results = new List<Contact>();

            foreach (var contact in contacts)
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

            Console.WriteLine($"Results ({results.Count}) :");
            int count = 1;
            foreach (var contact in results)
            {
                Console.WriteLine($"#{count++}\n{contact}");
            }
        }
    }
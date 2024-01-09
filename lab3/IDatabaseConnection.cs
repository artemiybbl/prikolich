public interface IDatabaseConnection
{
    void SaveContacts(List<Contact> contacts);
    List<Contact> LoadContacts(string filePath);
}
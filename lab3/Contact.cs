public class Contact
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public override string ToString()
    {
        return $"Имя: {Name}\nФамилия: {Surname}\nТелефон: {Phone}\nE-mail: {Email}\n";
    }
}
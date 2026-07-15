
User user = new User
{
    Id = 1,
    Name = "Fernando",
    Email = "email@gmail.com",
};

List<User> users = new List<User>();
// List<User> users = new();

users.Add(user);

public class User 
{
    public int Id { get; set; }
    public string Email { get; set; } = "";
    public string Name { get; set; } = "";
}


User user = new User
{
    Id = 1,
    Name = "Fernando",
    Email = "email@gmail.com",
};

List<User> users = new List<User>();
// List<User> users = new();

users.Add(user);

Console.WriteLine(user.Name);

static void CreateUser(List<User> users, User newUser)
{
    users.Add(newUser);
}

CreateUser(users, new User
{
    Id = 2,
    Name = "Joe",
    Email = "email",
});

static void ReadAllUsers(List<User> users)
{
    foreach (User u in users)
    {
        Console.WriteLine($"{u.Id}: {u.Name} - {u.Email}");
    }
}           

ReadAllUsers(users);

static User? ReadUserById(List<User> users, int id)
{
    // User? foundUser = users.FirstOrDefault(user => user.Id == 1); // searches list and returns first matching or Null if no match
    // the '?' means variable is allowed to contain null
    return users.FirstOrDefault(user => user.Id == 1);
}

User? foundUser = ReadUserById(users, 1);

if (foundUser == null)
{
    Console.WriteLine("no user found");
} 
else 
{
    Console.WriteLine($"Id: {foundUser.Id}, Name: {foundUser.Name}");
}


public class User 
{
    public int Id { get; set; }
    public string Email { get; set; } = "";
    public string Name { get; set; } = "";
}

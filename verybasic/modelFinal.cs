
public class User 
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
}

public class Program
{
    public static void Main()
    {
        List<User> users = new(); 
    }

    static void CreateUser(List<User> users, User newUser)
    {
        users.Add(newUser);
    }

    static void ReadAllUsers(List<User> users)
    {
        foreach (User u in users)
        {
            Console.WriteLine($"{u.Id}: {u.Name} - {u.Email}");
        }
    }

    static User? readUserById(List<User> users, int id)
    {
        return users.FirstOrDefault(user => user.Id == id);
    }

    static bool updateUser(
            List<User> users,
            int id,
            string newName,
            string newEmail)
    {
        User? user = users.FirstOrDefault(user => user.Id == id);
        if (user == null)
        {
            return false;
        }

        user.Name = newName;
        user.Email = newEmail;
        
        return true;
    }

    static bool deleteUser(List<User> users, int id)
    {
        User? user = users.FirstOrDefault(user => user.Id == id);
        
        if (user == null)
        {
            return false;
        }

        users.Remove(user);
        
        return true;
    }
}


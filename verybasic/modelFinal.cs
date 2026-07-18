
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

        // CREATE  
        CreateUser(users, new User
        {
            Id = 1,
            Name = "Fernando",
            Email = "gmail"
        });

        CreateUser(users, new User
        {
            Id = 2,
            Name = "John",
            Email = "hotmail"
        });

        // READ
        Console.WriteLine("All users:");
        ReadAllUsers(users);

        // READ one 
        Console.WriteLine("user with id == 2");
        User? user = readUserById(users, 2);

        if (user == null)
        {
            Console.WriteLine("user not found");
        }
        else
        {
            Console.WriteLine($"{user.Id}: {user.Name} - {user.Email}");
        }

        // UPDATE
        Console.WriteLine("Updating User2");
        bool wasUpdated = updateUser(
            users,
            2,
            "jane",
            "newemail@example.com"
        );

        Console.WriteLine(
            wasUpdated
                ? "User updated successfully."
                : "User not found."
        );

        // READ AFTER UPDATE
        Console.WriteLine("\nUsers after update:");
        ReadAllUsers(users);

        // DELETE
        Console.WriteLine("\nDeleting user 2:");

        bool wasDeleted = deleteUser(users, 2);

        Console.WriteLine(
            wasDeleted
                ? "User deleted successfully."
                : "User not found."
        );

        // READ AFTER DELETE
        Console.WriteLine("\nUsers after deletion:");
        ReadAllUsers(users);

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


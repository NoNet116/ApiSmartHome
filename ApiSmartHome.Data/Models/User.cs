using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSmartHome.Data.Models
{
    [Table("Users")]
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
    }
}

/*
 Что такое [Table("Users")]?
Атрибут [Table("Users")] используется в Entity Framework (EF) для указания имени таблицы в базе данных, 
которая будет соответствовать данному классу. В данном случае, это говорит Entity Framework, что класс 
User будет связан с таблицей Users в базе данных.

Когда ты используешь Entity Framework Core (EF Core) для работы с базой данных, ты часто создаёшь модели 
данных в виде классов C#. Эти классы затем соответствуют таблицам в базе данных. Атрибут [Table] помогает точно указать,
с какой таблицей будет работать EF Core, особенно если имя таблицы отличается от имени класса.
 */
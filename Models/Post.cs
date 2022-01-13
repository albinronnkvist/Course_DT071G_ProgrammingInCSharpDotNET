using System.ComponentModel.DataAnnotations;

namespace ForumAPI.Models
{
    // The definition of a post with all properties that will be stored in the database.
    public class Post
    {
        [Key] // Sets the Id property as a primary key
        [Required] // The value of this property can not be null
        public int Id { get; set;}

        [Required]
        [MaxLength(150)] // The length of this property can not exceed 150 characters.
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Sets a default value of the current date and time.

        // If the dependent entity contains a property with a name matching the pattern: "<navigation property name>Id".
        // Then it will be configured as the foreign key
        [Required]
        public int UserId { get; set; } // So here we define a foreign key property in the dependent entity class

        // Add a navigation property to create a relationship with the User entity.
        [Required]
        public User User { get; set; } 
    }
}
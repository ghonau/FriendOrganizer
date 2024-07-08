using System.ComponentModel.DataAnnotations;

namespace FriendOrganizer.Model
{
    public class Friend
    {
        public  int Id { get; set; }
        [Required]
        [StringLength(50)] 
        public string FirstName { get; set; } = string.Empty;
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; }  
        public int? FavoriteLanguageId { get; set; }
        public ProgrammingLanguage? FavoriteLanaguge { get; set; }
    }
}

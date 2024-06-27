using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Task.Data.Modals
{
    public class UserWithdraws
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "User ID is a required field.")]
        public string? UserId { get; set; }

        [Required(ErrorMessage = "Username is a required field.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Withdrawl amount is a required field.")]
        public double WithdrawnAmount { get; set; }

        public DateTime WithdrawnOn { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
    }
}

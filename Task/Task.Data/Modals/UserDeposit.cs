using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task.Data.Modals
{
    public class UserDeposit
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "User ID is a required field.")]
        public string? UserId { get; set; }

        [Required(ErrorMessage = "Username is a required field.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Deposit amount is a required field.")]
        public double DepositAmount { get; set; }

        public DateTime DepositedOn { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
    }
}

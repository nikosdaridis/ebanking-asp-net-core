using System.ComponentModel.DataAnnotations;

namespace eBanking.ViewModels
{
    public class AccountViewModel
    {
        [Required(ErrorMessage = "AFM is required")]
        [RegularExpression("^\\d{9}$", ErrorMessage = "Please enter 9 digits")]
        public int AFM { get; set; }

        [Required(ErrorMessage = "Balance is required")]
        [RegularExpression("^[+-]?\\d*\\.?\\d+$", ErrorMessage = "Please enter any number")]
        public double Balance { get; set; }

        [Required(ErrorMessage = "Number is required")]
        [RegularExpression("^[0-9][0-9]*$", ErrorMessage = "Please enter an integer greater than or equal to 0")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Branch is required")]
        [StringLength(30, ErrorMessage = "Maximum 30 characters")]
        public string Branch { get; set; } = "";

        [Required(ErrorMessage = "Type is required")]
        [StringLength(30, ErrorMessage = "Maximum 30 characters")]
        public string Type { get; set; } = "";
    }
}

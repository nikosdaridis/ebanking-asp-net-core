using System.ComponentModel.DataAnnotations;

namespace eBanking.ViewModels;

public class BankUserViewModel
{
    [Required(ErrorMessage = "First Name is required")]
    [RegularExpression("^[a-zA-Z]{1,30}$", ErrorMessage = "Maximum 30 alphabetical characters")]
    public string FirstName { get; set; } = "";

    [Required(ErrorMessage = "Last Name is required")]
    [RegularExpression("^[a-zA-Z]{1,30}$", ErrorMessage = "Maximum 30 alphabetical characters")]
    public string LastName { get; set; } = "";

    [Required(ErrorMessage = "Address is required")]
    [StringLength(30, ErrorMessage = "Maximum 30 characters")]
    public string Address { get; set; } = "";

    [Required(ErrorMessage = "Phone is required")]
    [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter 10 digits")]
    public long Phone { get; set; }

    [Required(ErrorMessage = "AFM is required")]
    [RegularExpression("^\\d{9}$", ErrorMessage = "Please enter 9 digits")]
    public int AFM { get; set; }

    [Required(ErrorMessage = "Username / Email is required")]
    [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter an email address")]
    [DataType(DataType.EmailAddress)]
    public string UserName { get; set; } = "";

    [Required(ErrorMessage = "Password is required")]
    [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";
}
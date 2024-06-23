using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Contract_Administrator.Models
{
    /// <summary>
    /// Model pro tabulku poradců.
    /// </summary>
    public class Adviser
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Toto pole je povinné"), NotNull]
        [DisplayName("Jméno")]
        public string FirstName { get; set; } = "";
        [Required(ErrorMessage = "Toto pole je povinné"), NotNull]
        [DisplayName("Příjmení")]
        public string LastName { get; set; } = "";
        [Required(ErrorMessage = "Toto pole je povinné"), NotNull]
        [EmailAddress(ErrorMessage = "Neplatná emailová adresa.")]
        [DisplayName("Email")]
        public string Email { get; set; } = "";
        [Required(ErrorMessage = "Toto pole je povinné"), NotNull]
        [Phone(ErrorMessage = "Nasprávné telefonní číslo.")]
        [DisplayName("Telefonní číslo")]
        public string TelNumber { get; set; }
        [Required(ErrorMessage = "Toto pole je povinné"), NotNull]
        [DisplayName("Rodné číslo")]
        public string PersonalIdNum { get; set; }
        [Required(ErrorMessage = "Toto pole je povinné"), NotNull]
        [Range(0, 130, ErrorMessage = "Nepltná hodnota věku.")]
        [DisplayName("Věk")]
        public int Age { get; set; }
        [DisplayName("Smlouvy")]
        public ICollection<ContractAdviser>? ContractAdviser { get; set; }
    }
}

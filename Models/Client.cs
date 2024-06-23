using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Contract_Administrator.Models
{
    /// <summary>
    /// Model pro tabulku Klientů.
    /// </summary>
    public class Client
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
        [Required, NotNull]
        [Range(0, 130, ErrorMessage = "Nepltná hodnota věku.")]
        [DisplayName("Věk")]
        public int Age { get; set; }
        [NotNull]
        [DisplayName("Uzavřené Smlouvy")]
        public ICollection<Contract>? Contracts { get; set; } = new List<Contract>();

    }

    
}

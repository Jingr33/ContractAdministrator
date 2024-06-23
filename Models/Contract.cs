using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Contract_Administrator.Models
{
    /// <summary>
    /// Model pro tabulku Smluv.
    /// </summary>
    public class Contract
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Toto pole je povinné"), NotNull]
        [DisplayName("Registrační číslo")]
        public int RegistrationNumber { get; set; }
        [Required(ErrorMessage = "Toto pole je povinné"), NotNull]
        [DisplayName("Instituce")]
        public string Institution { get; set; } = "";
        [Required(ErrorMessage = "Toto pole je povinné"), NotNull]
        [DisplayName("Správce smlouvy")]
        public int ContractAdmin { get; set; }
        [Required(ErrorMessage = "Toto pole je povinné"), NotNull]
        [DisplayName("Datum uzavření")]
        public DateOnly ConclusionDate { get; set; }
        [Required(ErrorMessage = "Toto pole je povinné"), NotNull]
        [DisplayName("Datum platnosti")]
        public DateOnly ValidityDate{ get; set; }
        [Required(ErrorMessage = "Toto pole je povinné"), NotNull]
        [DisplayName("Datum ukončení")]
        public DateOnly ExpirationDate { get; set; }
        // tady mají být ještě vztahy mezi daty - bacha na to

        [Required(ErrorMessage = "Toto pole je povinné")]
        [DisplayName("Url adresa PDF souboru se smlouvou")]
        public string File { get; set; }

        [Required(ErrorMessage = "Toto pole je povinné"), NotNull]
        [ForeignKey("Client")]
        [DisplayName("Klient")]
        public int ClientId { get; set; } = new int();
        public virtual Client Client { get; set; }

        [ForeignKey("Adviser")]
        [DisplayName("Poradci")]
        public List<int>? AdviserId { get; set; } = new List<int>();
        public ICollection<ContractAdviser>? ContractAdviser { get; set; }
    }
}

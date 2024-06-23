using Contract_Administrator.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Contract_Administrator.ViewModels
{
    public class ContractViewModel
    {
        public Contract Contract { get; set; }

        [Required(ErrorMessage = "Toto pole je povinné"), NotNull]
        [DisplayName("Registrační číslo")]
        public int RegistrationNumber { get; set; }
        [Required(ErrorMessage = "Toto pole je povinné"), NotNull]
        [DisplayName("Instituce")]
        public string Institution { get; set; } = "";
        [Required(ErrorMessage = "Toto pole je povinné"), NotNull]
        [DisplayName("Datum uzavření")]
        public DateOnly ConclusionDate { get; set; }
        [Required(ErrorMessage = "Toto pole je povinné"), NotNull]
        [DisplayName("Datum platnosti")]
        public DateOnly ValidityDate { get; set; }
        [Required(ErrorMessage = "Toto pole je povinné"), NotNull]
        [DisplayName("Datum ukončení")]
        public DateOnly ExpirationDate { get; set; }
        // tady mají být ještě vztahy mezi daty - bacha na to

        [Required]
        [DisplayName("Url adresa pdf se smlouvou")]
        public string File { get; set; }

        public IEnumerable<SelectListItem>? Clients { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem>? ContractAdmin { get; set; }
        public IEnumerable<SelectListItem>? Advisers { get; set; }
    }
}

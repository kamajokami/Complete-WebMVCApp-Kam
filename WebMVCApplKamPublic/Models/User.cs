using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebMVCApplKamPublic.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; init; }


        [Required(ErrorMessage = "Vyplňte jméno!")]
        [DisplayName("Jméno")]
        [StringLength(150, ErrorMessage = "Jméno je příliš dlouhé. Počet znaků délky jména je max. 150.")]
        public string Name { get; set; }

        // Řekněme, že příjmení je nepovinné
        [DisplayName("Příjmení")]
        [StringLength(200, ErrorMessage = "Příjmení je příliš dlouhé. Počet znaků délky příjmení je max. 200.")]
        public string Surname { get; set; } = "";


        // Email je zároveň unikátní
        [Required(ErrorMessage = "Email je povinný!")]
        [DisplayName("E-mailový účet")]
        [MaxLength(150, ErrorMessage = "Email je příliš dlouhý. Délka emailu je max. 150 znaků.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail je neplatný!")]
        public string Email { get; set; }






        [Required(ErrorMessage = "Pohlaví je povinné!")]
        [DisplayName("Pohlaví")]
        public string Gender { get; set; }


        [DisplayName("Telefonní číslo")]
        [StringLength(25, ErrorMessage = "Telefonní číslo je příliš dlouhé. Počet znaků délky tel. čísla je max. 25.")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Pohlaví je povinné!")]
        [DisplayName("Vlastník nemovitosti")]
        public string PropertyOwner { get; set; }

    }
}

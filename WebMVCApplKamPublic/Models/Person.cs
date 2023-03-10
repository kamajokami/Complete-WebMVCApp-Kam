using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMVCApplKamPublic.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Person
    {
        /*
        
        Úkolem je naprogramovat webovou aplikaci v ASP.NET MVC, která umožňuje vložit do 
        databáze Osobu (Jméno, Příjmení, E-mailová adresa). 
        Aplikace zároveň umožňuje Osobu v databázi upravit a smazat.

         */

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; init; }


        [Required(ErrorMessage ="Vyplňtě jméno!")]
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
        [DataType(DataType.EmailAddress, ErrorMessage ="E-mail je neplatný!")]
        public string Email { get; set; }

    }
}

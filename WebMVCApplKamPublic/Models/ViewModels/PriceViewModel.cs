using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMVCApplKamPublic.Models.ViewModels
{
    [Index(nameof(Title), IsUnique = true)]
    public class PriceViewModel
    {
        // Sklad na prodej zboží
        // Euro znak Alt+0128
        public string CurrencyValue = "€";


        public PriceViewModel() 
        {
            UniqueName = new List<string>();
        }



        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        // Předmět ceny - třeba pánské triko
        [Required(ErrorMessage = "Předmět musí být vyplněn!")]
        [StringLength(50, ErrorMessage = "Předmět je příliš dlouhý. Počet znaků délky předmětu ceny je max. 50.")]
        [DisplayName("Předmět ceny")]
        public string Title { get; set; }


        // Měna platby
        public string Currency { get; set; }


        // Cena v uvedené měně
        [BindProperty(SupportsGet = true)]
        public double PriceMoney { get; set; }


        // Šířka třeba 100*50
        [BindProperty(SupportsGet = true)]
        public string Width { get; set; }


        // CheckBox
        public bool IsSale { get; set; }




        // CheckBox - tests
        public bool IsCommodityActive { get; set; }

        public string IsCommodityActiveCheckboxDescription { get; set; }

        public string Value { get; set; }

        [NotMapped]
        public List<CheckboxOption> Checkboxes { get; set; }

        [NotMapped]
        public List<string> UniqueName { get; set; }


        [NotMapped]
        //[ForeignKey("CheckBoxOptions")]
        [DisplayName("Vícenásobný výběr")]
        public int? OptionId { get; set; }
        public virtual CheckboxOption Option { get; set; }






        // CheckBox
        public bool IsPropertyOwner { get; set; }


        // Nakupující je muž
        public bool IsBuyerMan { get; set; }


        // Nakupující je žena
        public bool IsBuyerWoman { get; set; }


        // Checkbox pro odeslání souhlasu?
        public bool SendConsent { get; set; }


    }
}

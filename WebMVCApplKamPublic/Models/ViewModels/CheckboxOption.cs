using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMVCApplKamPublic.Models.ViewModels
{
    public class CheckboxOption
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CheckboxOptionId { get; set; }

        // CheckBox
        public bool IsChecked { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }
    }
}

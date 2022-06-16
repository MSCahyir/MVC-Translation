using System;
using System.ComponentModel.DataAnnotations;

namespace Translation.Models
{
    public class FunTranslation
    {
        public int Id { get; set; }
        [Required]
        public string NormalText { get; set; }
        [Required]
        public string TranslatedText { get; set; }
    }
}

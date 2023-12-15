using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace pizza_mama.Models
{
    public class Pizza
    {
        [JsonIgnore]
        public int PizzaId { get; set; }
        [Display(Name = "Nom")]
        public string nom { get ; set; }
        [Display(Name = "Prix ($)")]
        public float prix { get; set; }
        [Display (Name = "Végétarienne")]
        public bool vegetarienne {  get; set; }
        [JsonIgnore]
        public string ingredients { get; set; }

        [NotMapped]
        [JsonPropertyName("ingredients")]
        public List<string> listeIngredients
        {
            get
            {
                if(ingredients == null)
                {
                    return null;
                }
                return ingredients.Split(", ").ToList();
            }
        }
    }
}

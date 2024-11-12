using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MiPymes_Front_end_C_.Models
{
    public class MiPyme
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El RNC es obligatorio")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "El RNC debe contener solo numeros y debe de tener 9 digitos.")]
        public string? RNC { get; set; }
        public string? Empresa { get; set; }
        public string? Fecha_Emision { get; set; }
        public string? Fecha_de_Vencimiento { get; set; }
        public string? Clasificacion { get; set; }
        [Required(ErrorMessage ="Debe especificar la actividad")]
        [RegularExpression(@"^.{5,100}$", ErrorMessage = "La actividad debe contener entre 5 a 100 caracteres")]
        public string? Actividad { get; set; }
    }
}

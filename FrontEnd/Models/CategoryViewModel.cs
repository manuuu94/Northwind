using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class CategoryViewModel
    {

        public int? CategoryId { get; set; }
        //[Required]
        public string? CategoryName { get; set; } /*= null!;*/
        //el ? significa que puede ser nullable, puede ser quitado
        public string? Description { get; set; }

        //se comenta porque el view en el backend no lo incluye tampoco. 
        //public byte[] Picture { get; set; }

    }
}

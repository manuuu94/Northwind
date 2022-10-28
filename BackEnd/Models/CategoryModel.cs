using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class CategoryModel
    {
        //copiar los atributos de la entidad, que nos interesa tener en la nueva vista
        //un api deberia hacer referencia a este MODELO y no a la ENTIDAD. 
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }


        public byte[]? Picture { get; set; }

    }
}

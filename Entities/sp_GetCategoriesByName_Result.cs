using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{//por no ser una entidad de base de datos, no lleva llave primaria. KEYLESS.
    //para que entity framework sepa que no estamos mapeando una tabla
    [Keyless]
    public class sp_GetCategoriesByName_Result
    {
        //se usan los mismos atributos de la entidad, porque el SP devuelve esos mismos atributos
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public byte[]? Picture { get; set; }


    }
}

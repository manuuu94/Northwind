using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class ProductViewModel
    {

        [Key]
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }

        [Display(Name = "Proveedor")]
        public int SupplierID { get; set; }
        public IEnumerable<SupplierViewModel> Suppliers { get; set; }        
        public SupplierViewModel Supplier { get; set; }

        public int CategoryID { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public CategoryViewModel Category { get; set; }

        public bool Discontinued { get; set; }


        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }



    }
}

using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class OrderViewModel
    {
        [Key]
        public int OrderId { get; set; }

        public string? CustomerId { get; set; }
        public List<CustomerViewModel> Customers{ get; set; }
        public CustomerViewModel Customer { get; set; }

        public int? EmployeeId { get; set; }
        public List<EmployeeViewModel> Employees { get; set; }
        public EmployeeViewModel Employee { get; set; }


        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public List<ShipperViewModel> Shippers { get; set; }
        public ShipperViewModel Shipper { get; set; }

        public decimal? Freight { get; set; }
        public string? ShipName { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipCity { get; set; }
        public string? ShipRegion { get; set; }
        public string? ShipPostalCode { get; set; }
        public string? ShipCountry { get; set; }
    }
}

//25:52
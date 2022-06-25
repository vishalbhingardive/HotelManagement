using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Data.Models
{
    public class Location
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        //public int DistrictRefId { get; set; }
        //[ForeignKey("DistrictRefId")]
        //public District? District { get; set; }
        //public int StateRefId { get; set; }
        //[ForeignKey("DistrictRefId")]
        //public State? State { get; set; }
        //public int CountryRefId { get; set; }
        //[ForeignKey("DistrictRefId")]
        //public Country? Country { get; set; }    
    }
}


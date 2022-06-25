using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Data.Models
{
    public class Hotel
    {
        public int Id { get; set; } 
        public string Name { get; set; }   
        public string Rooms { get; set; }
        public int PerDayCost { get; set; }
        public int LocationRefId { get; set; }

        [ForeignKey("LocationRefId")]
        public Location? Location { get; set; }
        public int? CheckInRefId { get; set; }

        [ForeignKey(nameof(CheckInRefId))]
        public CheckInOutSystem? CheckInSystem  { get; set; } = null!;

        public int? CheckOutRefId { get; set; }

        [ForeignKey(nameof(CheckOutRefId))]
        public CheckInOutSystem? CheckOutSystem { get; set; } = null!;

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }
    }
}

namespace ParkingLotSystem.Models
{
    public class ParkingTicket
    {
        public int Id { get; set; }
        public Vehicle Vehicle { get; set; }
        public ParkingSpace ParkingSpace { get; set; }
    }
}
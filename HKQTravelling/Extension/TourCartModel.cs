using static System.Runtime.InteropServices.JavaScript.JSType;
using HKQTravelling.Models;


namespace HKQTravelling.Extension
{
    public class TourCartModel
    {
        private readonly ApplicationDBContext data;
        public TourCartModel(ApplicationDBContext data)
        {
            this.data = data;
        }
        public long iIdTour { get; set; }

        public string sTourName { get; set; }

        public int iQuantityofAldult { get; set; }

        public decimal? dprice { get; set; }

        public decimal? dTotalpriceTour
        {
            get { return dprice * iQuantityofAldult; }
        }

        public DateTime CreatedDate { get; set; }

        public DateTime TimGoing { get; set; }

        public int iQuantityofKid { get; set; }

        public int iKidAges{ get; set; }

        public string sTicketTypes { get; set; }


        public TourCartModel(long TourId)
        {
            iIdTour = TourId;
            Tours tours = data.tours.Single(n => n.TourId == iIdTour);
            sTourName = tours.TourName;
            iQuantityofAldult = 1;
            iQuantityofKid = 1;
            iKidAges = 1;
            dprice = tours.Price;
/*            TicketTypes ticketTypes = ticketTypes
            sTicketTypes = 
            CreatedDate = DateTime.Now;*/

        }
    }
}

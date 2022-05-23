using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlinesReservationSystem
{
    class Ticket
    {

        // Attributes
        private int ticketNumber;
        private string flight;
        private string ticketType;
        private string date;
        private string name;
        private string seat;

        // Constructors
        public Ticket() { }

        public Ticket(int tNumber, string f, string tType, string d, string n, string s)
        {
            ticketNumber = tNumber;
            flight = f;
            ticketType = tType;
            date = d;
            name = n;
            seat = s;
        }

        public int TicketNumber
        {
            get { return ticketNumber; }
            set { ticketNumber = value; }
        }
        public string Flight
        {
            get { return flight; }
            set { flight = value; }
        }
        public string TicketType
        {
            get { return ticketType; }
            set { ticketType = value; }
        }
        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Seat
        {
            get { return seat; }
            set { seat = value; }
        }

        // Methods
        
    }
}

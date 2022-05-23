using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using FlightNamespace;

namespace AirlinesReservationSystem
{
    public partial class Form1 : Form
    {


        Flight[] flights = new Flight[1000];
        Ticket[] tickets = new Ticket[1000];
        int nextTicketID = 1;
        int nextIndex = 0;
        int nextTicketIndex = 0;
        string currTicketType = "";

        string flightName;

        public Form1()
        {
            InitializeComponent();
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            listBox1.Items.Add("Date\t\tFrom\tTo\tFlight");
            listBox1.Items.Add("\n");

            string line;

            // Add the flights from the file
            using (StreamReader file = new StreamReader("flights.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    string[] words = line.Split(',');
                    flights[nextIndex] = new Flight(words[0], words[1], words[2], words[3], words[4],
                        words[5], words[6], words[7], words[8], words[9]);


                    listBox1.Items.Add(flights[nextIndex].DateTakeOff + "\t" + flights[nextIndex].DepartingFrom +
                        "\t" + flights[nextIndex].LandingIn + "\t" + flights[nextIndex].ID);

                    toolStripComboBox1.Items.Add(flights[nextIndex].DepartingFrom + "-" + flights[nextIndex].LandingIn);
                    toolStripComboBox2.Items.Add(flights[nextIndex].DepartingFrom + "-" + flights[nextIndex].LandingIn);

                    nextIndex++;
                }
            }

            // Add the tickets from the file
            using(StreamReader file = new StreamReader("tickets.txt"))
            {
                while((line = file.ReadLine()) != null)
                {
                    string[] words = line.Split(',');
                    tickets[nextTicketIndex] = new Ticket(int.Parse(words[0]), words[1], words[2], words[3], words[4], words[5]);

                    comboBox3.Items.Add(tickets[nextTicketIndex].Name);

                    nextTicketIndex++;
                    nextTicketID++;
                }
            }
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            addFlight();

            if (flights[0] != null)
            {
                displayFlight();
            }
        }

        private void addFlight()
        {

            Flight temp = new Flight();
            if (!txtFlightID.Text.Contains(" ") && txtFlightID.Text.Length != 0)
            {
                temp.ID = txtFlightID.Text;
            }
            else
            {
                MessageBox.Show("INVALID FIELD\nNo spaces or empty fields.", "Invalid Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!txtDeparting.Text.Contains(" ") && txtDeparting.Text.Length != 0)
            {
                temp.DepartingFrom = txtDeparting.Text;
            }
            else
            {
                MessageBox.Show("INVALID FIELD\nNo spaces or empty fields.", "Invalid Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!txtLanding.Text.Contains(" ") && txtLanding.Text.Length != 0)
            {
                temp.LandingIn = txtLanding.Text;
            }
            else
            {
                MessageBox.Show("INVALID FIELD\nNo spaces or empty fields.", "Invalid Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!txtStopovers.Text.Contains(" ") && txtStopovers.Text.Length != 0)
            {
                temp.Stopovers = txtStopovers.Text;
            }
            else
            {
                MessageBox.Show("INVALID FIELD\nNo spaces or empty fields.", "Invalid Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!txtAirlaneType.Text.Contains(" ") && txtAirlaneType.Text.Length != 0)
            {
                temp.AirlaneType = txtAirlaneType.Text;
            }
            else
            {
                MessageBox.Show("INVALID FIELD\nNo spaces or empty fields.", "Invalid Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (int.TryParse(txtNumberOfPassengers.Text, out int numb))
            {
                temp.NumberOfPassengers = numb;
            }
            else
            {
                MessageBox.Show("INVALID FIELD\nNo spaces or empty fields or invalid type.", "Invalid Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (int.TryParse(txtNumberOfRows.Text, out numb))
            {
                temp.NumberOfRows = numb;
            }
            else
            {
                MessageBox.Show("INVALID FIELD\nNo spaces or empty fields or invalid type.", "Invalid Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!txtEatingOnBoard.Text.Contains(" ") && txtEatingOnBoard.Text.Length != 0)
            {
                temp.EatingOnBoard = txtEatingOnBoard.Text;
            }
            else
            {
                MessageBox.Show("INVALID FIELD\nNo spaces or empty fields.", "Invalid Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            temp.DateTakeOff = dateTimePicker2.Value.ToString("MM-dd-yyyy");
            temp.DateLanding = dateTimePicker3.Value.ToString("MM-dd-yyyy");
            using (StreamWriter file = new StreamWriter(temp.DepartingFrom + "-" + temp.LandingIn + ".txt", true))
            {

            }
            using (StreamWriter file = new StreamWriter("flights.txt", true))
            {
                file.WriteLine(temp.ID + "," + temp.DepartingFrom + "," + temp.LandingIn + "," + temp.Stopovers +
                    "," + temp.AirlaneType + "," + temp.NumberOfPassengers.ToString() + "," + temp.NumberOfRows.ToString() +
                    "," + temp.EatingOnBoard + "," + temp.DateTakeOff + "," + temp.DateLanding);
            }
            flights[nextIndex] = temp;

            listBox1.Items.Add(flights[nextIndex].DateTakeOff + "\t" + flights[nextIndex].DepartingFrom +
                        "\t" + flights[nextIndex].LandingIn + "\t" + flights[nextIndex].ID);

            toolStripComboBox1.Items.Add(flights[nextIndex].DepartingFrom + "-" + flights[nextIndex].LandingIn);
            toolStripComboBox2.Items.Add(flights[nextIndex].DepartingFrom + "-" + flights[nextIndex].LandingIn);

            nextIndex++;
        }

        private void displayFlight()
        {
            string result = "";
            for (int i = 0; i < nextIndex; i++)
            {
                result += flights[i].ID + " ";
                result += flights[i].DepartingFrom + " ";
                result += flights[i].LandingIn + " ";
                result += flights[i].Stopovers + " ";
                result += flights[i].AirlaneType + " ";
                result += flights[i].NumberOfPassengers + " ";
                result += flights[i].NumberOfRows + " ";
                result += flights[i].EatingOnBoard + " ";

                result += "\n";
            }

            MessageBox.Show(result);
        }

        private bool checkSeat(string seat, string file)
        {
            string line;
            using(StreamReader reader = new StreamReader(file))
            {
                while((line = reader.ReadLine()) != null)
                {
                    string[] words = line.Split(' ');

                    foreach(string var in words)
                    {
                        if(seat == var)
                        {
                            return false;
                        }
                    }
                }
            }


            return true;
        }

        private void BtnBookSeat_Click(object sender, EventArgs e)
        {
            Flight currFlight;
            Ticket temp = new Ticket();


            if (lblTypeOfTicketAndDestination.Text.Length <= 0)
            {
                MessageBox.Show("No flight selected!\nGo to Passenger -> New Booking and choose a flight.", "Invalid Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!textBox1.Text.Contains(" ") && textBox1.Text.Length != 0)
            {
                if (!textBox2.Text.Contains(" ") && textBox2.Text.Length != 0)
                {
                    temp.Name = textBox1.Text + " " + textBox2.Text;
                }
                else
                {
                    MessageBox.Show("INVALID FIELD\nNo spaces or empty fields or invalid type.", "Invalid Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("INVALID FIELD\nNo spaces or empty fields or invalid type.", "Invalid Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (Flight var in flights)
            {
                if ((var.DepartingFrom + "-" + var.LandingIn) == lblCurrFlight.Text)
                {
                    currFlight = var;

                    if(!(currFlight.DateTakeOff == dateTimePicker1.Value.ToString("MM-dd-yyyy")))
                    {
                        MessageBox.Show(currFlight.DateTakeOff + " == " + dateTimePicker1.Value.ToString("MM-dd-yyyy"));
                        MessageBox.Show("There is no such flight on that DATE!\nCheck the All Flights Tab.");
                        return;
                    }
                    break;
                }
            }

            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                string seat = comboBox1.Text + comboBox2.Text;
                string seatFile = lblCurrFlight.Text + ".txt";

                // If the seat is free create the ticket ----------------------------------------------------
                if (checkSeat(seat,seatFile))
                {
                    using(StreamWriter writer = new StreamWriter(seatFile, true))
                    {
                        writer.Write(seat + " ");
                    }

                    temp.TicketNumber = nextTicketID;
                    nextTicketID++;
                    temp.Flight = lblCurrFlight.Text;
                    temp.TicketType = currTicketType;
                    temp.Date = dateTimePicker1.Value.ToString("MM-dd-yyyy");
                    temp.Seat = seat;

                    using(StreamWriter writer = new StreamWriter("tickets.txt",true))
                    {
                        writer.WriteLine(temp.TicketNumber + "," + temp.Flight + "," + temp.TicketType + "," +
                            temp.Date + "," + temp.Name + "," + temp.Seat);
                    }

                    comboBox3.Items.Add(temp.Name);

                    tickets[nextTicketIndex] = new Ticket(temp.TicketNumber, temp.Flight, temp.TicketType,
                        temp.Date, temp.Name, temp.Seat);
                    nextTicketIndex++;

                    
                    MessageBox.Show("Successfully Created!");
                }
                else
                {
                    MessageBox.Show("This seat is taken!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("INVALID SEAT!");
                return;
            }
            
            lblCurrFlight.Text = "";
            lblTypeOfTicketAndDestination.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Text = "";
            comboBox2.Text = "";
            
        }

        private void ToolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Flight currFlight;

            lblCurrFlight.Text = toolStripComboBox1.Text;
            currTicketType = "Two-way Ticket";
            tabControl1.SelectedTab = tabPage2;
            tabControl1.SelectedTab = tabPage1;
            lblTypeOfTicketAndDestination.Visible = true;
            lblTypeOfTicketAndDestination.Text = "Two-way ticket: " + toolStripComboBox1.Text;

            comboBox1.Items.Clear();
            foreach(Flight var in flights)
            {
                if((var.DepartingFrom + "-" + var.LandingIn) == lblCurrFlight.Text)
                {
                    currFlight = var;

                    for (int i = 0; i < currFlight.NumberOfRows; i++)
                    {
                        comboBox1.Items.Add(i + 1);
                    }
                    break;
                }
            }


        }

        private void TicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void ToolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Flight currFlight;

            lblCurrFlight.Text = toolStripComboBox2.Text;
            currTicketType = "One-way Ticket";
            tabControl1.SelectedTab = tabPage2;
            tabControl1.SelectedTab = tabPage1;
            lblTypeOfTicketAndDestination.Visible = true;
            lblTypeOfTicketAndDestination.Text = "One-way ticket: " + toolStripComboBox2.Text;

            comboBox1.Items.Clear();
            foreach (Flight var in flights)
            {
                if ((var.DepartingFrom + "-" + var.LandingIn) == lblCurrFlight.Text)
                {
                    currFlight = var;

                    for (int i = 0; i < currFlight.NumberOfRows; i++)
                    {
                        comboBox1.Items.Add(i + 1);
                    }
                    break;
                }
            }

        }

        private void BtnCheckSeat_Click(object sender, EventArgs e)
        {

            if(comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                if (checkSeat(comboBox1.Text + comboBox2.Text, lblCurrFlight.Text+".txt"))
                {
                    MessageBox.Show("This seat is free!");
                }
                else
                {
                    MessageBox.Show("This seat is taken!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("INVALID SEAT!");
            }
            
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (Ticket var in tickets)
                {
                    if (var.Name == comboBox3.Text)
                    {
                        string result = "\nTicket Number: " + var.TicketNumber +
                                        "\nFlight: " + var.Flight +
                                        "\n" + var.TicketType +
                                        "\nDate: " + var.Date +
                                        "\nName: " + var.Name +
                                        "\nSeat: " + var.Seat;
                        MessageBox.Show(result);
                    }
                }
            }
            catch (System.Exception ex)
            {

            }

            comboBox3.SelectedItem = null;
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage5;
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            bool exists = false;
            if(!(textBox3.Text.Contains(" ")) && textBox3.Text.Length != 0)
            {
                string flightID = textBox3.Text;

                for(int i = 0; i < 1000; i++)
                {
                    if(flights[i] != null)
                    {
                        if(flights[i].ID == flightID)
                        {
                            exists = true;
                        }
                    }
                }

                if (exists)
                {
                    File.Delete("tickets.txt");
                    using (StreamWriter writer = new StreamWriter("tickets.txt", true))
                    {

                        for (int i = 0; i < 1000; i++)
                        {
                            if (flights[i] != null)
                            {
                                if (flights[i].ID == flightID)
                                {
                                    for (int j = 0; j < 1000; j++)
                                    {
                                        if (tickets[j] != null)
                                        {
                                            if (tickets[j].Flight != flights[i].DepartingFrom + "-" + flights[i].LandingIn)
                                            {
                                                writer.WriteLine(tickets[j].TicketNumber + "," + tickets[j].Flight + "," + tickets[j].TicketType + "," +
                                                        tickets[j].Date + "," + tickets[j].Name + "," + tickets[j].Seat);
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }


                    File.Delete("flights.txt");

                    using (StreamWriter writer = new StreamWriter("flights.txt", true))
                    {

                        for (int i = 0; i < 1000; i++)
                        {
                            if (flights[i] != null)
                            {
                                if (!(flights[i].ID == flightID))
                                {
                                    writer.WriteLine(flights[i].ID + "," + flights[i].DepartingFrom + "," + flights[i].LandingIn + "," +
                                        flights[i].Stopovers + "," + flights[i].AirlaneType + "," + flights[i].NumberOfPassengers + "," +
                                        flights[i].NumberOfRows + "," + flights[i].EatingOnBoard + "," + flights[i].DateTakeOff + "," +
                                        flights[i].DateLanding);
                                }
                            }
                        }
                    }



                    for (int i = 0; i < 1000; i++)
                    {
                        flights[i] = null;
                    }

                    nextIndex = 0;

                    toolStripComboBox1.Items.Clear();
                    toolStripComboBox2.Items.Clear();

                    listBox1.Items.Clear();

                    // Add the flights again ------------------------------------------------------------------------
                    listBox1.Items.Add("Date\t\tFrom\tTo\tFlight");
                    listBox1.Items.Add("\n");
                    using (StreamReader file = new StreamReader("flights.txt"))
                    {
                        string line;
                        while ((line = file.ReadLine()) != null)
                        {
                            string[] words = line.Split(',');
                            flights[nextIndex] = new Flight(words[0], words[1], words[2], words[3], words[4],
                                words[5], words[6], words[7], words[8], words[9]);

                            listBox1.Items.Add(flights[nextIndex].DateTakeOff + "\t" + flights[nextIndex].DepartingFrom +
                                "\t" + flights[nextIndex].LandingIn + "\t" + flights[nextIndex].ID);

                            toolStripComboBox1.Items.Add(flights[nextIndex].DepartingFrom + "-" + flights[nextIndex].LandingIn);
                            toolStripComboBox2.Items.Add(flights[nextIndex].DepartingFrom + "-" + flights[nextIndex].LandingIn);

                            nextIndex++;
                        }
                    }


                    for (int i = 0; i < 1000; i++)
                    {
                        tickets[i] = null;
                    }

                    nextTicketIndex = 0;
                    nextTicketID = 1;

                    comboBox3.Items.Clear();

                    // Add the tickets again ------------------------------------------------------------------------
                    using (StreamReader file = new StreamReader("tickets.txt"))
                    {
                        string line;
                        while ((line = file.ReadLine()) != null)
                        {
                            string[] words = line.Split(',');
                            tickets[nextTicketIndex] = new Ticket(int.Parse(words[0]), words[1], words[2], words[3], words[4], words[5]);

                            comboBox3.Items.Add(tickets[nextTicketIndex].Name);

                            nextTicketIndex++;
                            nextTicketID++;
                        }
                    }

                    MessageBox.Show("Flight Deleted!");

                    textBox3.Text = "";
                    tabControl1.SelectedTab = tabPage4;
                }
                else
                {
                    MessageBox.Show("INVALID ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            else
            {
                MessageBox.Show("INVALID ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }
    }
}

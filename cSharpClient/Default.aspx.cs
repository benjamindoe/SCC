using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FlightServiceReference.FlightBookingWSClient flightWSclient = new FlightServiceReference.FlightBookingWSClient();
            FlightServiceReference.Flight[] flights = flightWSclient.getAvailableSeats();
            SortedSet<String> airlines =  new SortedSet<String>();
            SortedSet<String> destFrom =  new SortedSet<String>();
            SortedSet<String> destTo =    new SortedSet<String>();
            for (int i = 0; i < flights.Length; ++i)
            {
                FlightServiceReference.Flight f = flights[i];
                airlines.Add(f.Airline);
                destFrom.Add(f.OriginCity.City);
                destTo.Add(f.DestinationCity.City);
            }
            List<String> airlineList = new List<String>(airlines);
            airlineList.Insert(0, "No Preference");

            ddAirlines.DataSource = airlineList;
            ddAirlines.DataBind();
            ddFlightsFrom.DataSource = destFrom;
            ddFlightsFrom.DataBind();
            ddFlightsTo.DataSource = destTo;
            ddFlightsTo.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string airline = ddAirlines.SelectedValue != "No Preference" ? ddAirlines.SelectedValue : "";
        Response.Redirect("~/Search-Results.aspx?origin=" + ddFlightsFrom.SelectedValue + "&dest="+ ddFlightsTo.SelectedValue + "&Airline=" + airline + "&Direct=" + chkDirect.Checked);
    }
}
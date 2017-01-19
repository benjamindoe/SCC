using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    FlightServiceReference.FlightBookingWSClient flightWSclient;
    CurrencyConvServiceReference.CurrencyConversionWSClient CurConvWSclient;
    protected void Page_Load(object sender, EventArgs e)
    {
        flightWSclient = new FlightServiceReference.FlightBookingWSClient();
        CurConvWSclient = new CurrencyConvServiceReference.CurrencyConversionWSClient();
        if (!IsPostBack)
        {
            FlightServiceReference.Flight[] flights = flightWSclient.getAvailableSeats();
            List<String> Airlines = new List<String>();
            for (int i = 0; i < flights.Length; ++i)
            {
                FlightServiceReference.Flight f = flights[i];
                Airlines.Add(f.Airline);
            }
            Airlines.Sort();
            Airlines.Insert(0, "No Preference");
            ddAirlines.DataSource = Airlines;
            ddAirlines.DataBind();
        }
        lblCurrency.Text = Session["fareCurr"].ToString();
        ddFlightsDataBind();
    }
    protected void ddFlightsDataBind()
    {
        FlightServiceReference.Flight[] flights = flightWSclient.getAvailableSeats();
        for (int i = 0; i < flights.Length; ++i)
        {
            FlightServiceReference.Flight f = flights[i];
            String fareCurr = Session["fareCurr"].ToString();
            double fareVal = CurrencyExchange(f.Fare.Currency, fareCurr, f.Fare.Value);
            String fareValStr = String.Format("{0:0.00}", Math.Round(fareVal, 2));
            String optionText = f.Date + " " + f.OriginCity + " - " + f.DestinationCity + " " + fareValStr + "(" + fareCurr + ")";
            ddFlights.Items.Insert(i, new ListItem(optionText, f.id.ToString()));
        }
        ddFlights.DataBind();
    }
    protected double CurrencyExchange(String fromCode, String toCode, double val)
    {
        double xRate = CurConvWSclient.GetConversionRate(fromCode, toCode);

        return xRate * val;
    }


}
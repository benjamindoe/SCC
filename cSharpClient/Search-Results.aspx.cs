using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Search_Results : System.Web.UI.Page
{
    CurrencyConvServiceReference.CurrencyConversionWSClient CurConvWSclient;
    FlightServiceReference.FlightBookingWSClient flightWSclient;
    protected void Page_Load(object sender, EventArgs e)
    {
        CurConvWSclient = new CurrencyConvServiceReference.CurrencyConversionWSClient();
        flightWSclient = new FlightServiceReference.FlightBookingWSClient();
        if(!IsPostBack)
        {
            // Make search results view
        }
    }
}
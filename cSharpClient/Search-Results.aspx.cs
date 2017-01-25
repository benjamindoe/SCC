using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Search_Results : System.Web.UI.Page
{
    FlightServiceReference.FlightBookingWSClient flightWSclient;
    protected void Page_Load(object sender, EventArgs e)
    {
        flightWSclient = new FlightServiceReference.FlightBookingWSClient();
        string origin = Request.QueryString["origin"] != null ? Request.QueryString["origin"] : "";
        string destination = Request.QueryString["dest"] != null ? Request.QueryString["dest"] : "";
        string airline = Request.QueryString["airline"] != null ? Request.QueryString["airline"] : "";
        flightWSclient = new FlightServiceReference.FlightBookingWSClient();
        FlightServiceReference.Flight[] flights = flightWSclient.searchFlight(origin, destination, airline, (Request.QueryString["Direct"] == "True"));
        DropDownList mpDd = (DropDownList)Master.FindControl("ddCurrency");
        string fareCurr = String.IsNullOrWhiteSpace(mpDd.SelectedValue) ? "GBP" : mpDd.SelectedValue;

        if (flights != null)
        {
            for (int i = 0; i < flights.Length; ++i)
            {
                FlightServiceReference.Flight f = flights[i];
                HtmlGenericControl liFlight = new HtmlGenericControl("li");
                liFlight.Attributes.Add("class", "flights__flight z-depth-3");

                List<HtmlGenericControl> spans = new List<HtmlGenericControl>();
                HtmlImage airlineLogo = new HtmlImage();
                airlineLogo.Src = "//logo.clearbit.com/" + f.Website + "?size=85";
                airlineLogo.Attributes.Add("class", "flight__logo");
                liFlight.Controls.Add(airlineLogo);
                string flightJourneyStr = "<span class=\"bold start_city\" data-country=\""+ f.OriginCity.Country +
                    "\" data-airport=\"" + f.OriginCity.Airport +  "\">" + f.OriginCity.Airport + ", "+ f.OriginCity.City +
                    "</span> to <span class=\"bold end_city\">" + f.DestinationCity.Airport + ", " + f.DestinationCity.City + "</span>";
                spans.Add(spanMaker("flight__journey", flightJourneyStr));
                spans.Add(spanMaker("flight__date", f.Date.ToLongDateString()));
                spans.Add(spanMaker("flight__airline", f.Airline));
                HtmlGenericControl seatSpan = spanMaker("flight__seat-no", "Available Seats: " + f.NoOfSeats);
                seatSpan.ID = "spnSeat";
                spans.Add(seatSpan);
                string directTxt = f.NoOfConnections > 0 ? f.NoOfConnections + " Connections" : "Direct Flight";
                spans.Add(spanMaker("flight__direct", directTxt));
                double fareVal = CurrencyExchange(f.Fare.Currency, fareCurr, f.Fare.Value);
                string fareValStr = string.Format("{0:0.00}", Math.Round(fareVal, 2));
                fareValStr += "(" + fareCurr + ")";
                spans.Add(spanMaker("flight__price", fareValStr));
                foreach (var span in spans)
                    liFlight.Controls.Add(span);

                if(f.DestinationCity.Country == f.OriginCity.Country) //same country
                {
                    HtmlAnchor hiddenDropdownBtn = new HtmlAnchor();
                    hiddenDropdownBtn.HRef = "#";
                    hiddenDropdownBtn.InnerText = "Show Driving Directions";
                    hiddenDropdownBtn.Attributes.Add("data-beloworigin", "true");
                    hiddenDropdownBtn.Attributes.Add("data-constrainwidth", "false");
                    hiddenDropdownBtn.Attributes.Add("class", "dropdown-button");
                    hiddenDropdownBtn.Attributes.Add("data-activates", "MainContent_directionDropdown");
                    HtmlGenericControl hiddenDropdown = new HtmlGenericControl("ul");
                    hiddenDropdown.ID = "directionDropdown";
                    hiddenDropdown.Attributes.Add("class", "dropdown-content");
                    List<string> directions = RestClient.getDirections(f.OriginCity.Airport, f.DestinationCity.Airport);
                    for (int j = 0; j < directions.Count; ++j)
                    {
                        HtmlGenericControl liDirection = new HtmlGenericControl("li");
                        liDirection.InnerHtml = directions[j];
                        hiddenDropdown.Controls.Add(liDirection);
                    }
                    liFlight.Controls.Add(hiddenDropdownBtn);
                    liFlight.Controls.Add(hiddenDropdown);
                }


                HtmlButton btn = new HtmlButton();
                btn.ID = "btnBuyFlight";
                btn.Attributes.Add("class", "btn flight__btn waves-effect waves-light light-green");
                btn.Attributes.Add("data-flightId", f.id.ToString());
                btn.ServerClick += new System.EventHandler(this.btnBuy_Click);
                btn.InnerHtml = "Buy <i class=\"material-icons right\">shopping_cart</i>";
                btn.DataBind();
                liFlight.Controls.Add(btn);
                lstFlights.Controls.Add(liFlight);
            }
        }
        else
        {
            HtmlGenericControl liFlight = new HtmlGenericControl("li");
            liFlight.Attributes.Add("class", "flights__flight z-depth-3 valign-wrapper");
            HtmlGenericControl valignWrap = new HtmlGenericControl("div");
            liFlight.Controls.Add(spanMaker("valign center-align", "No Flights Found."));
            lstFlights.Controls.Add(liFlight);
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {

    }
    protected HtmlGenericControl spanMaker(string className, string innerText)
    {
        HtmlGenericControl span = new HtmlGenericControl("span");
        span.Attributes.Add("class", className);
        span.InnerHtml = innerText;
        return span;
    }
    protected double CurrencyExchange(String fromCode, String toCode, double val)
    {
        Dictionary<string, double> rates = RestClient.getExRate(fromCode, toCode).Rates;
        double xRate = rates.ContainsKey(toCode) ? rates[toCode] : 1;

        return xRate * val;
    }
    protected void btnBuy_Click(object sender, EventArgs e)
    {
        HtmlButton btn = (HtmlButton)sender;
        int flightId = 0;
        if(Int32.TryParse(btn.Attributes["data-flightId"], out flightId))
        {
            int seatNo = flightWSclient.bookFlight(flightId);
            HtmlGenericControl spnSeat = (HtmlGenericControl)btn.Parent.FindControl("spnSeat");
            spnSeat.InnerText = "Available Seats: " + seatNo;
        }
    }
}
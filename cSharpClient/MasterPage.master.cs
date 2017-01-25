using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Exchange ex = RestClient.getAllExRates();
            ddCurrency.Items.Add(ex.Base);
            foreach(var val in ex.Rates)
                ddCurrency.Items.Add(val.Key);

            ddCurrency.DataBind();
            if (Session["fareCurr"] == null)
            {
                Session.Add("fareCurr", "GBP");
                Session["exRate"] = 1;
            }
            ddCurrency.SelectedValue = (string)Session["fareCurr"];
        }
    }
    protected void ddCurrency_Change(object sender, EventArgs e)
    {
        string key = ddCurrency.SelectedValue;
        Dictionary<string, double> rates = RestClient.getExRate(key).Rates;
        Session["fareCurr"] = key;
        Session["exRate"] = rates.ContainsKey(key) ? rates[key] : 1;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    CurrencyConvServiceReference.CurrencyConversionWSClient CurConvWSclient;
    protected void Page_Load(object sender, EventArgs e)
    {
        CurConvWSclient = new CurrencyConvServiceReference.CurrencyConversionWSClient();
        if (!IsPostBack)
        {
            String[] currencyCodes = CurConvWSclient.GetCurrencyCodes();
            ddCurrency.DataSource = currencyCodes;
            ddCurrency.DataBind();
        }
        Session["fareCurr"] = ddCurrency.SelectedValue.Substring(0, 3);
    }
    protected void ddCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Session["fareCurr"] = ddCurrency.SelectedValue.Substring(0, 3);
    }
}

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <div class="body default-aspx">
        <div class="input-field inline">
            <i class="material-icons prefix">location_on</i>
            <asp:DropDownList ID="ddFlightsFrom" runat="server"></asp:DropDownList>
            <label>From</label>
        </div>
        <div class="input-field inline">
            <i class="material-icons prefix">location_on</i>
            <asp:DropDownList ID="ddFlightsTo" runat="server"></asp:DropDownList>
            <label>To</label>
        </div>
        <div class="optional">
            <asp:CheckBox ID="chkDirect" runat="server" Text="Direct Flights Only"/>
            <div class="input-field inline">
                <asp:DropDownList ID="ddAirlines" runat="server"></asp:DropDownList>
            <label>Airline Preference</label>
            </div>
        </div>
    
        <asp:Button ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_Click" Text="Search" />
        <asp:Button ID="btnAllFlights" runat="server" CssClass="btn" OnClick="btnSearch_Click" Text="All Flights" />
    </div>

</asp:Content>


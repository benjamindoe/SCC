<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <asp:Label ID="lblCurrency" runat="server" Text="Label">Select Preferred Currency</asp:Label>
    <asp:DropDownList ID="ddFlights" runat="server"></asp:DropDownList>
    <asp:DropDownList ID="ddAirlines" runat="server"></asp:DropDownList>


</asp:Content>


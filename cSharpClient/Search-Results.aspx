<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Search-Results.aspx.cs" Inherits="Search_Results" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="js/searchResults.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <div class="body search-results">
        <ul id="lstFlights" class="flights" runat="server">

        </ul>
    </div>
</asp:Content>


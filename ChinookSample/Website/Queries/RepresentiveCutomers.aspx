<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RepresentiveCutomers.aspx.cs" Inherits="Queries_RepresentiveCutomers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:DropDownList ID="EmployeeList" runat="server" DataSourceID="EmployeeListODS"></asp:DropDownList>
    <asp:ObjectDataSource ID="EmployeeListODS" runat="server"></asp:ObjectDataSource>

    <br /><br /><br />

    <asp:GridView ID="RepCustomer" runat="server" DataSourceID="RepCustomerODS" AllowPaging="True"></asp:GridView>
  
    <asp:ObjectDataSource ID="RepCustomerODS" runat="server"></asp:ObjectDataSource>

</asp:Content>


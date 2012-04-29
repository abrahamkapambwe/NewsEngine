<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="TagsPage.aspx.cs" Inherits="NewsAggregate.TagsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label runat="server" ID="lblRsults"></asp:Label>
    <div>
        Tag Name<asp:TextBox runat="server" ID="txtTagName"></asp:TextBox></div>
    <br />
    <br />
    <asp:DropDownList ID="ddlCountry" runat="server">
        <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
        <asp:ListItem Text="Kenya" Value="Kenya"></asp:ListItem>
        <asp:ListItem Text="Malawi" Value="Malawi"></asp:ListItem>
        <asp:ListItem Text="Tanzania" Value="Tanzania"></asp:ListItem>
        <asp:ListItem Text="Uganda" Value="Uganda"></asp:ListItem>
        <asp:ListItem Text="Zambia" Value="Zambia"></asp:ListItem>
        <asp:ListItem Text="Zimbabwe" Value="Zimbabwe"></asp:ListItem>
        <asp:ListItem Text="South Africa" Value="South Africa"></asp:ListItem>
    </asp:DropDownList>
    <br />
    <asp:Button runat="server" ID="btnSubmit" Text="Save" OnClick="Save_Click" />
</asp:Content>

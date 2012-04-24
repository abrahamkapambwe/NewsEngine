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
    <asp:Button runat="server" ID="btnSubmit" Text="Save" OnClick="Save_Click"/>
</asp:Content>

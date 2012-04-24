<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EnterNews.aspx.cs" Inherits="NewsAggregate.EnterNews" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="HTMLEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <legend>NewsComponents</legend>
    <asp:Label runat="server" ID="lblResult" ForeColor="Red" Visible="False" Text="Save successfully"></asp:Label>
    <table>
        <tr>
            <td style="margin-left: 10px">
                Source
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtSource" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="margin-left: 10px">
                News Item
            </td>
            <td>
                <HTMLEditor:Editor runat="server" ID="txtNewsItem" Height="400px" Width="80%" AutoFocus="true" />
            </td>
        </tr>
        <tr>
            <td style="margin-left: 10px">
                Summary Content
            </td>
            <td>
                <HTMLEditor:Editor runat="server" ID="txtSummaryContent" Height="100px" Width="80%"
                    AutoFocus="true" />
            </td>
        </tr>
        <tr>
            <td style="margin-left: 10px">
                News Headline
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtNewsHeadline" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="margin-left: 10px">
                Image label
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtImageLabel" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="margin-left: 10px">
                News PhotoUrl
            </td>
            <td>
                <asp:FileUpload ID="FileUpload2" runat="server" /><br />
                <br />
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="margin-left: 10px">
                Publish
            </td>
            <td>
                <asp:RadioButton runat="server" ID="rbnYes" Text="Yes" />
                <asp:RadioButton runat="server" ID="rbnNo" Text="No" />
            </td>
        </tr>
        <tr>
            <td style="margin-left: 10px">
                <%--   Publish--%>
            </td>
            <td>
                <%--<asp:RadioButton runat="server" ID="RadioButton1" Text="Yes" />
                <asp:RadioButton runat="server" ID="RadioButton2" Text="No" />--%>
            </td>
        </tr>
        <tr>
            <td style="margin-left: 10px">
                Category
            </td>
            <td>
                <asp:DropDownList ID="ddlCategory" runat="server">
                    <asp:ListItem Text="Politics" Value="Politics"></asp:ListItem>
                    <asp:ListItem Text="Business" Value="Business"></asp:ListItem>
                    <asp:ListItem Text="Technology" Value="Technology"></asp:ListItem>
                    <asp:ListItem Text="Entertainment" Value="Entertainment"></asp:ListItem>
                    <asp:ListItem Text="Sport" Value="Sport"></asp:ListItem>
                    <asp:ListItem Text="LifeStyle" Value="LifeStyle"></asp:ListItem>
                    <asp:ListItem Text="World News" Value="WorldNews"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="margin-left: 10px">
                Country
            </td>
            <td>
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
            </td>
        </tr>
        <tr>
            <td>
                Tags
            </td>
            <td>
                <asp:ListBox ID="LstTags" runat="server" SelectionMode="Multiple">
                    
                </asp:ListBox>
            </td>
        </tr>
    </table>
    <asp:Label runat="server" ForeColor="Red" ID="lblResults2" Visible="False" Text="Save successfully"></asp:Label>
    <br />
    <br />
    <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="Submit_Click" /><br />
    <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="Clear_Click" />
</asp:Content>

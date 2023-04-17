<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="CustomerPanel.aspx.cs" Inherits="Car_Rental.Pages.PanelKlienta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<link href="../CSS/CustomerPanel1.css" rel="stylesheet" />
	<div class="panel">
		<form class="form" runat="server">
			<div style="display: inline-block;">
				<a>Imię:</a><br />
				<asp:TextBox CssClass="input" ID="nameTxt" runat="server" placeholder="Imię" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Nazwisko:</a><br />
				<asp:TextBox CssClass="input" ID="lastNameTxt" runat="server" placeholder="Nazwisko" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Miasto:</a><br />
				<asp:TextBox CssClass="input" ID="cityTxt" runat="server" placeholder="Miasto" required="required" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Kod pocztowy</a><br />
				<asp:TextBox CssClass="input" ID="postalCodeTxt" runat="server" placeholder="Adres" required="required" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Ulica:</a><br />
				<asp:TextBox CssClass="input" ID="streetTxt" runat="server" placeholder="Ulica" required="required" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
			</div>
			<div style="display: inline-block;">
				<a>Nr. budynku:</a><br />
				<asp:TextBox CssClass="input" ID="houseNumberTxt" runat="server" placeholder="Adres" required="required" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Twój login:</a><br />
				<asp:TextBox CssClass="input" ID="loginTxt" runat="server" placeholder="Login" required="required" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Twój email:</a><br />
				<asp:TextBox CssClass="input" ID="emailTxt" runat="server" placeholder="E-mail" required="required" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Telefon</a><br />
				<asp:TextBox CssClass="input" ID="phoneNumberTxt" runat="server" placeholder="Telefon" required="required" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Płeć</a><br />
				<asp:TextBox CssClass="input" ID="genderTxt" runat="server" placeholder="Płeć" required="required" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
			</div>
			<div class="helper">
				<a href="EditAccountDetails.aspx">Edytuj Dane</a><br />
				<a runat="server" onserverclick="deleteAccount">Usuń Konto</a><br />
				<a id="codeText" runat="server" visible="false">Wpisz swoje nazwisko, aby potwierdzić</a><br />
				<asp:TextBox Visible="false" CssClass="input" ID="lastNameDeleteAccTxt" runat="server" placeholder="Nazwisko" required="required" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<asp:Button ID="deleteBtn" Visible="false" class="button" runat="server" Text="Usuń" OnClick="deleteAccountButton" />
			</div>
		</form>
	</div>
</asp:Content>

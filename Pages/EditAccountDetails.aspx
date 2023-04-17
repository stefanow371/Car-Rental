<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditAccountDetails.aspx.cs" Inherits="Car_Rental.Pages.EditAccountDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<link href="../CSS/AccountDetails.css" rel="stylesheet" />
	<div class="panel">
		<form class="form" runat="server">
				<a>Miasto:</a><br />
				<asp:TextBox CssClass="input" ID="cityTxt" runat="server" placeholder="Miasto"  Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Kod pocztowy</a><br />
				<asp:TextBox CssClass="input" ID="postalCodeTxt" runat="server" placeholder="Adres"  Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Ulica:</a><br />
				<asp:TextBox CssClass="input" ID="streetTxt" runat="server" placeholder="Ulica"  Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Nr. budynku:</a><br />
				<asp:TextBox CssClass="input" ID="houseNumberTxt" runat="server" placeholder="Adres"  Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Telefon</a><br />
				<asp:TextBox CssClass="input" ID="phoneNumberTxt" runat="server" placeholder="Telefon"  Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
			<div class="helper">
				<br /><a href="AdminConfirmationPage.aspx">Autoryzuj jako administrator</a><br />
				<br /><a href="CustomerPanel.aspx">Cofnij</a><br />
			</div>
			<asp:Button ID="registerBtn" class="button" runat="server" Text="Zapisz" OnClick="UpdateButton_OnClick" />
		</form>
	</div>
</asp:Content>

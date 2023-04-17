<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdminConfirmationPage.aspx.cs" Inherits="Car_Rental.Pages.AdminConfirmationPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<link href="../CSS/AdminConfirmation.css" rel="stylesheet" />
		<div class="panel">
		<form class="form" runat="server">	
				<a>Wpisz kod:</a><br />
			<asp:TextBox CssClass="input" ID="AdminSpecialCode" runat="server" placeholder="Kod"  Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
			<br /><a href="EditAccountDetails.aspx">Cofnij</a><br />
			<asp:Button ID="confirmAsAdmin" class="button" runat="server" Text="Zatwierdź" OnClick="confirmAsAdmin_OnClick"/>
		</form>
	</div>
</asp:Content>

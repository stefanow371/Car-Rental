<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProductView.aspx.cs" Inherits="Car_Rental.Pages.ProductView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<link href="../CSS/ProductView.css" rel="stylesheet" />
	<div class="panel">
		<form class="form" runat="server">
			<asp:Image ID="Image1" Height="300" Width="300" runat="server" /><br />
			<div style="display: inline-block;">
				<a>Marka:</a><br />
				<asp:TextBox CssClass="input" ID="brandTxt" runat="server" placeholder="Imię" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Model:</a><br />
				<asp:TextBox CssClass="input" ID="modelTxt" runat="server" placeholder="Nazwisko" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Rok Produkcji:</a><br />
				<asp:TextBox CssClass="input" ID="productionYearTxt" runat="server" placeholder="Miasto" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Pojemność silnika</a><br />
				<asp:TextBox CssClass="input" ID="engineCapacityTxt" runat="server" placeholder="Adres" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Napęd:</a><br />
				<asp:TextBox CssClass="input" ID="driveTypeTxt" runat="server" placeholder="Ulica" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Paliwo:</a><br />
				<asp:TextBox CssClass="input" ID="fuelTypeTxt" runat="server" placeholder="Adres" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
			</div>
			<div style="display: inline-block;">
				<a>Liczba drzwi:</a><br />
				<asp:TextBox CssClass="input" ID="doorsAmountTxt" runat="server" placeholder="Login" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Liczba siedzeń:</a><br />
				<asp:TextBox CssClass="input" ID="seatsAmountTxt" runat="server" placeholder="E-mail" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Klimatyzacja</a><br />
				<asp:TextBox CssClass="input" ID="isACTxt" runat="server" placeholder="Telefon" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Skrzynia biegów</a><br />
				<asp:TextBox CssClass="input" ID="gearTypeTxt" runat="server" placeholder="Płeć" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Cena wynajmu za/dzień: </a>
				<br />
				<asp:TextBox CssClass="input" ID="carPriceTxt" runat="server" placeholder="Telefon" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
				<a>Opis: </a>
				<br />
				<asp:TextBox CssClass="input" ID="carDescriptionTxt" runat="server" placeholder="Płeć" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
			</div>
			<div style="display: inline-block;"></div>
			<br />
			<div style="display: inline-block;">
				<a id="startDateText" runat="server">Początek wynajmu</a>
				<input type="date" id="startDate" runat="server" name="Początek wynajmu">
				<a id="endDateText" runat="server">Koniec wynajmu</a>
				<input type="date" id="endDate" runat="server" name="Koniec wynajmu">
			</div>
			<br />
			<a id="cardNumber" runat="server">Numer Karty</a><br />
			<asp:TextBox CssClass="input" ID="cardNumberTxt" runat="server" placeholder="xxxxxxxxxxxxxxxx" MaxLength="16" Width="256px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
			<a id="cardExpDateText" runat="server">Ważność karty</a><br />
			<input type="date" id="cardExpireDate" runat="server" name="Koniec wynajmu"><br />
			<a id="CVCText" runat="server">CVC</a><br />
			<asp:TextBox CssClass="input" ID="cardCVCTxt" runat="server" MaxLength="3" placeholder="CVC" Width="50px" Font-Bold="true" Font-Size="Large"></asp:TextBox><br />
			<a href="Proposal.aspx">Cofnij</a><br />
			<asp:Button ID="Rent" Text="Wypożycz" runat="server" OnClick="Rent_Click" />
		</form>
	</div>
</asp:Content>

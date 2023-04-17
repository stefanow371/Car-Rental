<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdminAddProduct.aspx.cs" Inherits="Car_Rental.Pages.AdminAddProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<link href="../CSS/AdminAddProduct.css" rel="stylesheet" />
	<div class="register">
		<form class="form" runat="server">
			<a>Marka:</a><br />
			<asp:TextBox CssClass="input" ID="brandTxt" runat="server" placeholder="Marka" required="required" Width="100px"></asp:TextBox><br />
			<a>Model:</a><br />
			<asp:TextBox CssClass="input" ID="modelTxt" runat="server" placeholder="Model" required="required" Width="100px"></asp:TextBox><br />
			<a>Pojemność silnika</a><br />
			<asp:TextBox CssClass="input" ID="capacityTxt" runat="server" placeholder="Adres" required="required" Width="100px"></asp:TextBox><br />
			<a>Rok Produkcji:</a>
			<asp:DropDownList ID="prodYear" runat="server" required="true">
			</asp:DropDownList><br />
			<a>Rodzaj napędu:</a>
			<asp:DropDownList ID="drive" runat="server" required="true">
				<asp:ListItem Text="4x4" Value="4x4"></asp:ListItem>
				<asp:ListItem Text="Przód" Value="Przód"></asp:ListItem>
				<asp:ListItem Text="Tył" Value="Tył"></asp:ListItem>
			</asp:DropDownList><br />
			<div class="pick">
				<a>Źródło energii:</a><br />
				<asp:RadioButton ID="Diesel" Text="Diesel" runat="server" GroupName="petrol" />
				<asp:RadioButton ID="Petrol" Text="Benzyna" runat="server" GroupName="petrol" />
				<asp:RadioButton ID="Battery" Text="Bateria" runat="server" GroupName="petrol" />
			</div>
			<div class="pick">
				<a>Liczba drzwi</a><br />
				<asp:RadioButton ID="doorsAmount3" Text="3" runat="server" GroupName="doors" />
				<asp:RadioButton ID="doorsAmount5" Text="5" runat="server" GroupName="doors" />
			</div>
			<a>Liczba miejsc:</a>
			<asp:DropDownList ID="seats" runat="server" required="true">
				<asp:ListItem Text="2" Value="2"></asp:ListItem>
				<asp:ListItem Text="3" Value="3"></asp:ListItem>
				<asp:ListItem Text="4" Value="4"></asp:ListItem>
				<asp:ListItem Text="5" Value="5"></asp:ListItem>
			</asp:DropDownList>
			<div class="pick">
				<a>Klimatyzacja:</a><br />
				<asp:RadioButton ID="included" Text="Tak" runat="server" GroupName="AC" />
				<asp:RadioButton ID="notIncluded" Text="Nie" runat="server" GroupName="AC" />
			</div>
			<div class="pick">
				<a>Skrzynia biegów:</a><br />
				<asp:RadioButton ID="manual" Text="Manualna" runat="server" GroupName="gearType" />
				<asp:RadioButton ID="automatic" Text="Automatyczna" runat="server" GroupName="gearType" />
			</div>
			<a>Cena za dzień:</a><br />
			<asp:TextBox CssClass="input" ID="Price" runat="server" placeholder="Cena" required="required" Width="100px"></asp:TextBox><br />
			<a>Opis:</a><br />
			<asp:TextBox CssClass="input" ID="description" runat="server" placeholder="Opis" required="required" Width="100px" TextMode="multiline" Columns="5"></asp:TextBox><br />
			<asp:FileUpload ID="FileUpload" accept="image/*" AllowMultiple="true" runat="server" required="true" /><br />

			<asp:Button ID="registerBtn" class="button" runat="server" Text="Dodaj" OnClick="insert_Btn" />
		</form>
	</div>
</asp:Content>

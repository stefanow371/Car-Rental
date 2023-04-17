<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Car_Rental.Pages.Rejestracja" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<link href="../CSS/Register.css" rel="stylesheet" />
	<div class="register">
		<form class="form" runat="server">
			<div style="display: inline-block;">
				<a>Imię:</a><br />
				<asp:TextBox CssClass="input" ID="nameTxt" runat="server" placeholder="Imię" required="required"></asp:TextBox><br />
				<a>Nazwisko:</a><br />
				<asp:TextBox CssClass="input" ID="lastNameTxt" runat="server" placeholder="Nazwisko" required="required"></asp:TextBox><br />
				<a>Miasto:</a><br />
				<asp:TextBox CssClass="input" ID="cityTxt" runat="server" placeholder="Miasto" required="required"></asp:TextBox><br />
				<a>Kod pocztowy</a><br />
				<asp:TextBox CssClass="input" ID="postalCodeTxt" runat="server" placeholder="Adres" required="required"></asp:TextBox><br />
				<a>Ulica:</a><br />
				<asp:TextBox CssClass="input" ID="streetTxt" runat="server" placeholder="Ulica" required="required"></asp:TextBox><br />
			</div>
			<div style="display: inline-block;">
				<a>Nr. budynku:</a><br />
				<asp:TextBox CssClass="input" ID="houseNumberTxt" runat="server" placeholder="Adres" required="required"></asp:TextBox><br />
				<a>Twój login:</a><br />
				<asp:TextBox CssClass="input" ID="loginTxt" runat="server" placeholder="Login" required="required"></asp:TextBox><br />
				<a>Hasło:</a><br />
				<asp:TextBox CssClass="input" ID="passwordTxt" runat="server" placeholder="Hasło" TextMode="Password" required="required"></asp:TextBox><br />
				<a>E-mail:</a><br />
				<asp:TextBox CssClass="input" ID="emailTxt" runat="server" placeholder="E-mail" required="required"></asp:TextBox><br />
				<a>Telefon</a><br />
				<asp:TextBox CssClass="input" ID="phoneNumberTxt" runat="server" placeholder="Telefon" required="required"></asp:TextBox><br />
			</div>
			<div class="pick">
				<a>Płeć:</a><br />
				<asp:RadioButton ID="Male" Text="Mężczyzna" runat="server" GroupName="gender" />
				<asp:RadioButton ID="Female" Text="Kobieta" runat="server" GroupName="gender" />
			</div>
			<div class="helper">
				<a href="Account.aspx">Masz już konto? Zaloguj się!</a><br />
			</div>
			<asp:CheckBox ID="CheckBox" runat="server" AutoPostBack="True" Text="Konto Administratora" OnCheckedChanged="sendAdminRequest_OnClick" />
			<br />
			<a id="requestText" runat="server" visible="false">Powód :</a><br />
			<asp:TextBox ID="requestDescription" Visible="false" CssClass="input" runat="server" placeholder="Powód" required="required"></asp:TextBox><br />
			<asp:Button ID="registerBtn" class="button" runat="server" Text="Zarejestruj" OnClick="register_OnClick" />
		</form>
		<div id="errorMessageDiv" runat="server"></div>
	</div>
</asp:Content>

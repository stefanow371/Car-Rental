<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="Car_Rental.Pages.SignIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<link href="../CSS/SignIn.css" rel="stylesheet" />
	<div class="signIn">
		<form class="form" runat="server">
			<a>Login:</a><br />
			<asp:TextBox CssClass="input" ID="loginTxt" runat="server" placeholder="Login" required="required"></asp:TextBox><br />
			<a>Hasło:</a><br />
			<link href="../CSS/Contact.css" rel="stylesheet" />
			<asp:TextBox CssClass="input" ID="passwordTxt" runat="server" placeholder="Hasło" TextMode="Password" required="required"></asp:TextBox><br />
			<div class="links">
				<a href="Register.aspx">Załóż konto</a><br />
				<a href="RemindPassword.aspx">Zapomniałem hasła</a><br />
			</div>
			<br />
			<asp:Button ID="loginBtn" class="button" runat="server" Text="Zaloguj" OnClick="Login_Click" />
		</form>
	</div>
</asp:Content>

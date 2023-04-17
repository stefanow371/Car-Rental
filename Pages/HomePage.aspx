<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="Car_Rental.Pages.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<link href="../CSS/HomePage.css?Version=1" rel="stylesheet" />
	<div class="welcome">
		<h1>Witaj na CARRENTL</h1>
		<h2>W czym możemy Ci pomóc?</h2>
	</div>
	<div class="menu">
		<form class="form">
			<img class="pic" src="../Pages/Pictures/user.png" /><br />
			<h3>Twoje konto</h3>
			<a>Czeka na ciebie wiele możliwości wynajmu<br />
				Wystarczy, że założysz konto,<br />
				żeby poznać całą ofertę!<br />
			</a>
			<br />
			<a href="Register.aspx">Rejestracja</a>
		</form>
		<form class="form">
			<img class="pic" src="../Pages/Pictures/sedan.png" /><br />
			<h3>Nasza Oferta</h3>
			<a>Potrzebujesz wynająć auto na chwilę?<br />
				A może szukasz czegoś na dłuższą trasę?<br />
				Na pewno znajdziesz coś dla siebie!<br />
			</a>
			<br />
			<a href="Proposal.aspx">Oferta</a>
		</form>
		<form class="form">
			<img class="pic" src="../Pages/Pictures/company.png" /><br />
			<h3>O nas</h3>
			<a>Masz pytania?<br />
				Dowiedz się, jak działa CARRENTL<br />
				Odwiedź zakładkę O nas<br />
			</a>
			<br />
			<a href="AboutUs.aspx">O nas</a>
		</form>
	</div>
</asp:Content>

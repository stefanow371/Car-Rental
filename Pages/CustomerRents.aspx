<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="CustomerRents.aspx.cs" Inherits="Car_Rental.Pages.CustomerRents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<link href="../CSS/CustomerRents.css" rel="stylesheet" />
	<div class="proposal" >
	<form class="form" runat="server">
		<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="GetRentsByAccID" CssClass="proposalView" HeaderStyle-CssClass="header" RowStyle-CssClass="rows">
			<Columns>
				<asp:TemplateField>
						<ItemTemplate>
							<asp:Image ID="Image1" Height = "150" Width = "150" runat="server" ImageUrl ='<%# "Pages\\Pictures\\"+Eval("image_path").ToString().Trim() %>' />
						</ItemTemplate>
					</asp:TemplateField>
				<asp:BoundField DataField="car_brand" HeaderText="Marka" SortExpression="rent_start_date"></asp:BoundField>
				<asp:BoundField DataField="car_model" HeaderText="Model" SortExpression="rent_start_date"></asp:BoundField>
				<asp:BoundField DataField="rent_start_date" HeaderText="Data Wypożyczenia" SortExpression="rent_start_date"></asp:BoundField>
				<asp:BoundField DataField="rent_end_date" HeaderText="Koniec Wypożyczenia" SortExpression="rent_end_date"></asp:BoundField>
			</Columns>
		</asp:GridView>
		<asp:SqlDataSource runat="server" ID="GetRentsByAccID" ConnectionString="<%$ ConnectionStrings:connection %>"
			SelectCommand="SELECT [rent_acc_Id], [rent_car_Id], [rent_card_Id], [rent_start_date], [rent_end_date], [carImages].[image_path], [carDetails].[car_brand], [carDetails].[car_model] FROM [Rent]
			JOIN [CarDetails] ON [Rent].[rent_car_id] = [CarDetails].[car_Id]
			JOIN [carImages] ON [CarDetails].[car_id] = [carImages].[car_table_Id]
			JOIN [Account] ON [Rent].[rent_acc_Id] = [Account].[acc_Id] WHERE ([acc_login] = @acc_login)">

			<SelectParameters>
				<asp:SessionParameter Name="acc_login" SessionField="acc_login" Type="string" />
			</SelectParameters>
		</asp:SqlDataSource>
	</form>
	</div>
</asp:Content>

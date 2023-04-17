<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="Proposal.aspx.cs" Inherits="Car_Rental.Pages.Oferta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<link href="../CSS/Proposal.css" rel="stylesheet" />
	<div class="proposal">
		<form runat="server" class="form">
			<asp:GridView ID="GridViewForProposal" runat="server" DataKeyNames="car_Id" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CssClass="proposalView" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" BorderStyle="None" OnRowCommand="GridViewForProposal_RowCommand" OnRowDataBound="GridViewForProposal_RowDataBound">
				<Columns>
					<asp:TemplateField>
						<ItemTemplate>
							<asp:Image ID="Image1" Height="150" Width="150" runat="server" ImageUrl='<%# "Pages\\Pictures\\"+Eval("image_path").ToString().Trim() %>' />
						</ItemTemplate>
					</asp:TemplateField>
					<asp:BoundField DataField="car_Id" HeaderText="carid" SortExpression="car_Id" Visible="false"></asp:BoundField>
					<asp:BoundField DataField="car_brand" HeaderText="Marka" SortExpression="car_brand"></asp:BoundField>
					<asp:BoundField DataField="car_model" HeaderText="Model" SortExpression="car_model"></asp:BoundField>
					<asp:BoundField DataField="car_production_year" HeaderText="Rok produkcji" SortExpression="car_production_year"></asp:BoundField>
					<asp:BoundField DataField="car_engine_capacity" HeaderText="Pojemność silnika (l)" SortExpression="car_engine_capacity"></asp:BoundField>
					<asp:TemplateField>
						<ItemTemplate>
							<asp:Button ID="Proposal" Text="Zobacz Ofertę" runat="server" CommandName="showDetails" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
							<asp:Button ID="deleteProposal" Visible ="false"  Text="Usuń" runat="server" CommandName="deleteProposal" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
			<asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:connection %>"
				SelectCommand="SELECT [car_Id], [car_brand], [car_model], [car_production_year], [car_engine_capacity],
				[car_drive_type], [car_fuel_type], [car_doors_amount], [car_seats_amount], [car_is_air_conditioning], [car_gear_type], [carImages].[image_path] 
                FROM [CarDetails] 
                JOIN [carImages] ON [CarDetails].[car_id] = [carImages].[car_table_Id]"></asp:SqlDataSource>
		</form>
	</div>
</asp:Content>

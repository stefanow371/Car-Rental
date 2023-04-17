using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Data.Common;

namespace Car_Rental.Pages {
	public partial class Oferta : System.Web.UI.Page {
		string CONNECTION = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
		protected void Page_Load(object sender, EventArgs e) {


		}
		protected void GridViewForProposal_RowCommand(object sender, GridViewCommandEventArgs e) {
			if (e.CommandName == "showDetails") {
				int rowIndex = Convert.ToInt32(e.CommandArgument);
				int dataKey = Convert.ToInt32(GridViewForProposal.DataKeys[rowIndex].Value);
				Session["car_Id"] = dataKey;
				Response.Redirect("ProductView.aspx");
			}

			string sessionLogin = Session["acc_login"] as string;

			try {
				if (!string.IsNullOrEmpty(sessionLogin)) {

					SqlConnection sqlCon = new SqlConnection(CONNECTION);

					if (sqlCon.State == ConnectionState.Closed) {
						sqlCon.Open();
					}

					SqlCommand getIsAdmin = new SqlCommand("SELECT acc_is_Admin FROM Account WHERE acc_login='" + sessionLogin + "'", sqlCon);

					SqlDataAdapter isAdminDA = new SqlDataAdapter(getIsAdmin);

					DataTable isAdminDT = new DataTable();
					isAdminDA.Fill(isAdminDT);

					string isAdmin = isAdminDT.Rows[0]["acc_is_Admin"].ToString().Trim();

					bool isAdminBoolean = bool.Parse(isAdmin);

					if (e.CommandName == "deleteProposal") {

						int rowIndex = Convert.ToInt32(e.CommandArgument);
						int dataKey = Convert.ToInt32(GridViewForProposal.DataKeys[rowIndex].Value);

						if (sqlCon.State == ConnectionState.Closed) {
							sqlCon.Open();
						}

						if (isAdminBoolean == true) {

							SqlCommand deleteAccount = new SqlCommand("DELETE FROM carDetails WHERE car_Id='" + dataKey + "'", sqlCon);

							deleteAccount.ExecuteNonQuery();
							sqlCon.Close();
							Response.Redirect("Proposal.aspx");

						}
					}

				}
				else {
					Response.Write("<script>alert('Nie posiadasz uprawnień administratora');</script>");
					return;
				}
			}
			catch (Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		protected void GridViewForProposal_RowDataBound(object sender, GridViewRowEventArgs e) {
			string sessionLogin = Session["acc_login"] as string;

			if (!string.IsNullOrEmpty(sessionLogin)) {

				SqlConnection sqlCon = new SqlConnection(CONNECTION);

				if (sqlCon.State == ConnectionState.Closed) {
					sqlCon.Open();
				}

				SqlCommand getIsAdmin = new SqlCommand("SELECT acc_is_Admin FROM Account WHERE acc_login='" + sessionLogin + "'", sqlCon);

				SqlDataAdapter isAdminDA = new SqlDataAdapter(getIsAdmin);

				DataTable isAdminDT = new DataTable();
				isAdminDA.Fill(isAdminDT);

				string isAdmin = isAdminDT.Rows[0]["acc_is_Admin"].ToString().Trim();

				bool isAdminBoolean = bool.Parse(isAdmin);

				if(isAdminBoolean == true) {
				if (e.Row.RowType == DataControlRowType.DataRow) {
					Button deleteProposalButton = (Button)e.Row.FindControl("deleteProposal");
					deleteProposalButton.Visible = true;
				}
				}


			}
		}
	}
}



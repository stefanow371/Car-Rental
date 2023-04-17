using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Car_Rental {
	public partial class MasterPage : System.Web.UI.MasterPage {
		string con = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
		protected void Page_Load(object sender, EventArgs e) {
			string sessionLogin = Session["acc_login"] as string;
			try {
				if (!string.IsNullOrEmpty(sessionLogin)) {
					logOut.Visible = true;
					account.Visible = true;
					signIn.Visible = false;
					isAdmin();
				}
			}
			catch (Exception ex) {
				throw new Exception(ex.Message);
			}
		}
		protected void logout_Click(object sender, EventArgs e) {
			Session.Clear();
			Response.Redirect("HomePage.aspx");
		}

		protected void isAdmin() {
			string sessionLogin = Session["acc_login"] as string;
			try {
				if (!string.IsNullOrEmpty(sessionLogin)) {


					SqlConnection sqlCon = new SqlConnection(con);

					if (sqlCon.State == ConnectionState.Closed) {
						sqlCon.Open();
					}

					SqlCommand cmd = new SqlCommand("select acc_is_Admin from Account where acc_login='" + sessionLogin + "'", sqlCon);

					SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					string query = dataTable.Rows[0]["acc_is_Admin"].ToString().Trim();
					bool booleanValue = bool.Parse(query);


					if (booleanValue == true) {

						adminPanel.Visible = true;
					}
				}
			}
			catch (Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}
	}
}
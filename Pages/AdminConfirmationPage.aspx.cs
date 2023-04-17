using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Xml.Linq;
using System.Threading;

namespace Car_Rental.Pages {
	public partial class AdminConfirmationPage : System.Web.UI.Page {
		string CONNECTION = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
		protected void Page_Load(object sender, EventArgs e) {

		}
		protected void confirmAsAdmin_OnClick(object sender, EventArgs e) {
			string codeField = AdminSpecialCode.Text.Trim();
			string sessionLogin = Session["acc_login"] as string;

			try {
				if (!string.IsNullOrEmpty(sessionLogin)) {
					SqlConnection sqlCon = new SqlConnection(CONNECTION);

					if (sqlCon.State == ConnectionState.Closed) {
						sqlCon.Open();
					}

					SqlCommand getUniqueCode = new SqlCommand("SELECT acc_unique_code FROM Account WHERE acc_login='" + sessionLogin + "'", sqlCon);
					SqlDataAdapter dataAdapter = new SqlDataAdapter(getUniqueCode);
					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					string uniqueCode = dataTable.Rows[0]["acc_unique_code"].ToString().Trim();

					if (uniqueCode == codeField) {
						changePermission();
						Response.Redirect("HomePage.aspx");
					}
				}
			}
			catch (Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		protected void changePermission() {
			string sessionLogin = Session["acc_login"] as string;

			SqlConnection sqlCon = new SqlConnection(CONNECTION);

			if (sqlCon.State == ConnectionState.Closed) {
				sqlCon.Open();
			}

			SqlCommand updatePermissions = new SqlCommand("UPDATE Account SET acc_is_Admin = @acc_is_Admin WHERE acc_login='" + sessionLogin + "'", sqlCon);

			updatePermissions.Parameters.AddWithValue("@acc_is_Admin", true);
			updatePermissions.ExecuteNonQuery();

			sqlCon.Close();

			Response.Write("<script>alert('Zaktualizowano');</script>");
			Response.Redirect("HomePage.aspx");
		}
	}
}
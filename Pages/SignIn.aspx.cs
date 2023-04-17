using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Car_Rental.Pages {
	public partial class SignIn : System.Web.UI.Page {
		string CONNECTION = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
		protected void Page_Load(object sender, EventArgs e) {
			string sessionLogin = Session["acc_login"] as string;

			try {
				if (!string.IsNullOrEmpty(sessionLogin)) {
					Response.Redirect("CustomerPanel.aspx");
				}
				else {
					return;
				}
			}
			catch (Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}
		protected void Login_Click(object sender, EventArgs e) {
			signIn();
		}

		private void signIn() {
			try {
				SqlConnection sqlCon = new SqlConnection(CONNECTION);

				if (sqlCon.State == ConnectionState.Closed) {
					sqlCon.Open();
				}
				string hashedPassword = Eramake.eCryptography.Encrypt(passwordTxt.Text);
				SqlCommand getLogInDetails = new SqlCommand("SELECT acc_login, acc_password FROM Account WHERE acc_login='" + loginTxt.Text + "'AND acc_password='" + hashedPassword + "'", sqlCon);

				SqlDataAdapter dataAdapter = new SqlDataAdapter(getLogInDetails);
				DataTable dataTable = new DataTable();
				dataAdapter.Fill(dataTable);

				foreach (DataRow dataRow in dataTable.Rows) {
					Session["acc_login"] = dataRow["acc_login"].ToString();
					Response.Redirect("CustomerPanel.aspx");
				}
			}
			catch (Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}
	}
}
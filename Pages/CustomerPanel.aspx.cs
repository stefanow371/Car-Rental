using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;

namespace Car_Rental.Pages {
	public partial class PanelKlienta : System.Web.UI.Page {
		string CONNECTION = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

		protected void Page_Load(object sender, EventArgs e) {
			string sessionLogin = Session["acc_login"] as string;
			try {
				if (!string.IsNullOrEmpty(sessionLogin)) {
					getAccountDetails();
				}
				else {
					return;
				}
			}
			catch (Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		private void getAccountDetails() {
			try {
				SqlConnection SqlCon = new SqlConnection(CONNECTION);

				if (SqlCon.State == ConnectionState.Closed) {
					SqlCon.Open();
				}

				SqlCommand getACCDetails = new SqlCommand("SELECT * FROM Account WHERE acc_login='" + Session["acc_login"].ToString() + "';", SqlCon);
				SqlDataAdapter dataAdapter = new SqlDataAdapter(getACCDetails);
				DataTable dataTable = new DataTable();
				dataAdapter.Fill(dataTable);

				string nameTextBox = dataTable.Rows[0]["acc_name"].ToString();
				nameTxt.Text = nameTextBox.Trim();
				this.nameTxt.Enabled = false;

				string lastNameTextBox = dataTable.Rows[0]["acc_last_name"].ToString();
				lastNameTxt.Text = lastNameTextBox.Trim();
				this.lastNameTxt.Enabled = false;

				string cityTextBox = dataTable.Rows[0]["acc_city"].ToString();
				cityTxt.Text = cityTextBox.Trim();
				this.cityTxt.Enabled = false;

				string postalCodeTextBox = dataTable.Rows[0]["acc_postal_code"].ToString();
				postalCodeTxt.Text = postalCodeTextBox.Trim();
				this.postalCodeTxt.Enabled = false;

				string streetTextBox = dataTable.Rows[0]["acc_street"].ToString();
				streetTxt.Text = streetTextBox.Trim();
				this.streetTxt.Enabled = false;

				string houseNumberTextBox = dataTable.Rows[0]["acc_house_number"].ToString();
				houseNumberTxt.Text = houseNumberTextBox.Trim();
				this.houseNumberTxt.Enabled = false;

				string loginTextBox = dataTable.Rows[0]["acc_login"].ToString();
				loginTxt.Text = loginTextBox.Trim();
				this.loginTxt.Enabled = false;

				string emailTextBox = dataTable.Rows[0]["acc_email"].ToString();
				emailTxt.Text = emailTextBox.Trim();
				this.emailTxt.Enabled = false;

				string phoneNumberTextBox = dataTable.Rows[0]["acc_phone_number"].ToString();
				phoneNumberTxt.Text = phoneNumberTextBox.Trim();
				this.phoneNumberTxt.Enabled = false;

				string genderTextBox = dataTable.Rows[0]["acc_gender"].ToString();
				genderTxt.Text = genderTextBox.Trim();
				this.genderTxt.Enabled = false;
			}
			catch (Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}
		protected void deleteAccount(object sender, EventArgs e) {
			codeText.Visible = true;
			lastNameDeleteAccTxt.Visible = true;
			deleteBtn.Visible = true;
		}

		protected void deleteAccountButton(object sender, EventArgs e) {
			string sessionLogin = Session["acc_login"] as string;
			string lastNameTextboxValue = lastNameDeleteAccTxt.Text.Trim();
			try {
				if (!string.IsNullOrEmpty(sessionLogin)) {

					SqlConnection sqlCon = new SqlConnection(CONNECTION);

					if (sqlCon.State == ConnectionState.Closed) {
						sqlCon.Open();
					}

					SqlCommand getLastNameFromDB = new SqlCommand("SELECT acc_last_name FROM Account WHERE acc_login='" + sessionLogin + "'", sqlCon);

					SqlDataAdapter dataAdapter = new SqlDataAdapter(getLastNameFromDB);
					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					string lastNameDB = dataTable.Rows[0]["acc_last_name"].ToString().Trim();

					if (lastNameDB == lastNameTextboxValue) {
						deleteRecordFromDB();
						Session.Abandon();
						Response.Redirect("HomePage.aspx");
						Response.Write("<script>alert('Konto zostało usunięte');</script>");
					}
				}
			}
			catch (Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}
		protected void deleteRecordFromDB() {
			string sessionLogin = Session["acc_login"] as string;

			SqlConnection sqlCon = new SqlConnection(CONNECTION);

			if (sqlCon.State == ConnectionState.Closed) {
				sqlCon.Open();
			}

			SqlCommand deleteAccount = new SqlCommand("DELETE FROM Account WHERE acc_login='" + sessionLogin + "'", sqlCon);

			deleteAccount.ExecuteNonQuery();
			sqlCon.Close();

			Response.Redirect("HomePage.aspx");
			Response.Write("<script>alert('Usunięto!');</script>");
		}
	}
}

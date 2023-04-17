using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using System.Configuration;

namespace Car_Rental.Pages {
	public partial class EditAccountDetails : System.Web.UI.Page {
		string con = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
		protected void Page_Load(object sender, EventArgs e) { 

		}

		protected void UpdateButton_OnClick(object sender, EventArgs e) {
			try {
				string sessionLogin = Session["acc_login"] as string;
				if (!string.IsNullOrEmpty(sessionLogin)) {

					SqlConnection sqlCon = new SqlConnection(con);

					if (sqlCon.State == ConnectionState.Closed) {
						sqlCon.Open();
					}

					if (cityTxt.Text.Trim() != "") {	
						if (cityTxt.Text.IsEmpty() || cityTxt.Text == null || cityTxt.Text.Length >= 32) {
							Response.Write("<script>alert('Miasto nie może być puste i dłuższe niż 32 znaki');</script>");
							return;
						}
						else {
							SqlCommand updateCity = new SqlCommand("UPDATE Account SET acc_city = @acc_city WHERE acc_login='" + sessionLogin + "'", sqlCon);
							updateCity.Parameters.AddWithValue("@acc_city", cityTxt.Text.Trim());
							updateCity.ExecuteNonQuery();
						}
					}
					if (streetTxt.Text.Trim() != "") {
						if (streetTxt.Text.IsEmpty() || streetTxt.Text == null || streetTxt.Text.Length >= 32) {
							Response.Write("<script>alert('Ulica nie może być pusta i dłuższa niż 32 znaki');</script>");
							return;
						}
						else {
							SqlCommand updateStreet = new SqlCommand("UPDATE Account SET acc_street = @acc_street WHERE acc_login='" + sessionLogin + "'", sqlCon);
							updateStreet.Parameters.AddWithValue("@acc_street", streetTxt.Text.Trim());
							updateStreet.ExecuteNonQuery();
						}
					}
					if (houseNumberTxt.Text.Trim() != "") {
						if (houseNumberTxt.Text.IsEmpty() || houseNumberTxt.Text == null || houseNumberTxt.Text.Length >= 32) {
							Response.Write("<script>alert('Numer budynku nie może być pusty i dłuższy niż 32 znaków');</script>");
							return;
						}
						else {
							SqlCommand updateHouseNumber = new SqlCommand("UPDATE Account SET acc_house_number = @acc_house_number WHERE acc_login='" + sessionLogin + "'", sqlCon);
							updateHouseNumber.Parameters.AddWithValue("@acc_house_number", houseNumberTxt.Text.Trim());
							updateHouseNumber.ExecuteNonQuery();
						}
					}
					if (postalCodeTxt.Text.Trim() != "") {
						if (postalCodeTxt.Text.IsEmpty() || postalCodeTxt.Text == null || postalCodeTxt.Text.Length >= 32) {
							Response.Write("<script>alert('Kod pocztowy nie może być pusty i dłuższy niż 32 znaki');</script>");
							return;

						}
						else if (isValidPostalCode(postalCodeTxt.Text) == false) {
							Response.Write("<script>alert('Niepoprawny typ kodu pocztowego - Poprawny: xx-xxx');</script>");
							return;
						}
						else {
							SqlCommand updatePostalCode = new SqlCommand("UPDATE Account SET acc_postal_code = @acc_postal_code WHERE acc_login='" + sessionLogin + "'", sqlCon);
							updatePostalCode.Parameters.AddWithValue("@acc_postal_code", postalCodeTxt.Text.Trim());
							updatePostalCode.ExecuteNonQuery();
						}
					}
					if (phoneNumberTxt.Text.Trim() != "") {
						if (phoneNumberTxt.Text.IsEmpty() || phoneNumberTxt.Text == null || phoneNumberTxt.Text.Length >= 10) {
							Response.Write("<script>alert('Nieprawidłowa długośc nr. telefonu');</script>");
							return;
						}

						else if (IsValidPhoneNumber(phoneNumberTxt.Text) == false) {
							Response.Write("<script>alert('Nieprawidłowy numer telefonu');</script>");
							return;
						}
						else {
							SqlCommand updatePhoneNumber = new SqlCommand("UPDATE Account SET acc_phone_number = @acc_phone_number WHERE acc_login='" + sessionLogin + "'", sqlCon);
							updatePhoneNumber.Parameters.AddWithValue("@acc_phone_number", phoneNumberTxt.Text.Trim());
							updatePhoneNumber.ExecuteNonQuery();
						}
					}

				;

					
					sqlCon.Close();

					Response.Redirect("CustomerPanel.aspx");
					Response.Write("<script>alert('Zaktualizowano');</script>");
				}
			}
			catch (Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}
		protected bool isValidPostalCode(string postalCode) {
			Regex validatePostalCode = new Regex("^[0-9]{2}(?:-[0-9]{3})?$");
			return validatePostalCode.IsMatch(postalCode);
		}
		protected bool IsValidPhoneNumber(string phoneNumber) {
			Regex validatePhoneNumber = new Regex("^\\d{9}$");
			return validatePhoneNumber.IsMatch(phoneNumber);
		}
	}
}
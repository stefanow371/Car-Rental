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
using System.Web.WebPages;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Threading.Tasks;
using FluentEmail.Smtp;
using FluentEmail.Core;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading;
using System.Security.Cryptography;

namespace Car_Rental.Pages {
	public partial class Rejestracja : System.Web.UI.Page {
		string CONNECTION = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
		string GENDER = null;
		string API_KEY = "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";
		string UNIQUE_CODE = randomKeyGenerator();
		protected void Page_Load(object sender, EventArgs e) {
			
		}
		protected void register_OnClick(object sender, EventArgs e) {
			if (checkAccountExists()) {
				Response.Write("<script>alert('Taki użytkownik już istnieje');</script>");
				return;
			}
			else {
				SendMail().Wait();
				if (CheckBox.Checked) {
					sendRequest().Wait();
				}
				registerAccount();
			}
		}
		protected void validateFields() {

			if (nameTxt.Text.IsEmpty() || nameTxt.Text == null || nameTxt.Text.Length >= 32) {
				Response.Write("<script>alert('Imię nie może być puste i dłuższe niż 32 znaki');</script>");
				return;
			}

			if (lastNameTxt.Text.IsEmpty() || lastNameTxt.Text == null || lastNameTxt.Text.Length >= 32) {	
				Response.Write("<script>alert('Nazwisko nie może być puste i dłuższe niż 32 znaki');</script>");
				return;
			}

			if (GENDER == null) {
				Response.Write("<script>alert('Nie wybrano płci!');</script>");
				return;
			}

			if (cityTxt.Text.IsEmpty() || cityTxt.Text == null || cityTxt.Text.Length >= 32) {
				Response.Write("<script>alert('Miasto nie może być puste i dłuższe niż 32 znaki');</script>");
				return;
			}

			if (postalCodeTxt.Text.IsEmpty() || postalCodeTxt.Text == null || postalCodeTxt.Text.Length >= 32) {
				Response.Write("<script>alert('Kod pocztowy nie może być pusty i dłuższy niż 32 znaki');</script>");
			}

			if (isValidPostalCode(postalCodeTxt.Text) == false) {
				Response.Write("<script>alert('Niepoprawny typ kodu pocztowego - Poprawny: xx-xxx');</script>");
				return;
			}

			if (streetTxt.Text.IsEmpty() || streetTxt.Text == null || streetTxt.Text.Length >= 32) {
				Response.Write("<script>alert('Ulica nie może być pusta i dłuższa niż 32 znaki');</script>");
				return;
			}

			if (loginTxt.Text.IsEmpty() || loginTxt.Text == null || loginTxt.Text.Length >= 16) {
				Response.Write("<script>alert('Login nie może być pusty i dłuższy niż 32 znaki');</script>");
				return;
			}

			if (loginTxt.Text.IsEmpty() || loginTxt.Text == null || passwordTxt.Text.Length >= 16 || passwordTxt.Text.Length < 8) {
				Response.Write("<script>alert('Hasło nie może być puste, krótsze niż 8 i dłuższy niż 16 znaków');</script>");
				return;
			}

			if (HasSpecialChars(passwordTxt.Text) == false) {
				Response.Write("<script>alert('Hasło musi zawierać znaki specjalne');</script>");
				return;
			}

			if (emailTxt.Text.IsEmpty() || emailTxt.Text == null || emailTxt.Text.Length >= 32) {
				Response.Write("<script>alert('Email nie może być pusty i dłuższy niż 32 znaków');</script>");
				return;
			}

			if (IsValidEmail(emailTxt.Text) == false) {
				Response.Write("<script>alert('Nieprawidłowa forma maila');</script>");
				return;
			}

			if (houseNumberTxt.Text.IsEmpty() || houseNumberTxt.Text == null || houseNumberTxt.Text.Length >= 32) {
				Response.Write("<script>alert('Numer budynku nie może być pusty i dłuższy niż 32 znaków');</script>");
				return;
			}

			if (phoneNumberTxt.Text.IsEmpty() || phoneNumberTxt.Text == null || phoneNumberTxt.Text.Length >= 10) {
				Response.Write("<script>alert('Nieprawidłowa długośc nr. telefonu');</script>");
				return;
			}

			if (IsValidPhoneNumber(phoneNumberTxt.Text) == false) {
				Response.Write("<script>alert('Nieprawidłowy numer telefonu');</script>");
				return;
			}

		}
		private void registerAccount() {
			try {
				genderPick();

				SqlConnection sqlCon = new SqlConnection(CONNECTION);

				if (sqlCon.State == ConnectionState.Closed) {
					sqlCon.Open();
				}

				SqlCommand cmd = new SqlCommand("INSERT INTO Account" +
					"(acc_name, acc_last_name,acc_gender,acc_city, acc_street," +
					"acc_house_number,acc_login,acc_password, acc_email," +
					" acc_postal_code, acc_phone_number, acc_is_Admin, acc_unique_code)" +

					"values(@acc_name, @acc_last_name,@acc_gender,@acc_city, @acc_street," +
					"@acc_house_number,@acc_login,@acc_password, @acc_email, @acc_postal_code," +
					" @acc_phone_number, @acc_is_Admin, @acc_unique_code)"
					, sqlCon);

				string password = Eramake.eCryptography.Encrypt(passwordTxt.Text.Trim());

				cmd.Parameters.AddWithValue("@acc_name", nameTxt.Text.Trim());
				cmd.Parameters.AddWithValue("@acc_last_name", lastNameTxt.Text.Trim());
				cmd.Parameters.AddWithValue("@acc_gender", GENDER);
				cmd.Parameters.AddWithValue("@acc_city", cityTxt.Text.Trim());
				cmd.Parameters.AddWithValue("@acc_street", streetTxt.Text.Trim());
				cmd.Parameters.AddWithValue("@acc_house_number", houseNumberTxt.Text.Trim());
				cmd.Parameters.AddWithValue("@acc_login", loginTxt.Text.Trim());
				cmd.Parameters.AddWithValue("@acc_password", password);
				cmd.Parameters.AddWithValue("@acc_email", emailTxt.Text.Trim());
				cmd.Parameters.AddWithValue("@acc_postal_code", postalCodeTxt.Text.Trim());
				cmd.Parameters.AddWithValue("@acc_is_Admin", false);
				cmd.Parameters.AddWithValue("@acc_phone_number", phoneNumberTxt.Text.Trim());
				cmd.Parameters.AddWithValue("@acc_unique_code", UNIQUE_CODE);

				validateFields();

				cmd.ExecuteNonQuery();
				sqlCon.Close();

				Response.Redirect("HomePage.aspx");
				Response.Write("<script>alert('Zarejestrowano!');</script>");
			}
			catch (Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}
		bool checkAccountExists() {
			try {
				SqlConnection sqlCon = new SqlConnection(CONNECTION);

				if (sqlCon.State == ConnectionState.Closed) {		
					sqlCon.Open();
				}

				SqlCommand checkIfAccountExists = new SqlCommand("SELECT * FROM Account WHERE acc_login='" + loginTxt.Text.Trim() + "' OR acc_email='" + emailTxt.Text.Trim() + "'", sqlCon);

				SqlDataAdapter dataAdapter = new SqlDataAdapter(checkIfAccountExists);
				DataTable dataTable = new DataTable();
				dataAdapter.Fill(dataTable);

				if (dataTable.Rows.Count > 0) {
					return true;
				}
				else {
					return false;
				}
			}
			catch (Exception e) {
				Response.Write("<script>alert('" + e.Message + "');</script>");
				return false;
			}
		}
		async Task SendMail() {
			try {
				var client = new SendGridClient(API_KEY);

				var sender = new EmailAddress("carrentltest@gmail.com", "CARRENTL");

				string reciever = emailTxt.Text.Trim();

				var recieverEmail = new EmailAddress(reciever, "Reciever");

				string emailSubject = "Hello, your account has been created!";

				string content = "<strong>Welcome on a board, now you can use CARRENTL</strong>";

				var msg = MailHelper.CreateSingleEmail(sender, recieverEmail, emailSubject, null, content);

				var resp = await client.SendEmailAsync(msg).ConfigureAwait(false);

			}
			catch (Exception e) {
				Response.Write("<script>alert('" + e.Message + "');</script>");
			}
		}

		async Task sendRequest() {
			try {
				var client = new SendGridClient(API_KEY);

				var senderEmail = new EmailAddress("carrentltest@gmail.com", "REQUEST");

				var recieverEmail = new EmailAddress("carrentltest@gmail.com", "Reciever");

				string requester = nameTxt.Text.Trim() + lastNameTxt.Text.Trim();

				string emailSubject = "Admin account Request from" + requester;

				string requestMessage = requestDescription.Text.Trim();

				string htmlContent = "<strong>Request motive + " + requestMessage + "unique code: " + UNIQUE_CODE + "</strong>";

				var msg = MailHelper.CreateSingleEmail(senderEmail, recieverEmail, emailSubject, null, htmlContent);

				var resp = await client.SendEmailAsync(msg).ConfigureAwait(false);

			}
			catch (Exception e) {
				Response.Write("<script>alert('" + e.Message + "');</script>");
			}
		}
		protected void sendAdminRequest_OnClick(object sender, EventArgs e) {
			if (CheckBox.Checked) {
				requestDescription.Visible = true;
				requestText.Visible = true;
			}
			if (!CheckBox.Checked) {
				requestDescription.Visible = false;
				requestText.Visible = false;
			}
		}
		private void genderPick() {
			if (Male.Checked) {
				GENDER = Male.Text.ToString();
			} else if (Female.Checked) {
				GENDER = Female.Text.ToString();
			}
		}
		private bool HasSpecialChars(string validateSpecialSigns) {
			return validateSpecialSigns.Any(ch => !char.IsLetterOrDigit(ch));
		}
		public bool IsValidEmail(string email) {
			Regex validateEmail = new Regex("^[^@\\s]+@[^@\\s]+\\.(com|net|org|gov|pl)$");
			return validateEmail.IsMatch(email);
		}
		public bool isValidPostalCode(string postalCode) {
			Regex validatePostalCode = new Regex("^[0-9]{2}(?:-[0-9]{3})?$");
			return validatePostalCode.IsMatch(postalCode);
		}
		public bool IsValidPhoneNumber(string phoneNumber) {
			Regex validatePhoneNumber = new Regex("^\\d{9}$");
			return validatePhoneNumber.IsMatch(phoneNumber);
		}
		private static string randomKeyGenerator() {
			var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var stringChars = new char[8];
			var random = new Random();

			for (int i = 0; i < stringChars.Length; i++) {
				stringChars[i] = chars[random.Next(chars.Length)];
			}

			var finalString = new String(stringChars);

			return finalString;
		}
	}
}
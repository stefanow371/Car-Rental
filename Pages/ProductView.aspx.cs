using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Car_Rental.Pages {
	public partial class ProductView : System.Web.UI.Page {
		string con = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
		string API_KEY = "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";

		protected void Page_Load(object sender, EventArgs e) {
			getCarDetails();
		}
		private void getCarDetails() {
			try {
				SqlConnection connection = new SqlConnection(con);

				if (connection.State == ConnectionState.Closed) {
					connection.Open();
				}

				SqlCommand cmd = new SqlCommand("SELECT * from CarDetails JOIN carImages ON CarDetails.car_id = carImages.car_table_Id WHERE car_Id='" + Session["car_Id"].ToString() + "';", connection);
				SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
				DataTable dataTable = new DataTable();
				dataAdapter.Fill(dataTable);

				string carID = dataTable.Rows[0]["car_Id"].ToString();

				string brandTextBox = dataTable.Rows[0]["car_brand"].ToString();
				brandTxt.Text = brandTextBox.Trim();
				this.brandTxt.Enabled = false;

				string modelTextBox = dataTable.Rows[0]["car_model"].ToString();
				modelTxt.Text = modelTextBox.Trim();
				this.modelTxt.Enabled = false;

				string carYearTextBox = dataTable.Rows[0]["car_production_year"].ToString();
				productionYearTxt.Text = carYearTextBox.Trim();
				this.productionYearTxt.Enabled = false;

				string engineCapacityTextBox = dataTable.Rows[0]["car_engine_capacity"].ToString();
				engineCapacityTxt.Text = engineCapacityTextBox.Trim();
				this.engineCapacityTxt.Enabled = false;

				string driveTypeTextBox = dataTable.Rows[0]["car_drive_type"].ToString();
				driveTypeTxt.Text = driveTypeTextBox.Trim();
				this.driveTypeTxt.Enabled = false;

				string fuelTypeTextBox = dataTable.Rows[0]["car_fuel_type"].ToString();
				fuelTypeTxt.Text = fuelTypeTextBox.Trim();
				this.fuelTypeTxt.Enabled = false;

				string doorsAmountTextBox = dataTable.Rows[0]["car_doors_amount"].ToString();
				doorsAmountTxt.Text = doorsAmountTextBox.Trim();
				this.doorsAmountTxt.Enabled = false;

				string seatsTextBox = dataTable.Rows[0]["car_seats_amount"].ToString();
				seatsAmountTxt.Text = seatsTextBox.Trim();
				this.seatsAmountTxt.Enabled = false;

				string isACTextBox = dataTable.Rows[0]["car_is_air_conditioning"].ToString();
				isACTxt.Text = isACTextBox.Trim();
				this.isACTxt.Enabled = false;

				string gearTypeTextBox = dataTable.Rows[0]["car_gear_type"].ToString();
				gearTypeTxt.Text = gearTypeTextBox.Trim();
				this.gearTypeTxt.Enabled = false;

				string carPriceTextBox = dataTable.Rows[0]["car_price"].ToString();
				carPriceTxt.Text = carPriceTextBox.Trim();
				this.carPriceTxt.Enabled = false;

				string carDescriptionTextBox = dataTable.Rows[0]["car_description"].ToString();
				carDescriptionTxt.Text = carDescriptionTextBox.Trim();
				this.carDescriptionTxt.Enabled = false;

				string imagePath = dataTable.Rows[0]["image_path"].ToString().Trim();
				Image1.ImageUrl = "Pages\\Pictures\\" + imagePath;
			}
			catch (Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}
		protected void Rent_Click(object sender, EventArgs e) {
			string sessionLogin = Session["acc_login"] as string;

			if (!string.IsNullOrEmpty(sessionLogin)) {
				if (validateDates() == true && validateCard() == true) {
					insertCardToDB();
					createRentDB();
					sendConfirmation().Wait();
				}
				else {
					Response.Write("<script>alert('Nieprawidłowe dane');</script>");
				}
			}
			else {
				Response.Write("<script>alert('Aby wypożyczyć musisz się zalogować');</script>");
			}
		}


		protected void insertCardToDB() {

			string sessionLogin = Session["acc_login"] as string;
			try {
				if (!string.IsNullOrEmpty(sessionLogin)) {
					SqlConnection sqlCon = new SqlConnection(con);

					if (sqlCon.State == ConnectionState.Closed) {
						sqlCon.Open();
					}

					SqlCommand getAccountID = new SqlCommand("SELECT acc_Id FROM Account WHERE acc_login='" + sessionLogin + "'", sqlCon);
					SqlDataAdapter dataAdapterID = new SqlDataAdapter(getAccountID);
					DataTable dataTableID = new DataTable();
					dataAdapterID.Fill(dataTableID);

					int relatedAccID = Convert.ToInt32(dataTableID.Rows[0]["acc_Id"]);
					getAccountID.ExecuteNonQuery();

					SqlCommand cmdInsert = new SqlCommand("INSERT INTO CardPayment (card_acc_Id, card_number, card_CVC, card_end_date) values(@card_acc_Id, @card_number, @card_CVC, @card_end_date)", sqlCon);

					string cardEncrypted = Eramake.eCryptography.Encrypt(cardNumberTxt.Text);
					string CVCEncrypted = Eramake.eCryptography.Encrypt(cardCVCTxt.Text);
					cmdInsert.Parameters.AddWithValue("@card_acc_Id", relatedAccID);
					cmdInsert.Parameters.AddWithValue("@card_number", cardEncrypted);
					cmdInsert.Parameters.AddWithValue("@card_CVC", CVCEncrypted);
					cmdInsert.Parameters.AddWithValue("@card_end_date", cardExpireDate.Value);
					cmdInsert.ExecuteNonQuery();
					sqlCon.Close();
				}
			}
			catch (Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}
		protected void createRentDB() {
			string sessionLogin = Session["acc_login"] as string;

			try {
				if (!string.IsNullOrEmpty(sessionLogin)) {
					SqlConnection sqlCon = new SqlConnection(con);

					if (sqlCon.State == ConnectionState.Closed) {
						sqlCon.Open();
					}

					SqlCommand sessionAccountID = new SqlCommand("SELECT acc_Id FROM Account WHERE acc_login='" + sessionLogin + "'", sqlCon);

					SqlDataAdapter dataAdapter = new SqlDataAdapter(sessionAccountID);
					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					int relatedAccID = Convert.ToInt32(dataTable.Rows[0]["acc_Id"]);
					sessionAccountID.ExecuteNonQuery();

					SqlCommand getRelatedCardID = new SqlCommand("SELECT card_Id FROM CardPayment WHERE card_acc_Id ='" + relatedAccID + "'", sqlCon);

					SqlDataAdapter dataAdapter1 = new SqlDataAdapter(getRelatedCardID);
					DataTable dataTable1 = new DataTable();
					dataAdapter1.Fill(dataTable1);

					int relatedCardID = Convert.ToInt32(dataTable1.Rows[0]["card_Id"]);
					getRelatedCardID.ExecuteNonQuery();

					SqlCommand insertRentDB = new SqlCommand("INSERT INTO Rent (rent_acc_Id, rent_car_Id, rent_card_Id, rent_start_date, rent_end_date)" +
						" values(@rent_acc_Id, @rent_car_Id, @rent_card_Id, @rent_start_date, @rent_end_date)", sqlCon);


					int carID = Convert.ToInt32(Session["car_Id"]);

					insertRentDB.Parameters.AddWithValue("@rent_acc_Id", relatedAccID);
					insertRentDB.Parameters.AddWithValue("@rent_car_Id", carID);
					insertRentDB.Parameters.AddWithValue("@rent_card_Id", relatedCardID);
					insertRentDB.Parameters.AddWithValue("@rent_start_date", startDate.Value);
					insertRentDB.Parameters.AddWithValue("@rent_end_date", endDate.Value);

					SqlCommand checkForDuplicates = new SqlCommand("SELECT * FROM Rent WHERE rent_acc_Id='" + relatedAccID + "'AND rent_car_Id='" + carID + "'", sqlCon);

					SqlDataAdapter dataAdapter2 = new SqlDataAdapter(checkForDuplicates);
					DataTable dataTable2 = new DataTable();
					dataAdapter2.Fill(dataTable2);

					if (dataTable2.Rows.Count > 1) {
						Response.Write("<script>alert('Wypożyczyłeś już to auto!');</script>");
						return;
					}
					else {
						insertRentDB.ExecuteNonQuery();
						checkForDuplicates.ExecuteNonQuery();
					}

					sqlCon.Close();
					Response.Redirect("HomePage.aspx");
				}
			}
			catch (Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		async Task sendConfirmation() {
			string sessionLogin = Session["acc_login"] as string;
			try {
				string reciever = "";
				if (!string.IsNullOrEmpty(sessionLogin)) {
					SqlConnection sqlCon = new SqlConnection(con);

					if (sqlCon.State == ConnectionState.Closed) {
						sqlCon.Open();
					}

					SqlCommand getAccountEmail = new SqlCommand("select acc_email from Account where acc_login='" + sessionLogin + "'", sqlCon);

					SqlDataAdapter dataAdapter = new SqlDataAdapter(getAccountEmail);
					DataTable dataTable = new DataTable();
					dataAdapter.Fill(dataTable);

					string sessionEmail = (dataTable.Rows[0]["acc_email"].ToString().Trim());
					reciever = sessionEmail;

					getAccountEmail.ExecuteNonQuery();
				}

				var client = new SendGridClient(API_KEY);

				var sender = new EmailAddress("carrentltest@gmail.com", "CARRENTL");

				var recieverEmail = new EmailAddress(reciever, "Reciever Name");

				string emailSubject = "Twoje wypożyczenie zostało zaakceptowane!";

				string Content = "<strong>Twoje zamówienie zostało przyjęte, zapraszamy po odbiór auta i szczegółowe informacje w dniu wypożyczenia</strong>";

				var msg = MailHelper.CreateSingleEmail(sender, recieverEmail, emailSubject, null, Content);

				var resp = await client.SendEmailAsync(msg).ConfigureAwait(false);

			}
			catch (Exception e) {
				Response.Write("<script>alert('" + e.Message + "');</script>");
			}
		}

		bool validateDates() {
			try {

				DateTime endDateTime = DateTime.Parse(endDate.Value);
				DateTime startDateTime = DateTime.Parse(startDate.Value);
				if (startDateTime < endDateTime) {
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

		bool validateCard() {
			DateTime cardEndTime = DateTime.Parse(cardExpireDate.Value);


			if (cardEndTime > DateTime.Today && cardNumberTxt.Text.Trim().Length == 16 && cardCVCTxt.Text.Trim().Length == 3) {
				return true;
			}
			else {

				return false;
			}
		}

	}
}
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
using System.IO;
using System.Collections;
using System.Web.WebPages;

namespace Car_Rental.Pages {
	public partial class AdminAddProduct : System.Web.UI.Page {
		string CONNECTION = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
		string POWERED_BY = null;
		string DOOR_AMOUNT = null;
		bool IS_ACC;
		string GEAR_TYPE = null;

		protected void Page_Load(object sender, EventArgs e) {
			int startYear = 1990;
			prodYear.DataSource = Enumerable.Range(startYear, DateTime.Now.Year - startYear + 1);
			prodYear.DataBind();
		}

		protected void insert_Btn(object sender, EventArgs e) {
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

					if (checkIfCarExistsDB() == true) {
						Response.Write("<script>alert('Taka oferta już istnieje');</script>");
						return;
					}

					if (isAdminBoolean == true) {
						validateCarDetails();
						saveImageToFiles();
						insertIntoCarDetailsDB();
						Response.Redirect("HomePage.aspx");
						Response.Write("<script>alert('Zapisano ofertę');</script>");
					}
				} else {
					Response.Write("<script>alert('Nie posiadasz uprawnień administratora');</script>");
					return;
				}
			} catch (Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		protected void insertIntoCarDetailsDB() {
			try {
				SqlConnection sqlCon = new SqlConnection(CONNECTION);

				if (sqlCon.State == ConnectionState.Closed) {
					sqlCon.Open();
				}

				SqlCommand cmd = new SqlCommand("INSERT INTO CarDetails" +
					"(car_brand, car_model,car_production_year,car_engine_capacity, car_drive_type," +
					"car_fuel_type,car_doors_amount,car_seats_amount, car_is_air_conditioning, car_gear_type, car_price, car_description)" +

					"values(@car_brand, @car_model,@car_production_year,@car_engine_capacity, @car_drive_type," +
					"@car_fuel_type,@car_doors_amount,@car_seats_amount, @car_is_air_conditioning, @car_gear_type, @car_price, @car_description)", sqlCon);

				cmd.Parameters.AddWithValue("@car_brand", brandTxt.Text.Trim());
				cmd.Parameters.AddWithValue("@car_model", modelTxt.Text.Trim());
				cmd.Parameters.AddWithValue("@car_engine_capacity", capacityTxt.Text.Trim());
				cmd.Parameters.AddWithValue("@car_production_year", productionYear());
				cmd.Parameters.AddWithValue("@car_drive_type", driveType());
				cmd.Parameters.AddWithValue("@car_fuel_type", poweredBy());
				cmd.Parameters.AddWithValue("@car_doors_amount", doorAmount());
				cmd.Parameters.AddWithValue("@car_seats_amount", seatsAmount());
				cmd.Parameters.AddWithValue("@car_is_air_conditioning", isACInluded());
				cmd.Parameters.AddWithValue("@car_gear_type", gearType());
				cmd.Parameters.AddWithValue("@car_price", Price.Text.Trim());
				cmd.Parameters.AddWithValue("@car_description", description.Text.Trim());

				cmd.ExecuteNonQuery();
				sqlCon.Close();

				sqlCon.Open();

				SqlCommand command = new SqlCommand("INSERT INTO carImages (image_path, car_table_Id) VALUES (@image_path, @car_table_Id)", sqlCon);

				command.Parameters.AddWithValue("@image_path", FileUpload.FileName);

				SqlCommand getCarID = new SqlCommand("select car_Id from CarDetails where car_model='" + modelTxt.Text + "'", sqlCon);

				SqlDataAdapter dataAdapter = new SqlDataAdapter(getCarID);
				DataTable dataTable = new DataTable();
				dataAdapter.Fill(dataTable);

				int query = (int)(dataTable.Rows[0]["car_Id"]);
				command.Parameters.AddWithValue("@car_table_Id", query);

				command.ExecuteNonQuery();
				sqlCon.Close();

				Response.Redirect("HomePage.aspx");
				Response.Write("<script>alert('Wprowadzono!');</script>");
			}

			catch (Exception ex) {
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		private string productionYear() {
			return prodYear.SelectedItem.Value.ToString().Trim();
		}
		private string driveType() {
			return drive.SelectedItem.Value.ToString().Trim();
		}
		private string seatsAmount() {
			return seats.SelectedItem.Value.ToString().Trim();
		}
		private string poweredBy() {
			if (Diesel.Checked) {
				return POWERED_BY = Diesel.Text.ToString();
			} else if (Petrol.Checked) {
				return POWERED_BY = Petrol.Text.ToString();
			} else if (Battery.Checked) {
				return POWERED_BY = Battery.Text.ToString();
			} else {
				return "";
			}
		}
		private string doorAmount() {
			if (doorsAmount3.Checked) {
				return DOOR_AMOUNT = doorsAmount3.Text.ToString();
			} else if (doorsAmount5.Checked) {
				return DOOR_AMOUNT = doorsAmount5.Text.ToString();
			} else {
				return "";
			}
		}
		private bool isACInluded() {
			if (included.Checked) {
				return IS_ACC = true;
			} else if (notIncluded.Checked) {
				return IS_ACC = false;
			} else {
				return false;
			}
		}
		private string gearType() {
			if (manual.Checked) {
				return GEAR_TYPE = manual.Text.ToString();
			} else if (automatic.Checked) {
				return GEAR_TYPE = automatic.Text.ToString();
			} else {
				return "";
			}
		}
		protected void validateCarDetails() {
			if (brandTxt.Text.IsEmpty() || brandTxt.Text == null || brandTxt.Text.Length >= 64) {
				Response.Write("<script>alert('Nazwa marki auta nie może być pusta i dłuższa niż 64 znaki');</script>");
				return;
			}

			if (modelTxt.Text.IsEmpty() || modelTxt.Text == null || modelTxt.Text.Length >= 64) {
				Response.Write("<script>alert('Nazwa modelu auta nie może być pusta i dłuższa niż 64 znaki');</script>");
				return;
			}

			if (capacityTxt.Text.IsEmpty() || capacityTxt.Text == null || capacityTxt.Text.Length >= 20) {
				Response.Write("<script>alert('Wartość pojemności silnika nie może być pusta i większa niż 20');</script>");
				return;
			}

			if (productionYear() == null || productionYear().IsEmpty()) {
				Response.Write("<script>alert('Nie wybrano roku produkcji');</script>");
				return;
			}

			if (driveType() == null || driveType().IsEmpty()) {
				Response.Write("<script>alert('Nie wybrano rodzaju napędu');</script>");
				return;
			}

			if (seatsAmount() == null || seatsAmount().IsEmpty()) {
				Response.Write("<script>alert('Nie wybrano liczby miejsc');</script>");
				return;
			}

			if (doorAmount() == null || doorAmount().IsEmpty()) {
				Response.Write("<script>alert('Nie wybrano liczby drzwi');</script>");
				return;
			}

			if (poweredBy() == null) {
				Response.Write("<script>alert('Nie wybrano rodzaju paliwa');</script>");
				return;
			}

			if (gearType() == null || gearType().IsEmpty()) {
				Response.Write("<script>alert('Nie wybrano typu napędu');</script>");
				return;
			}
		}
		protected void saveImageToFiles() {
			string extension = System.IO.Path.GetExtension(FileUpload.PostedFile.FileName);
			string imagePath = Server.MapPath("Pages/Pictures/");

			if (!Directory.Exists(imagePath)) {
				Directory.CreateDirectory(imagePath);
			}
			FileUpload.PostedFile.SaveAs(imagePath + Path.GetFileName(FileUpload.FileName));
		}
		protected bool checkIfCarExistsDB() {
			try {
				SqlConnection sqlCon = new SqlConnection(CONNECTION);

				if (sqlCon.State == ConnectionState.Closed) {
					sqlCon.Open();
				}

				SqlCommand checkIfExist = new SqlCommand("SELECT * FROM CarDetails WHERE car_model='" + modelTxt.Text.Trim() + "'", sqlCon);
				SqlDataAdapter checkIfExistDA = new SqlDataAdapter(checkIfExist);
				DataTable checkIfExistDT = new DataTable();
				checkIfExistDA.Fill(checkIfExistDT);

				if (checkIfExistDT.Rows.Count > 0) {
					return true;
				} else {
					return false;
				}
			}
			catch (Exception e) {
				Response.Write("<script>alert('" + e.Message + "');</script>");
				return false;
			}
		}
	}
}
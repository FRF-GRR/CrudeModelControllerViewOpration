using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUDWITHModelViewController.Models;
using Newtonsoft.Json;
using System.Data;

namespace CRUDWITHModelViewController.Controllers
{
    public class CascadingDropDownController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;initial catalog=dbCRUDEddlJuly28_26;integrated security=true");

        public ActionResult CascadingDropDownForm()
        {
            return View();
        }

        //public ActionResult CascadingDropDownShow()
        //{
        //    return View();
        //}

        public JsonResult GetCountries()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from countries", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            string countries = JsonConvert.SerializeObject(dt);
            return Json(countries, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStates(int? CountryId)
        {
            if (CountryId == null || CountryId == 0)
            {
                return Json(new List<object>(), JsonRequestBehavior.AllowGet);
            }

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from states where counterid = @C", con);
            cmd.Parameters.AddWithValue("@C", CountryId);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            string states = JsonConvert.SerializeObject(dt);
            return Json(states, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCities(int stateId)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from cities where StateId = @StateId", con);
            cmd.Parameters.AddWithValue("@StateId", stateId);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            string cities = JsonConvert.SerializeObject(dt);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        public void InsertData(CascadingDropDownClass obj)
        {
            if (obj.id == 0)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("procemployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@type", "insert");
                cmd.Parameters.AddWithValue("@name", obj.name);
                cmd.Parameters.AddWithValue("@contact", obj.contact);
                cmd.Parameters.AddWithValue("@email", obj.email);
                cmd.Parameters.AddWithValue("@countries", obj.countries);
                cmd.Parameters.AddWithValue("@states", obj.states);
                cmd.Parameters.AddWithValue("@cities", obj.cities);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("procemployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@type", "update");
                cmd.Parameters.AddWithValue("@id", obj.id);
                cmd.Parameters.AddWithValue("@name", obj.name);
                cmd.Parameters.AddWithValue("@contact", obj.contact);
                cmd.Parameters.AddWithValue("@email", obj.email);
                cmd.Parameters.AddWithValue("@countries", obj.countries);
                cmd.Parameters.AddWithValue("@states", obj.states);
                cmd.Parameters.AddWithValue("@cities", obj.cities);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public JsonResult GetEmployee()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("procemployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@type", "show");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            string data = JsonConvert.SerializeObject(dt);
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public void DeleteEmployee(CascadingDropDownClass obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("procemployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("type", "delete");
            cmd.Parameters.AddWithValue("@id", obj.id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public JsonResult EditEmployee(CascadingDropDownClass obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("procemployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@type", "edit");
            cmd.Parameters.AddWithValue("@id", obj.id);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            string data = JsonConvert.SerializeObject(dt);
            return Json(data, JsonRequestBehavior.AllowGet);

        }

    }
}

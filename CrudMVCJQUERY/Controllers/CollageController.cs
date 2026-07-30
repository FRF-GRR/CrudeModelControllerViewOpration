using CRUDWITHModelViewController.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDWITHModelViewController.Controllers
{
    public class CollageController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;initial catalog=dbCRUDEddlJuly28_26;integrated security=true");

        public ActionResult CollageForm()
        {
            return View();
        }
        public JsonResult bindcountries()
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
        public JsonResult bindstates()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from states", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            string states = JsonConvert.SerializeObject(dt);
            return Json(states, JsonRequestBehavior.AllowGet);
        }

        public void InsertData(CollageClass obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("proctblmanager", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("action", "insert");
            cmd.Parameters.AddWithValue("@name", obj.Name);
            cmd.Parameters.AddWithValue("@country", obj.Countries);
            cmd.Parameters.AddWithValue("@state", obj.States);
            cmd.ExecuteNonQuery();
            con.Close();
            //hello
        }
        public JsonResult ShowData()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from tblmanager", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            string data = JsonConvert.SerializeObject(dt);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
    }

}
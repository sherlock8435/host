using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  Model;

namespace ViewModel
{
    public class CityDB : DBFunctions
    {
        private CitiesList list = new CitiesList();
        private Cities CreateModel(Cities c)
        {
            c.CityId = (int)reader["id"];
            c.CityName = (string)reader["name"];
            return c;
        }

        public CitiesList SlectAllCities()
        {
            try
            {
                string sqlStr = "Select * From Citytbl";
                cmd = GenerateOleDBCommand(sqlStr, "DB.accdb");
                conObjll.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cities c = new Cities();
                    list.Add(CreateModel(c));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (this.conObjll.State == System.Data.ConnectionState.Open)
                    this.conObjll.Close();
            }
            return list;
        }
        public Cities SelectCityByName(string cityName)
        {
            list = SlectAllCities();
            Cities c = list.Find(Item=>Item.CityName == cityName);
            return c;
        }
        public Cities SelectCityById(int id)
        {
            list = SlectAllCities();
            Cities c = list.Find(Item => Item.CityId == id);
            return c;
        }
        public List<Cities> OrderByCityName()
        {
            list = SlectAllCities();
            return list.OrderBy(item=>item.CityName).ToList();
        }
    }
}

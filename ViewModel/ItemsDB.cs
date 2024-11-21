using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class ItemsDB : DBFunctions
    {
        private ItemsList list = new ItemsList();
        DBFunctions bkDB = new DBFunctions();

        private Items CreateModel(Items b)  // fill in the object b
        {
            b.ItemsCode = (int)reader["ItemsCode"];
            b.Name = reader["BookName"].ToString();
            b.Quantity = int.Parse(reader["Quantity"].ToString());
            b.ItemImg = reader["ItemImage"].ToString();
            b.Description = reader["Description"].ToString();
            b.Price = int.Parse(reader["Price"].ToString());
            b.Category = (Catrgory)reader["Category"];

            return b;
        }  // end of CreateModel(Book b)

        private ItemsList GetBookList(string sqlStr)
        {
            try
            {
                cmd = GenerateOleDBCommand(sqlStr, "DB.accdb");
                conObjll.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Items c = new Items();
                    list.Add(CreateModel(c));  // add the obj to the book list
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
                if(this.conObjll.State == System.Data.ConnectionState.Open)
                    this.conObjll.Close();
            }
            return list;
        }

        public ItemsList SelectAllBookList()
        {
            string sqlStr = "Select * From BooksTbl";
            list = GetBookList(sqlStr);
            return list;
        }

        public Items SelectBookByName(string BookName)
        {
            string sqlStr = "Select * From BooksTbl " +
                            " where BookName = '" + BookName + "'";
            list = GetBookList(sqlStr);
            Items c = list.Find(item => item.Name == BookName);
            return c;
        }

        public ItemsList SelectBookListByCategory(string CategoryName)
        {
            string sqlStr = "Select * From BooksTbl " +
                            "where Category ='" + CategoryName + "'";
            list = GetBookList(sqlStr);
            return list;
        }
        public ItemsList SelectBookListByPrice(double price1, double price2)
        {
            string sqlStr = "Select * From BooksTbl " +
                            "where price between " + price1 + " and " + price2;
            list = GetBookList(sqlStr);
            return list;
        } 

        public int AddBook(Items book)
        {
            string insertSql = string.Format("insert into Itemstbl " +
                            "(ItemCode,Name,Quantity,ItemImage,Description, Price, Category)" +
                            " values ({0}, '{1}', '{2}', '{3}', {4}, '{5}', '{6}')",
                            book.ItemsCode, book.Name, book.Quantity, book.ItemImg,
                            book.Description, book.Price, book.Category);
            return bkDB.ChangeTable(insertSql, "DB.accdb");
        } 

        public int UpdateBook(Items book)
        {
            string updateSql = string.Format("update Itemstbl SET " +
                            "ItemCode='{0}', Name='{1}', " +
                            "Quantity='{2}', ItemImage={3}, description='{4}', " +
                            "price='{5}', Category='{6}' " +
                            "where ISBN=" + book.ItemsCode,
                            book.Name, book.Quantity, book.ItemImg,
                            book.Description, book.Price, book.Category);
            return bkDB.ChangeTable(updateSql, "DB.accdb");
        } 

        public int DeleteBook(int ISBN)
        {
            string delSql = string.Format("Delete from Itemstbl " +
                            " where ItemCode=" + ISBN);
            return bkDB.ChangeTable(delSql, "DB.accdb");
        } 

    } 
}

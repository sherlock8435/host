using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class OrderDB:DBFunctions
    {
        private OrderList list = new OrderList();
        DBFunctions dbf = new DBFunctions();

        private Order CreateModel(Order order)
        {
            order.UEmail = reader["UEmail"].ToString();
            order.ItemId = int.Parse(reader["ItemId"].ToString());
            order.OrderStatus = reader["OrderStatus"].ToString();
            order.OrderDate = reader["OrderDate"].ToString();
            order.Qnty = int.Parse(reader["OrderQnty"].ToString());
            order.Price = int.Parse(reader["Price"].ToString());
            order.VisaNumber = reader["VisaNr"].ToString();

            return order;
        }

        private OrderList SelectOrders(string sqlStr)
        {
            try
            {
                cmd = GenerateOleDBCommand(sqlStr, "DB.accdb");
                conObjll.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Order c = new Order();
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

        public OrderList SelectAllOrders(string uEmail)
        {
            string sqlStr = "Select * From Ordertbl where Uemail='" + uEmail + "'";
            return SelectOrders(sqlStr);
        }

        public int AddOrder(Order od)
        {
            string insertSql = string.Format("insert into Ordertbl " +
                        "(ItemId, UserEmail, OrderDate, OrderQnty, Price, OrderStatus, VisaNr) values ({0}, '{1}', '{2}', '{3}', {4}, {5}, '{6}')",
                        od.ItemId, od.UEmail, od.OrderDate, od.Qnty, od.Price, od.OrderStatus, od.VisaNumber);
            return dbf.ChangeTable(insertSql, "DB.accdb");
        }

        public OrderList SelectOrdersByOrderDate(string uEmail, string orderDate)
        {
            string sqlStr = string.Format("Select * From Ordertbl where userEmail='" + uEmail + "' and OrderDate='" + orderDate + "'");
            return SelectOrders(sqlStr);
        }

        public Order SelectOneOrder(string uEmail, string orderDate, int ISBN)
        {
            string sqlStr = string.Format("Select * From Ordertbl where userEmail='" + uEmail + "' and OrderDate='" + orderDate + "' and ItemId=" + ISBN);
            list = SelectOrders(sqlStr);
            Order order = list.Find(item => item.ItemId == ISBN && item.OrderDate == orderDate);
            return order;
        }

        public OrderList SelectOrdersByStatus(string uEmail, string status)
        {
            string sqlStr = "Select * From Ordertbl where userEmail='" + uEmail + "' and orderStatus='" + status + "'";
            list = SelectOrders(sqlStr);
            return list;
        }

        public int UpdateOrderStatus(Order order, string orderStatus)
        {
            string updateSql = string.Format("update Ordertbl SET orderStatus='" + orderStatus + "' where UserEmail='" + order.UEmail + "' and ISBN=" + order.ItemId + " and OrderDate='" + order.OrderDate + "'");
            return dbf.ChangeTable(updateSql, "DB.accdb");
        }

        public int DeleteUserOrderByEmail(string uEmail, int ISBN, string orderDate)
        {
            string delSql = string.Format("Delete from Ordertbl where userEmail='" + uEmail + "' and ISBN=" + ISBN + " and OrderDate='" + orderDate + "'");
            return dbf.ChangeTable(delSql, "DB.accdb");
        }
    }
}

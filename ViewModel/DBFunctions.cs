using System;
using System.Data;
using System.Data.OleDb;
using System.Runtime.CompilerServices;

namespace ViewModel
{
    public class DBFunctions
    {
        protected OleDbConnection conObjll;
        protected OleDbCommand cmd;
        protected OleDbDataReader reader;

        public DBFunctions()
        {
            conObjll = new OleDbConnection();
            cmd = new OleDbCommand();
        }

        public OleDbCommand GenerateOleDBCommand(string sqlStr, string dbFileName)
        {
            conObjll = GenerateConnection(dbFileName);
            cmd = new OleDbCommand(sqlStr, conObjll);
            return cmd;
        }

        private string Path()
        {
            string currentDir = Environment.CurrentDirectory;
            string[] dirStr = currentDir.Split('\\');
            int index = dirStr.Length - 3;
            dirStr[index] = "ViewModel";
            Array.Resize(ref dirStr, index + 1);
            string pathStr = String.Join("\\", dirStr);
            return pathStr;
        }

        public OleDbConnection GenerateConnection(string dbFileName)
        {
            try
            {
                if (dbFileName.Contains(".mdb"))
                    conObjll.ConnectionString = "Provider=Microsoft.jet.OLEDB.4.0;Data Source=" + Path() + "\\App_Data\\" + dbFileName;
                else
                    conObjll.ConnectionString = "Provider=Microsoft.Ace.OLEDB.12.0;Data Source=" + Path() + "\\App_Data\\" + dbFileName;
                conObjll.Open();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                conObjll.Close();
            }
            return conObjll;
        }

        public DataTable Select(string sqlString, string dbFileName)
        {
            conObjll = GenerateConnection(dbFileName);
            DataTable dt = null;
            DataSet dsUser = new DataSet();
            try
            {
                OleDbDataAdapter daObj = new OleDbDataAdapter(sqlString, conObjll);
                daObj.Fill(dsUser);
                dt = dsUser.Tables[0];
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                conObjll.Close();
            }
            return dt;

        }

        public int ChangeTable(string sqlString, string dbFileName)
        {
            int records = 0;
            cmd = GenerateOleDBCommand(sqlString, dbFileName);
            conObjll.Open();
            try
            {
                records = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                conObjll.Close();
            }
            return records;
        }
    }
}

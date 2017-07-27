using WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SQLite;
using System.Configuration;
using System.Data.Common;
//using System.Data.SqlClient;


namespace WebApplication2.Controllers
{

    public class PanelsController : ApiController
    {
        static bool hasRun = false;
        string dbName = "Project.sqlite";
        string tableName = "Panel";
        string tableColumns, tableColumnsWthType;//, tableColumnsForValues;

        //1. Use a listdefined in System.Collections.Generic;
        List<Panel> panels = new List<Panel>();

        //Database connection
        SQLiteConnection m_dbConnection;

        // Creates an empty database file
        void createNewDatabase()
        {
            SQLiteConnection.CreateFile("C:\\Database\\Project.sqlite");
        }

        // Creates a connection with our database file.
        void connectToDatabase()
        {
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["test"];
            m_dbConnection = new SQLiteConnection("Data Source=" + "C:\\Database\\Project.sqlite" + "; Version=3;");
            m_dbConnection.Open();
        }

        // Creates a table
        void createTable()
        {
            //string sql = "create table panel (Id int,Name varchar(20), Category varchar(20),Price int)";
            //SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            //command.ExecuteNonQuery();

            tableColumns = "Id, Name, Category,Price";
            //tableColumnsForValues = "@Id, @Name, @Category,@Price";
            tableColumnsWthType = "Id INT primary key, Name varchar(20), Category varchar(20),Price int";

            string sql = "create table " + tableName + " (" + tableColumnsWthType + ")";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        //Insert some data into table
        void fillTable()
        {
            SQLiteCommand command = new SQLiteCommand(m_dbConnection);
            command.CommandText = "INSERT INTO " + tableName + " (" + tableColumns + ") "+ "VALUES" +"( 1, 'DA Panel','Home',100);";
            command.ExecuteNonQuery();

            command = new SQLiteCommand(m_dbConnection);
            command.CommandText = "INSERT INTO " + tableName + " (" + tableColumns + ") " + "VALUES" + "( 2, 'Flexes Panel','Industrial',200);";
            command.ExecuteNonQuery();

            command = new SQLiteCommand(m_dbConnection);
            command.CommandText = "INSERT INTO " + tableName + " (" + tableColumns + ") " + "VALUES" + "( 3, 'Anx Panel','Commercial',300);";
            command.ExecuteNonQuery();
        }

        public PanelsController()
        {
            if (hasRun == false)
            {
                hasRun = true;
                createNewDatabase();
                connectToDatabase();
                createTable();
                fillTable();
            }
        }

        //1. Use a listdefined in System.Collections.Generic;
        //List<Panel> panels = new List<Panel>();
        public List<Panel> GetAllPanels()//Panel[] GetAllPanels()////List<Panel> GetAllPanels()
        {


            string selectQuery = "SELECT Id, Name, Category, Price FROM Panel";
            SQLiteCommand command = new SQLiteCommand(selectQuery, m_dbConnection);


            SQLiteDataReader reader = null;
            //Panel mypanel = null;

            // at this point make sure your connection is open
            try
            {
                //m_dbConnection.Open();
                if (m_dbConnection.State != ConnectionState.Open)
                {
                    m_dbConnection.Close();
                    m_dbConnection.Open();
                }
                reader = command.ExecuteReader();
                while (reader.Read())
                {

                    Panel mypanel = new Panel();

                    // get the results of each column
                    mypanel.Id   = reader.IsDBNull(reader.GetOrdinal("Id")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id"));
                    mypanel.Name = reader.IsDBNull(reader.GetOrdinal("Name"))? string.Empty : reader.GetString(reader.GetOrdinal("Name")); //reader.GetString(reader.GetOrdinal("Name")));
                    mypanel.Category = reader.IsDBNull(reader.GetOrdinal("Category")) ? string.Empty : reader.GetString(reader.GetOrdinal("Category"));
                    mypanel.Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetInt32(reader.GetOrdinal("Price"));

                    panels.Add(mypanel);
                }
                
            }

            finally
            {
                // always call Close when done reading.
                if (reader != null)
                {
                    reader.Close();
                }
                // always call close the connection when done.
                if (m_dbConnection != null)
                {
                    m_dbConnection.Close();
                }
            }

            //If you want to return simply an array of panels you can ahieve this by calling the following function:
            // panels.ToArray(); 
            // and you can change the signature of your function to be public Panel[]  

            return panels;
            //panels.ToArray();
        }
 
        public IHttpActionResult GetPanel(int id)
        {
            var panel = panels.FirstOrDefault((p) => p.Id == id);
            if (panel == null)
            {
                return NotFound();
            }
            return Ok(panel);
        }

        // do work
    }
}

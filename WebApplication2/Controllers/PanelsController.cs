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

namespace WebApplication2.Controllers
{
    public class PanelsController : ApiController
    {
        string dbName = "Project.sqlite";
        string tableName = "Panel";
        string tableColumns, tableColumnsWthType, tableColumnsForValues;

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
            tableColumnsForValues = "@Id, @Name, @Category,@Price";
            tableColumnsWthType = "Id REAL primary key, Name varchar(20), Category varchar(20),Price int";

            string sql = "create table " + tableName + " (" + tableColumnsWthType + ")";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        //Insert some data into table
        void fillTable()
        {
            SQLiteCommand command = new SQLiteCommand(m_dbConnection);
            command.CommandText = "INSERT INTO " + tableName + " (" + tableColumns + ") "+ "VALUES" +"( 1, 'DA Panel','Home',10000);";
            command.ExecuteNonQuery();

            command = new SQLiteCommand(m_dbConnection);
            command.CommandText = "INSERT INTO " + tableName + " (" + tableColumns + ") " + "VALUES" + "( 2, 'Flexes Panel','Industrial',375000);";
            command.ExecuteNonQuery();

            command = new SQLiteCommand(m_dbConnection);
            command.CommandText = "INSERT INTO " + tableName + " (" + tableColumns + ") " + "VALUES" + "( 3, 'Anx Panel','Commercial',16990);";
            command.ExecuteNonQuery();
        }
        public PanelsController()
        {
            createNewDatabase();
            connectToDatabase();
            createTable();
            fillTable();


        }
        Panel[] panels = new Panel[]
       {
            new Panel { Id = 1, Name = "DA Panel", Category = "Home", Price = 1 },
            new Panel { Id = 2, Name = "Flexes Panel", Category = "Industrial", Price = 375 },
            new Panel { Id = 3, Name = "Anx Panel", Category = "Commercial", Price = 1699 }
       };

        public IEnumerable<Panel> GetAllPanels()
        {

            return panels;
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

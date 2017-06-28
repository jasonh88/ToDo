using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ToDo.Models;

namespace ToDo.Services
{
    public class TaskService : BaseService
    {
        public int CreateTask(TaskAddRequest payload)
        {
            /*
        1. You need a connection
        2. You need a command (a function)
          a. Name
           b. Parameters
         3. You need to execute that command
         a. ExecuteNonQuery ==> public void C#
         b. ExeuteReader ==> public List<T> C#
        */
            int id = 0;

            //set the connection string to a variable
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            //establish the connection
            using (SqlConnection sqlConn = new SqlConnection(connString))
            {
                //establish the command object
                using (SqlCommand cmd = new SqlCommand("dbo.Task_Insert", sqlConn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Task", payload.Task);

                    if (payload.Notes == null)
                    {
                        cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Notes", payload.Notes);
                    }



                    if (payload.Due == null)
                    {
                        cmd.Parameters.AddWithValue("@Due", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Due", payload.Due);
                    }

                  
                    cmd.Parameters.AddWithValue("@Completed", payload.Completed);

                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@ID";
                    param.SqlDbType = System.Data.SqlDbType.Int;
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    //Open SQL connection
                    sqlConn.Open();

                    //for insert or update statements
                    cmd.ExecuteNonQuery(); //takes total number of rows that are affected

                    id = (int)cmd.Parameters["@ID"].Value;
                }
            }
            return id;
        }

        public List<Tasks> GetTasks()
        {
            List<Tasks> taskList = new List<Tasks>();

            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection sqlConn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.Task_SelectAll", sqlConn))
                {
                    sqlConn.Open();
                    SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        Tasks t = new Tasks();
                        int startingIndex = 0;

                        //read data from db
                        t.ID = reader.GetInt32(startingIndex++);
                        t.Task = reader.GetString(startingIndex++);
                        t.Notes = reader.GetString(startingIndex++);
                        t.Due = reader.GetDateTime(startingIndex++);
                        t.Completed = reader.GetBoolean(startingIndex++);

                        taskList.Add(t);
                    }
                }
            }
            return taskList;
        }

        public void DeleteTask(int id)
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection sqlConn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.Task_Delete", sqlConn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    sqlConn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateTask(int id, Tasks model)
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection sqlConn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.Task_Update", sqlConn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Task", model.Task);

                    if (model.Notes == null)
                    {
                        cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Notes", model.Notes);
                    }

                    if (model.Due == null)
                    {
                        cmd.Parameters.AddWithValue("@Due", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Due", model.Due);
                    }

     
                    cmd.Parameters.AddWithValue("@Completed", model.Completed);

                    sqlConn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }


}
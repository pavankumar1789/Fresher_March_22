using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
//using System.Configuration;
namespace AdoCrud
{
    
        class Program
        {
        //private static object con;
        
        static void Main(string[] pavan)
        {
            callmethod();
           
            Console.WriteLine("Existing from the console...");
            
            Console.ReadLine();
        
           //Console.ReadKey();
        }
        public static void callmethod()
        {
            SqlConnection con = null;
            //string connectionstring = ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString;
            string connectionstring = "data source = .; database = employee_details; integrated security = SSPI";
            string forloop;
            do
            { 
            Console.WriteLine("Enter your choice: 1.create table 2. Insert 3. Retrieve 4.Update 5.Delete");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {

                case 1:
                        Console.WriteLine();
                    using (con = new SqlConnection(connectionstring))
                    {
                        SqlCommand createtable = new SqlCommand("Create table empdetails(id int , ename varchar(20), salary decimal(10,2))", con);
                        con.Open();
                        createtable.ExecuteNonQuery();
                        Console.WriteLine(" Table created");
                    }
                    break;

                case 2:
                        Console.WriteLine();
                        using (con = new SqlConnection(connectionstring))
                    {
                            Console.Write(" Enter the id to insert : ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine();
                            Console.Write(" Enter the name  to insert : ");
                            string ename1 = Console.ReadLine();
                            Console.WriteLine();
                            Console.Write(" Enter the salary to insert : ");
                           double salary =  Convert.ToDouble(Console.ReadLine());
                            SqlCommand insertquery = new SqlCommand(" insert into empdetails(id, ename, salary) values(@id, @ename, @salary)", con);
                            insertquery.Parameters.AddWithValue("id", id);
                            insertquery.Parameters.AddWithValue("ename", ename1);
                            insertquery.Parameters.AddWithValue("salary", salary);
                        con.Open();
                            try
                            {
                                insertquery.ExecuteNonQuery();
                                Console.WriteLine(" Record inserted successfully");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("You entered id is already exists in the database.."+ex.Message);
                            }
                       
                       
                    }
                    break;

                case 3:

                        Console.WriteLine();
                        try
                    {



                        con = new SqlConnection(connectionstring);
                        SqlCommand cmd = new SqlCommand("select * from empdetails", con);
                        con.Open();


                        //Console.WriteLine("The connection open" + cmd);
                        SqlDataReader str = cmd.ExecuteReader();
                            Console.WriteLine("The employees data are shown below ");
                        while (str.Read())
                        {
                            Console.WriteLine(str["id"] + " " + str["ename"] + " " + str["salary"]);
                        }

                        //Console.WriteLine("The result is" + str);
                        // Console.ReadKey();
                    }
                    catch (Exception e)
                    {


                        Console.WriteLine("Exception" + e);
                    }
                    finally
                    {
                        con.Close();
                        Console.WriteLine("connection closed");
                    }

                    break;

                case 4:
                        Console.WriteLine();
                        using (con = new SqlConnection(connectionstring))
                    {
                            int id;
                            Console.Write(" Enter an id number to perform the update on employee details based on this id :");
                            id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine();
                            Console.WriteLine($" what do u gonna update due to entered above id-{id} 1. ID 2. Employee Name 3. Salary");
                            int choiceforUpdate =Convert.ToInt32(Console.ReadLine());
                           
                            switch(choiceforUpdate)
                            {

                                case 1:
                                    Console.WriteLine();
                                    Console.Write($"Enter id to update the employee details in this id:{id} :");
                                    int empid = Convert.ToInt32(Console.ReadLine());
                                    SqlCommand Updatequery1 = new SqlCommand("Update empdetails SET id = @id WHERE id="+id+" ", con);
                                    Updatequery1.Parameters.AddWithValue("id", empid);
                                    //Updatequery1.Parameters.AddWithValue("id", id);
                                    con.Open();
                                    int Updateddetailsid = Updatequery1.ExecuteNonQuery();
                                    Console.WriteLine($"After the updation No.of rows affected: {Updateddetailsid}" );
                                    break;

                                case 2:
                                    Console.WriteLine();
                                    Console.Write($"Enter employee name to update in employee id {id}:");
                                    string employeename = Console.ReadLine();
                                    SqlCommand Updatequery2 = new SqlCommand("Update empdetails SET ename = @ename WHERE id=@id ", con);
                                    Updatequery2.Parameters.AddWithValue("ename", employeename);
                                    Updatequery2.Parameters.AddWithValue("id", id);
                                    con.Open();
                                    int Updateddetailsname = Updatequery2.ExecuteNonQuery();
                                    Console.WriteLine($"After the updation No.of rows affected: {Updateddetailsname}");
                                    break;

                                case 3:
                                    Console.WriteLine();
                                    Console.Write($"Enter salary based on employee id = {id}:");
                                    double salary = Convert.ToDouble(Console.ReadLine());
                                    SqlCommand Updatequery3 = new SqlCommand("Update empdetails SET salary = @salary WHERE id=@id ", con);
                                    Updatequery3.Parameters.AddWithValue("salary", salary);
                                    Updatequery3.Parameters.AddWithValue("id", id);
                                    con.Open();
                                    int UpdateddetailsSalary = Updatequery3.ExecuteNonQuery();
                                    Console.WriteLine($"After the updation No.of rows affected:{ UpdateddetailsSalary} ");
                                    break;
                            }
                       
                       
                        
                          
                           


                    }
                    break;

                case 5:
                        Console.WriteLine();
                        using (con = new SqlConnection(connectionstring))
                    {
                        Console.Write(" Enter an id to delete employee of that particular person : ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        SqlCommand Deletequery = new SqlCommand(" Delete from empdetails where id=@id ", con);
                            Deletequery.Parameters.AddWithValue("id", id);
                        con.Open();
                        Deletequery.ExecuteNonQuery();
                        Console.WriteLine($"The Id : {id} of this Record deleted successfully");

                    }
                    break;

                default:
                    Console.WriteLine("Please Enter values as shown in above range choice");
                    break;
            }
                Console.WriteLine();
                Console.WriteLine("Do you Want to Continue: yes/no ::");
                forloop = Console.ReadLine();
            } while (forloop!= "no" );



        }


    }
    
}

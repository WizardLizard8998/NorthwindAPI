using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using NorthwindAPI.Models;


namespace NorthwindAPI.Controllers
{
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        readonly IConfiguration Configuration;

        public EmployeeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        //  readonly string constr = Configuration.GetConnectionString("conStr");



        [HttpGet]
        [Route("api/GetEmployees")]
        public IActionResult GetEmployees()
        {



            string sqlcmd = "select * from Employees";

            DataTable dt = new DataTable();

            
            using (SqlConnection con = new SqlConnection(connectionString: Configuration.GetConnectionString("conStr")))
            {
                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(sqlcmd, con);

                    

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);

                    adp.Fill(dt);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                finally { con.Close(); }

            }


            return Ok(dt);


        }



        [HttpGet]
        [Route("api/GetEmployeeById/{ID}")]

        public IActionResult GetEmployeeById( int ID)
        {
            string sqlcmd = 
                " select * from Employees where EmployeeID = @ID ";

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString: Configuration.GetConnectionString("conStr")))
            {
                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(sqlcmd, con);

                    
                    SqlParameter param = new SqlParameter("@ID", SqlDbType.Int);
                    param.Value = ID;
                    cmd.Parameters.Add(param);
                    
                    
                   
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);

                    adp.Fill(dt);


                }catch(Exception ex)
                {
                    return BadRequest(ex);
                }
                finally
                { con.Close(); }
            
            
            
            
            }


            return Ok(dt);

        }




        // put yazımında tek istek üzerinden gelen veriyi değiştirmek için araştırma yap 
        

        [HttpPut]
        [Route("api/PutEmployee")]
        public IActionResult PutEmployee([FromBody] Employees employees)
        {

            string sqlcmd = "select COUNT(*) from Employees where EmployeeID = @ID";

            DataTable dt = new DataTable();

            // will be continue 

            string sqlcmdupdate = "update Employees " +
                                 "set " +
                                 " FirstName = @fname , LastName = @lname , Title =@title , TitleOfCourtesy =@toc , BirthDate =@birthdate , " +
                                 " HireDate =@hiredate  " +
                                 " where EmployeeID = @Id";  

            using (SqlConnection con = new SqlConnection(connectionString: Configuration.GetConnectionString("conStr")))
            {
                try
                {
                    con.Open();


                    SqlCommand cmd = new SqlCommand(sqlcmd, con);

                    SqlCommand cmd2 = new SqlCommand(sqlcmdupdate, con);

                    cmd.Parameters.AddWithValue("@ID", employees.EmployeeID);

                    var result  =  cmd.ExecuteScalar();


                    if(Convert.ToInt32(result) == 0)
                    {
                        throw new Exception("what da hell ?"); 
                    }

                    cmd2.Parameters.AddWithValue("@Id", employees.EmployeeID);                    
                    cmd2.Parameters.AddWithValue("@fname", employees.FirstName);
                    cmd2.Parameters.AddWithValue("@lname", employees.LastName);
                    cmd2.Parameters.AddWithValue("@title", employees.Title);
                    cmd2.Parameters.AddWithValue("@toc", employees.TitleOfCourtesy);
                    cmd2.Parameters.AddWithValue("@birthdate", employees.BirthDate);
                    cmd2.Parameters.AddWithValue("@hiredate", employees.HireDate);

                    cmd2.ExecuteNonQuery();





                }
                catch(Exception ex )
                {
                    return BadRequest(ex);
                }
                finally
                {
                    con.Close(); 
                }
            
                
             
                
             }

                return Ok();

        }


        


        [HttpDelete]
        [Route("api/DeleteEmployee/{ID}")]
        public IActionResult DeleteEmployee(int ID)
        {

            string sqlcmd = "delete from Employees where EmployeeId = @Id";

            string sqlcmd2 = "select COUNT(*) from Employees where EmployeeId = @Id";

            using(SqlConnection con = new SqlConnection(connectionString: Configuration.GetConnectionString("conStr")))
            {


                try
                {
                    con.Open();

                   SqlCommand cmd  =new SqlCommand (sqlcmd, con);

                    SqlCommand cmd2 = new SqlCommand (sqlcmd2, con);

                    cmd.Parameters.AddWithValue("@Id", ID);

                    cmd2.Parameters.AddWithValue("@Id", ID); 

                    
                    var result = cmd2.ExecuteScalar();

                    if(Convert.ToInt64(result) != 1)
                    {
                        throw new Exception("there is something unexpected occur ");
                    }

                    //Console.WriteLine(result);




                    cmd.ExecuteScalar();




                }catch(Exception ex)
                {
                    return BadRequest(ex);
                }
                finally
                {
                    con.Close(); 
                }


                return Ok("delete complette");

            }




        }




    }
}

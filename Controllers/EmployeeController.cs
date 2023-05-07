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



        

    }
}

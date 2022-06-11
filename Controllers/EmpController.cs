using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace WebApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpController : ControllerBase
    {  
        private readonly IConfiguration _confg;
        public EmpController(IConfiguration confg)
        {
            _confg = confg;
        }
        //if custom method u need to add routing with the path and name of method
        [HttpGet]
        public JsonResult Get()
        {
            string str_qry = @"select Employees.ID,Employees.Name as E_Name,Employees.Age,Departments.Name as D_Name
                                from Departments inner join Employees on Employees.Dept_ID=Departments.ID";
            string Con_str = _confg.GetConnectionString("CompanyDBCon");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(Con_str);
            SqlDataReader sql_rdr;
            using (conn)
            {

                conn.Open();
                using (SqlCommand cmd = new SqlCommand(str_qry, conn))
                { 
                  sql_rdr = cmd.ExecuteReader();
                    dt.Load(sql_rdr);
                    sql_rdr.Close();
                    conn.Close();
                }


            }
            return new JsonResult(dt);
        }




    }




}

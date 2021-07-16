using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SimpleApi.Models;
using System.Linq;

namespace SimpleApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private IConfiguration Configuration;

        public QuotesController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        [HttpGet]
        public string Index()
        {
            string quote = "";
            string connString = this.Configuration.GetConnectionString("MyConn");
            using (SqlConnection con= new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select quote from quotes where id=1", con);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while(sqlDataReader.Read())
                {
                    quote = sqlDataReader["quote"].ToString();
                }
                con.Close();
            }
            return quote;
        }

        [HttpGet]
        public string getQuotes()
        {
            string quote = "";
            using (var context = new quotesDBContext())
            {
                quote = (from q in context.Quotes
                             where q.Id == 3
                             select q.Quote1).FirstOrDefault().ToString();
                
            }
            return quote;
        }
        [HttpPost]
        public bool updateQuotes()
        {
            using (var context = new quotesDBContext())
            {
                var quoteDetails = (from q in context.Quotes
                                    where q.Id == 4
                                    select q).FirstOrDefault();
                quoteDetails.Quote1 = "NEW QUOTE";
                context.SaveChanges();

            }
            return true;
        }

    }
}
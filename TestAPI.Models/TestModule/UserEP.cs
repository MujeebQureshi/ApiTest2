//using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using Newtonsoft.Json;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Web.Routing;
using TestAPI.Models.Utility;

namespace TestAPI.Models
{
    public class UserEP
    {
        public static object ValidateUser(string username, string password, string type)
        {
            // Setup the connection and compiler
            var connection = new SqlConnection(ConfigurationManager.AppSettings["MySqlDBConn"].ToString());
            var compiler = new MySqlCompiler();
            var db = new QueryFactory(connection, compiler);
            
            try
            {
                password = DBManagerUtility._encodeJWT(new Dictionary<string, string>() { { "Password", password } }, AppConstants.AppSecretKeyPassword);

                // You can register the QueryFactory in the IoC container
                if (type == "USER")
                {
                    object response = db.Query("User").Where(q=> q.Where("Email", username).OrWhere("Username", username) )
                        .Where("Password", password)
                        .Where("RegistrationConfirmation", "Y").First();

                    var strResponse = response.ToString().Replace("DapperRow,", "").Replace("=", ":");
                    Dictionary<string,string> temp =  JsonConvert.DeserializeObject< Dictionary < string,string>>(strResponse);
                    return temp;
                }
                else if (type == "ADMIN")
                {
                    object response = db.Query("Admin").Where("AdmUserId", username).Where("Password", password).First();
                    var strResponse = response.ToString().Replace("DapperRow,", "").Replace("=", ":");
                    Dictionary<string, string> temp = JsonConvert.DeserializeObject<Dictionary<string, string>>(strResponse);
                    return temp;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                //Logger.WriteErrorLog(ex);
                //return new ErrorResponse(ex.Message, HttpStatusCode.BadRequest);
                return null;
            }

        }
        
    }
}

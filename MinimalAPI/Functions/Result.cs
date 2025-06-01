using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MinimalAPI.Models;

namespace MinimalAPI.Functions
{
    public static class Result
    {
        public static IResult HandleLogin([Microsoft.AspNetCore.Mvc.FromBody] Login data)
        {
            if (data.Login_UserName == "admin" && data.Login_PassWord == "admin")
            {
                return TypedResults.Ok(
                    $"User with this UserName: {data.Login_UserName} and this Pass: \"{data.Login_PassWord}\" has Logged in on {DateTime.Now}"
                );
            }
            else
            {
                return TypedResults.BadRequest("Just Admin is Accessible! 😁");
            }
        }
    }
}

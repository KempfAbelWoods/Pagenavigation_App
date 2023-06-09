﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Page_Navigation_App.DB;
using Page_Navigation_App.Helper;

namespace Page_Navigation_App.Configs;
public class ConnectionHelper
{
    public List<Db_Tasks> Tasks { get; set; }
    public List<Db_Users> UsersList { get; set; }


    public static (string , Error) SendMessage(string datasource)
    {

        var (userlist, err) = Rw_Users.Read("", Paths.sqlite_path);
        if (err!=null) { return (null,err); }
        var (taskList, error) = Rw_Tasks.Read("", Paths.sqlite_path);
        if (error!=null) { return (null,error); }
        var message = new ConnectionHelper
        {
            Tasks = taskList,
            UsersList = userlist
        };

        string jsonString = JsonSerializer.Serialize(message);
        //TODO anstatt return json string async connection 
        return (jsonString,null);

    }

    private static Random random = new Random();

    public static (string,Error) RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        try
        {
            return (new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray()),null);
        }
        catch (Exception e)
        {
            return ("",new Error(e.ToString()));
        }
        
    }
    
}


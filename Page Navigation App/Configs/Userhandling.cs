using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Page_Navigation_App.DB;
using Page_Navigation_App.Helper;

namespace Page_Navigation_App.Configs;

public static class Userhandling
{
    /// <summary>
    /// Gibt zurück ob der angemeldete Benutzer die Rechte für die Aktion besitzt
    /// </summary>
    /// <param name="neededrights">Integer: 0=read/1=write/2=fullaccess</param>
    /// <param name="enablemessage">Integer: get a Message if wanted</param>
    /// <returns>bool: 1=Access granted/ 0=no acess</returns>
    public static bool GrantPermission(int neededrights, bool enablemessage)
    {
        var (data, err) = Rw_Users.ReadwithUserName(ActualUser.Username, Paths.sqlite_path);
        if (err != null)
        {
            MessageBox.Show(err.GetException().Message);
        }
        if (data.Count == 0)
        {
            if (enablemessage)
            {
                MessageBox.Show("Please Login to access these features");
            }
            return false;
        }
        if (data.Count == 1)
        {
            if (data[0].Rights !=null && data[0].Rights !="")
            {
                string[] permissions = data[0].Rights.Split(",");
            
            foreach (var permission in permissions)
            {
                switch (neededrights)
                {
                    case 0:
                        if (permission == "readaccess")
                        {
                            return true;
                        }
                        break;
                    case 1:
                        if (permission == "writeaccess")
                        {
                            return true;
                        }
                        break;
                    case 2:
                        if (permission == "fullaccess")
                        {
                            return true;
                        }
                        break;
                }
            }
            }

            if (enablemessage)
            {
                MessageBox.Show("You do not have the rights for this function");
            }
        }

        return false;
    }
}
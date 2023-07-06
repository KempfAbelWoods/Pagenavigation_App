using System.Windows.Forms;

namespace Page_Navigation_App.Helper;

public class Error
{
    private readonly System.Exception _ex;


    /// <summary>
    /// Neuer Error aus string generieren
    /// </summary>
    /// <param name="err"></param>
    public Error(string err)
    {
        _ex = new System.InvalidOperationException(err);
    }


    /// <summary>
    /// Neuer Error aus Exception generieren
    /// </summary>
    /// <param name="err"></param>
    public Error(System.Exception err)
    {
        _ex = err;
    }


    /// <summary>
    /// Error als Exception zurueckgeben
    /// </summary>
    /// <returns></returns>
    public System.Exception GetException()
    {
        return _ex;
    }


    /// <summary>
    /// Error als string zurueckgeben
    /// </summary>
    /// <returns></returns>
    public string GetString()
    {
        return _ex.ToString();
    }


    private class Response
    {
        public string Status { get; set; }
    }
    
}
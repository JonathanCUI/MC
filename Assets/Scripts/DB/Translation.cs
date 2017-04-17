using System.Collections.Generic;
using Mono.Data.Sqlite;


public class Translation
{

    //data member
    private static Translation _instance;
    private Dictionary<string, string> _transDic;

    public static void Initialize()
    {
        _instance = new Translation();
    }


    private Translation()
    {
        _transDic = new Dictionary<string, string>();
        SqliteDataReader reader = DBManager.ExecuteQuery("SELECT * FROM Localization");
        while(reader.Read())
        {
            _transDic.Add(
                reader.GetString(reader.GetOrdinal("key")),
                reader.GetString(reader.GetOrdinal("value")));
        }
    }

    public static string GetTrans(string pKey)
    {
        return _instance._transDic[pKey];
    }
}


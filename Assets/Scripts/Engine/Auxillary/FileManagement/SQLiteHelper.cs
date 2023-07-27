using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;

public static class SQLiteHelper
{
    private static string connection = "URI=file:" + Application.persistentDataPath + "/" + "My_Database";
    static SQLiteHelper()
    {
        Debug.Log(connection);
    }

    public static void Test()
    {
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();
        
        IDbCommand dbcmd;
        IDataReader reader;

        dbcmd = dbcon.CreateCommand();
        string q_createTable =
          "CREATE TABLE IF NOT EXISTS " + "my_table" + " (" +
          "id" + " INTEGER PRIMARY KEY, " +
          "val" + " INTEGER )";
        Debug.Log(q_createTable);
        dbcmd.CommandText = q_createTable;
        reader = dbcmd.ExecuteReader();

        dbcon.Close();
    }
}

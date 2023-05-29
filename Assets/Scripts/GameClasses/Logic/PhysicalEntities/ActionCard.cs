using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Runtime.Serialization;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class ActionCard
{
    [Column("Index"),PrimaryKey, AutoIncrement]
    public int Index
    {
        get;
        private set;
    }
    [Column("Name")]
    public string name
    {
        get;
        private set;
    }

    private string[] procedures;

    public ActionCard()
    {

    }

}

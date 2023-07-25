using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string Name { get; private set; }
    public enum PlayerTypes
    {
        Player,
        Automa
    }

    private PlayerTypes _type;

    public PlayerTypes Type
    {
        get
        {
            return Type;
        }
    }

    public Player(PlayerTypes type, string name)
    {
        _type = type;
        Name = name;
    }
}

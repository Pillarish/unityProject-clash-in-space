using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;

public class AvailableArmy : MonoBehaviour
{
    public int warrior;
    public int archer;

    public bool checkAvailable(string minionName)
    {
        switch(minionName) {
            case "warrior":
                return this.warrior > 0;
            case "archer":
                return this.archer > 0;
            default:
                return false;
        }
    }

    public void useTroop(string minionName)
    {
        switch(minionName) {
            case "warrior":
                this.warrior -= 1;
                break;
            case "archer":
                this.archer -= 1;
                break;
        }
    }
}

using System;
using UnityEngine;
public class BuildLock
{
    public string identifier = "No name";
    public int remain = 0;
    public bool isChosen = false;
    public int tryBuild = 0;
    public KeyCode key;
    public BuildLock()
    {
    }

    public BuildLock(string newName){
       identifier = newName;
    }

}


using System.Collections;
using System.Collections.Generic;
using MyLibrary.DesignPattern;
using UnityEngine;

public partial class DataManager : Singleton<DataManager>
{
    static PlayerData PlayerData = new PlayerData();
    static UserData UserData = new UserData();
}

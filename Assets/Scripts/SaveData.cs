using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class SaveData
{
    public  bool varnost = true;
    public bool showTutorial = true;
    public  bool odzivnost, dihanje, cpr, aed = false;
    public int verjetnostPrezivetja = 100;

    /*public static int VerjetnostPrezivetja
    {
        get => verjetnostPrezivetja;
        set
        {
            if ((verjetnostPrezivetja - value) <= 0)
            {
                verjetnostPrezivetja = 0;
            }
           
        }
    }*/
}

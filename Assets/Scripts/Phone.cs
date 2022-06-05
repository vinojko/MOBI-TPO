using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Phone : MonoBehaviour
{

    public static Phone instance;
    [SerializeField]TextMeshProUGUI phoneText;

    private void Awake()
    {
        instance = this;
        phoneText.text = "";
    }
    
    public void Write1()
    {
        phoneText.text = phoneText.text + "1";
    }
    public void Write2()
    {
        phoneText.text = phoneText.text + "2";
    }
    public void Write3()
    {
        phoneText.text = phoneText.text + "3";
    }
    public void Write4()
    {
        phoneText.text = phoneText.text + "4";
    }
    public void Write5()
    {
        phoneText.text = phoneText.text + "5";
    }
    public void Write6()
    {
        phoneText.text = phoneText.text + "6";
    }
    public void Write7()
    {
        phoneText.text = phoneText.text + "7";
    }
    public void Write8()
    {
        phoneText.text = phoneText.text + "8";
    }
    public void Write9()
    {
        phoneText.text = phoneText.text + "9";
    }
    public void Write0()
    {
        phoneText.text = phoneText.text + "0";
    }
    public void WriteStar()
    {
        phoneText.text = phoneText.text + "*";
    }
    public void WriteLadder()
    {
        phoneText.text = phoneText.text + "#";
    }
    public void DeleteButton()
    {
        phoneText.text = phoneText.text.Remove(phoneText.text.Length - 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string _alias = "jperez";
    private int _coin = 0;
    private int _billete = 5;
    #region getset
    public string getAlias()
    {
        return _alias;
    }
    public int getCoin()
    {
        return _coin;
    }
    public int getBillete()
    {
        return _billete;
    }
    public void setAlias(string newAlias)
    {
        _alias = newAlias;
    }
    public void setCoin(int newCoin)
    {
        _coin += newCoin;
        Debug.Log(_coin);
    }
    public void setBillete(int newBillete)
    {
        _billete += newBillete;
    }
    #endregion
}

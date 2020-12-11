using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Newtonsoft.Json;
public class Player : MonoBehaviour
{
    
    private string _alias = "jperez";
    private int _coin = 0;
    private int _billete = 5;
    public Player(string alias, int coin, int billete)
    {
        _alias = alias;
        _coin = coin;
        _billete = billete;
    }

    public Player Jugador = new Player("jperez",0,0);
    #region getset
    public Player getPlayer()
    {
        return Jugador;
    }
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

    public void toObject(string jsonString)
    {
        Player m = JsonConvert.DeserializeObject<Player>(jsonString);
        Debug.Log(m._billete);
    }
}

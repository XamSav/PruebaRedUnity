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
    public Player(string alias, int coins, int billete)
    {
        _alias = alias;
        _coin = coins;
        _billete = billete;
    }

    //public Player Jugador = new Player("jperez",0,0);

    #region getset
    /*public Player getPlayer()
    {
        return Jugador;
    }*/
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
    }
    public void setBillete(int newBillete)
    {
        _billete += newBillete;
    }
    #endregion

    public void toObject(string jsonString)
    {
        Player m = JsonConvert.DeserializeObject<Player>(jsonString);
        _billete = m._billete;
        _coin = m._coin;
        Debug.Log(_billete +" "+ _coin);
    }
}

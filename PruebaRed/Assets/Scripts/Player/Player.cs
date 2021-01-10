using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[CreateAssetMenu(fileName = "PlayerAccount", menuName = "ScriptableObjects/PlayerAccount", order = 1)]
public class Player : ScriptableObject
{
    public string alias;
    public string name;
    private string password;
    public int score;
    public int coins;
    public int billetes;
    private string texto;
    public void getData(string peticiontexto)
    {
        texto = peticiontexto;
        //TransformData(texto);
    }
    public void TransformData(string peticiontexto)
    {
        Player m = JsonConvert.DeserializeObject<Player>(peticiontexto);
        alias = m.alias;
        name = m.name;
        billetes = m.billetes;
        coins = m.coins;
    }
    public void newCoin(int coin)
    {
        coins += coin;
    }
}

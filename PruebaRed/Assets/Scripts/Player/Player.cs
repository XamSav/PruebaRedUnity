using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "PlayerAccount", menuName = "ScriptableObjects/PlayerAccount", order = 1)]
public class Player : MonoBehaviour
{
    private string alias;
    public int score;
    public int coins;
    public int billetes;

    public Player(string alias)
    {
        this.alias = alias;
    }
    public void SetAlias(string newAlias)
    {
        alias = newAlias;
    }
    public string GetAlias()
    {
        return alias;
    }
}

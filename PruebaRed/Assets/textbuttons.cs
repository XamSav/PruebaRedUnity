using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textbuttons : MonoBehaviour
{
    private GameObject player;
    private Player _player;

    private void Awake()
    {
        player = GameObject.Find("Player");
        _player = player.GetComponent<Player>();
    }
    public void moreCoins()
    {
        _player.setCoin(5);
    }
}

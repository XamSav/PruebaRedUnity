using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ClientIO socketio;
    public Player info;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        socketio.NewPosition(GameObject.Find(info.alias));
    }
}

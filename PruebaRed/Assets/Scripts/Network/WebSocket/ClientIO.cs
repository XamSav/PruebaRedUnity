using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using TMPro;
public class ClientIO : SocketIOComponent
{
	public override void Start()
	{
		base.Start();
		SetupEvents();
	}
	public override void Update()
	{
		base.Update();
	}
	public void SetupEvents()
	{
		On("open", TestOpen);
		On("Mensaje", RecibeMensaje);
		On("jugador", RecibeMensaje);
		On("EnemyNewPosition", EnemyNewPosition);
	}
	public void LookPlayer(TMP_InputField e)
    {
		Emit("player:look", e.text);
    }
	public void RecibeMensaje(SocketIOEvent e)
    {
		Debug.Log(e.data);
    }

	#region Player Movement
	public void NewPosition(GameObject player)
	{
		float x = player.transform.position.x;
		float y = player.transform.position.y;
		string position = x + " " + y;
		Emit("newPosition", position);
	}

	public void EnemyNewPosition(SocketIOEvent e)
    {
		Debug.Log(e.data);
    }
	#endregion

	#region Respuestas Base
	///Servidor Abierto\\\
	public void TestOpen(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
	}

	public void TestClose(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
	}

	/////////// ERROR \\\\\\\\\\\
	public void TestError(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Error recivido: " + e.name + " " + e.data);
	}
	#endregion

}

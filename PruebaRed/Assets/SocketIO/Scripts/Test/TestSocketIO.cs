#region License
/*
 * TestSocketIO.cs
 *
 * The MIT License
 *
 * Copyright (c) 2014 Fabio Panettieri
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
#endregion

using System.Collections;
using System;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;

public class TestSocketIO : MonoBehaviour
{
	private GameObject player;
	private Player _player = new Player("jperez", 0 , 0);
	private SocketIOComponent socket;
	/*[SerializeField]
	private string number = "1";*/
	public void Start() 
	{
		player = GameObject.Find("Player");
		_player = player.GetComponent<Player>();
		//_player = _player.getPlayer();
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();

		socket.On("open", TestOpen);
		socket.On("jugador", LookPlayer);
		socket.On("jugadores", LookPlayers);
		socket.On("server:buycoin", Respuesta);
		socket.On("error", TestError);
		socket.On("close", TestClose);
	}
	public void Respuesta(SocketIOEvent e)
    {
		Debug.Log(e.data);
		_player.toObject(e.data.ToString());
    }

	/// OBTENER JUGADORES \\\
		///ENVIAR EVENTO PARA RECIBIR 1 JUGADOR\\\
	public void GetAPlayer(InputField e)
	{
		socket.Emit("player:look", e.text);
	}
		///ENVIAR EVENTO PARA RECIBIR TODOS LOS JUGADORES\\\
	public void GetPlayers()
	{
		socket.Emit("players:look");
	}
		/// RECIVIENDO JUGADOR \\\
	public void LookPlayer(SocketIOEvent e)
    {
		Debug.Log(e.data);

	}
		/// RECIVIENDO JUGADORES \\\
	public void LookPlayers(SocketIOEvent e)
	{
		Debug.Log(e.data);

	}

	////////////COMPRAR MONEDA//////////////////
	public void buyCoin()
    {
		socket.Emit("player:buycoin", "jperez"/* _player.getAlias()*/);
    }



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
}

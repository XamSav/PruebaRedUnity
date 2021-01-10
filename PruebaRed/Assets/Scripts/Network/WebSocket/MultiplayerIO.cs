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

using System;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;
using UnityEngine.UI;
using Project.Networking;
using TMPro;
namespace Project.Networking
{
	public class MultiplayerIO : SocketIOComponent
	{
		[Header("Network Client")]
		[SerializeField]
		private Transform networkContainer;
		[SerializeField]
		private GameObject playerPrefab;

		public static string ClientAlias { get; private set; }

		private Dictionary<string, NetworkIdentity> serverObjects;
		public string command;
		public override void Start()
		{
			base.Start();
			initialize();
			SetupEvents();
		}
		public override void Update()
		{
			base.Update();
		}
		public void SetupEvents()
		{
			On("open", TestOpen);
			On("loged", (E) => {
				ClientAlias = E.data["alias"].ToString().Trim('"');

				Debug.LogFormat("Tu alias es: {0} ", ClientAlias);
			});
			On("spawn", (e) =>
			{
				string id = e.data["alias"].ToString().Trim('"');

				GameObject go = Instantiate(playerPrefab, networkContainer);
				go.name = string.Format("Player {0}", id);
				NetworkIdentity ni = go.GetComponent<NetworkIdentity>();
				ni.SetControllerID(id);
				ni.SetSocketReference(this);
				serverObjects.Add(id, ni);
			});
			On("disconnected", (e) =>
			{
				string id = e.data["alias"].ToString().Trim('"');

				GameObject go = serverObjects[id].gameObject;
				Destroy(go);
				serverObjects.Remove(id);
			});

			On("updatePosition", (e) =>
			{
				string id = e.data["alias"].ToString().Trim('"');
				float x = e.data["position"]["x"].f;
				float y = e.data["position"]["y"].f;
				float z = e.data["position"]["z"].f;

				NetworkIdentity ni = serverObjects[id];
				ni.transform.position = new Vector3(x, y, z);
			});
		}
		public void login(TMP_InputField data)
		{
			Emit("login", data.text);
		}
		private void initialize()
		{
			serverObjects = new Dictionary<string, NetworkIdentity>();
		}

		public void Respuesta(SocketIOEvent e)
		{
			Debug.Log(e.data);
		}
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
	[SerializeField]
	public class Player{
		public string id;
		public Position position;
	}

	[SerializeField]
	public class Position
    {
		public float x;
		public float y;
		public float z;
    }
}
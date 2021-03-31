using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using TMPro;
public class ClientIO : SocketIOComponent
{
	[SerializeField]
	private GameObject _PlayerPrefab;
	//[SerializeField]
	//private Player _info;
	public static ClientIO _cl;
	public override void Start()
	{
		base.Start();
		SetupEvents();
		if (_cl != null && _cl != this)
		{
			Destroy(this);
		}
		else
		{
			_cl = this;
		}
	}
	public override void Update()
	{
		base.Update();
	}
	public void SetupEvents()
	{
		On("open", TestOpen);
		On("error", TestError);
		On("close", TestClose);
		On("Mensaje", RecibeMensaje);
		On("server:loged", LogedPlayer);
		On("server:playerdeath", DeathC);
		On("server:player", SpawnPlayers);
		On("server:playerreloaded", PlayerRe);
		On("server:youplayer", SpawnMyPlayers);
		On("server:playerdisconnect", PlayerDisc);
		On("server:playerposition", EnemyNewPosition);
	}
	public void Login(TMP_InputField alias)
    {
		if(GameObject.Find(alias.text) == null)
        {
			Emit("player:login", alias.text);
		}
    }
	public void RecibeMensaje(SocketIOEvent e)
    {
		Debug.Log(e.data);
    }
	private void LogedPlayer(SocketIOEvent e)
    {
		GameManager.gm.Rename(e.data["OldAlias"].ToString().Replace("\"", string.Empty), e.data["NewAlias"].ToString().Replace("\"", string.Empty));
	}
	#region My Player
	private void SpawnMyPlayers(SocketIOEvent e)
	{
		string alias = e.data["alias"].ToString().Replace("\"", string.Empty);
		Vector3 positionPl = ChangeStringToInt(e.data["x"].ToString().Replace("\"", string.Empty), e.data["y"].ToString().Replace("\"", string.Empty), e.data["z"].ToString().Replace("\"", string.Empty));
		GameManager.gm.SpawnMe(alias, positionPl);
		//_info.SetAlias(alias);
	}
	public void NewPosition(Vector3 posicion, float Ry, string alias)
	{
		Dictionary<string, string> data = new Dictionary<string, string>(); ;
		data["x"] = posicion.x.ToString();
		data["y"] = posicion.y.ToString();
		data["z"] = posicion.z.ToString();
		data["ry"] = Ry.ToString();
		data["alias"] = alias;
		Emit("player:newposition", new JSONObject(data));
	}
	public void Reload(string alias)
    {
		Emit("player:reload", alias);
    }
	public void PlayerRe(SocketIOEvent e)
    {
		string alias = e.data.ToString().Replace("\"", string.Empty);
		Debug.Log(alias);
		GameManager.gm.UserRepeated(alias);
	}
	#endregion
	#region Players 
	private void SpawnPlayers(SocketIOEvent e)
	{
		string alias = e.data["alias"].ToString().Replace("\"", string.Empty);
		Vector3 positionPl = ChangeStringToInt(e.data["x"].ToString().Replace("\"", string.Empty), e.data["y"].ToString().Replace("\"", string.Empty), e.data["z"].ToString().Replace("\"", string.Empty));
		GameManager.gm.NewEnemy(alias, positionPl);
	}
	public void EnemyNewPosition(SocketIOEvent e)
    {
		float RotacionY = 0;
		Debug.Log("Entro en EnemyNewPosition");
		string alias = e.data["alias"].ToString().Replace("\"", string.Empty);
		if (e.data["ry"] != null)
		{
			RotacionY = ChangeStringToInt(e.data["ry"].ToString().Replace("\"", string.Empty));
        }
		Vector3 newPosition = ChangeStringToInt(e.data["x"].ToString().Replace("\"", string.Empty), e.data["y"].ToString().Replace("\"", string.Empty), e.data["z"].ToString().Replace("\"", string.Empty));
		GameManager.gm.MoveEnemy(alias, newPosition, RotacionY);
	}
	public void PlayerDisc(SocketIOEvent e)
    {
		string alias = e.data["alias"].ToString().Replace("\"", string.Empty);
		GameManager.gm.DeleteEnemy(alias);
	}
	public void Death(string alias)
    {
		Emit("player:death", alias);
    }
	public void DeathC(SocketIOEvent e)
    {
		Debug.Log("DeathConfirmed");
		string alias = e.data.ToString().Replace("\"", string.Empty);
		GameManager.gm.KillEnemy(alias);
	}
	#endregion
	#region Utilidades
	private Vector3 ChangeStringToInt(string xS, string yS, string zS)
	{
		Vector3 posicionN;
		float.TryParse(xS, out posicionN.x);
		float.TryParse(yS, out posicionN.y);
		float.TryParse(zS, out posicionN.z);
		return posicionN;
	}
	private float ChangeStringToInt(string yR)
	{
		float y;
		float.TryParse(yR, out y);
		return y;
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
		GameManager.gm.ConnectionLost();
		Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
	}

	/////////// ERROR \\\\\\\\\\\
	public void TestError(SocketIOEvent e)
	{
		GameManager.gm.ConnectionLost();
		Debug.Log("[SocketIO] Error recivido: " + e.name + " " + e.data);
	}
	#endregion

}

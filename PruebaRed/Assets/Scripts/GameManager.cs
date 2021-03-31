using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{

    ///Canvas ///
    [SerializeField]
    private GameObject _deathCanvas;
    [SerializeField]
    private GameObject _ConnectionCanvas;
    [SerializeField]
    private GameObject _canvasOptions;
    [SerializeField]
    private GameObject camara;
    private TMP_Text aliasUser;
    bool isPaused = false;
    ///Jugadores
    [SerializeField]
    private GameObject _PlayerPrefab;
    [SerializeField]
    private GameObject _localPlayer;
    [SerializeField]
    private List<GameObject> _Enemyplayers = new List<GameObject>();
    public static GameManager gm;
    private void Start()
    {
        if(gm != null && gm != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gm = this;
        }
        _ConnectionCanvas.SetActive(true);
    }
    
    #region Enemy Player
    public void MoveEnemy(string alias, Vector3 EnemyNewPosition, float rotacion)
    {
        GameObject enemy = GameObject.Find(alias);
        enemy.transform.position = new Vector3(EnemyNewPosition.x, EnemyNewPosition.y, EnemyNewPosition.z);
        enemy.transform.eulerAngles = new Vector3(enemy.transform.eulerAngles.x, rotacion, enemy.transform.eulerAngles.z);  //A saber si funciona
    }
    public void NewEnemy(string alias, Vector3 EnemyPosition)
    {
        GameObject User = Instantiate(_PlayerPrefab, EnemyPosition, Quaternion.identity);
        User.name = alias;
        PlayerController otherPlayer = GameObject.Find(alias).GetComponent<PlayerController>();
        otherPlayer.isControllet = false;
        _Enemyplayers.Add(User);
        aliasUser = User.GetComponentInChildren<TextMeshPro>();
        aliasUser.text = alias;
    }
    public void DeleteEnemy(string alias)
    {
        if (alias == _localPlayer.name)
        {
            Destroy(_localPlayer);
        }
        else
        {
            int index = SearchEnemy(alias);
            Destroy(_Enemyplayers[index]);
            _Enemyplayers[index].SetActive(true);
            _Enemyplayers.Remove(_Enemyplayers[index]);
        }
    }
    public void KillEnemy(string alias)
    {
        if(alias == _localPlayer.name)
        {
            camara.SetActive(true);
            _deathCanvas.SetActive(true);
            _localPlayer.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            int index = SearchEnemy(alias);
            _Enemyplayers[index].SetActive(false);
        }
    }
    #endregion
    #region My Player
    public void SpawnMe(string alias, Vector3 Myposition)
    {
        _ConnectionCanvas.SetActive(false);
        camara.SetActive(false);
        _localPlayer = Instantiate(_PlayerPrefab, Myposition, Quaternion.identity);
        _localPlayer.name = alias;
        _localPlayer.GetComponent<Player>().SetAlias(alias);
        PlayerController myPlayer = GameObject.Find(alias).GetComponent<PlayerController>();
        myPlayer.isControllet = true;        
        aliasUser = _localPlayer.GetComponentInChildren<TextMeshPro>();
        aliasUser.text = alias;
    }
    public void Repeat()
    {
        ClientIO._cl.Reload(_localPlayer.name);
        _localPlayer.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
    public void UserRepeated(string alias)
    {
        _Enemyplayers[SearchEnemy(alias)].SetActive(true);
        GameObject.Find(alias).SetActive(true);
    }
    public GameObject getMyPlayer()
    {
        return _localPlayer;
    }
    #endregion
    #region Bases
    public void Rename(string oldAlias, string newAlias)
    {
        GameObject User = GameObject.Find(oldAlias);
        User.name = newAlias;
        User.GetComponent<Player>().SetAlias(newAlias);
        //_info.SetAlias(newAlias);
        aliasUser = User.GetComponentInChildren<TextMeshPro>();
        aliasUser.text = newAlias;
    }
    public void ConnectionLost()
    {
        GameObject[] totalUsers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject theuser in totalUsers)
        {
            Destroy(theuser);
        }
        _ConnectionCanvas.SetActive(true);
        camara.SetActive(true);
    }
    private int SearchEnemy(string alias)
    {
        int index = 0;
        for(int s = 0; s < _Enemyplayers.Count; s++) {
            if(_Enemyplayers[s].name == alias)
            {
                index = s;
            }
        }
        return index;
    }
    public void ResumePause()
    {
        isPaused = !isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused ? true : false;
        _canvasOptions.SetActive(isPaused ? true : false);
        _localPlayer.GetComponent<PlayerController>().isPaused = isPaused;
    }
    #endregion

}

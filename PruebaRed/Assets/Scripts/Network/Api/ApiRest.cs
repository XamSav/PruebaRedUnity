using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
public class ApiRest : MonoBehaviour
{
    private Encriptador m;
    public Player info;
    [SerializeField]
    private TMP_Text resultado;
    [SerializeField]
    private TMP_InputField inputAlias, inputNom, inputCognom, inputPassword;
    public bool localOrHost = false;
    private string url;
    void Start()
    {
        m = GetComponent<Encriptador>();
        if (localOrHost)
        {
            url = "http://samvp.herokuapp.com/";
        }
        else
        {
            url = "http://192.168.0.121:4567/";
        }
    }
    public void StartCreatePlayer()
    {
        StartCoroutine(CreatePlayer());
    }
    public void SendLogin()
    {
        StartCoroutine(Login());
    }
    private void SaveAlias()
    {
        string alias = inputAlias.text;
        PlayerPrefs.SetString("Alias", alias);
        PlayerPrefs.Save();
    }
    public void SetPlayer(UnityWebRequest peticion)
    {
        SaveAlias();
        resultado.text = peticion.downloadHandler.text;
        nextScene();
        //StartCoroutine(WaitSeconds(peticion.downloadHandler.text));
    }
    public void nextScene()
    {
        resultado.text = "Hey";
        Debug.Log("Hey");
        SceneManager.LoadScene("SecondScene");
    }
    
    public void setPrefAlias(string respuesta)
    {
        SaveAlias();
        nextScene();
    }
    IEnumerator WaitSeconds(string peticion)
    {
        resultado.text = peticion;
        yield return new WaitForSeconds(2f);
    }
    IEnumerator Login()
    {
        string passwordEncrypt = callEncripter(inputPassword.text);
        UnityWebRequest peticion = UnityWebRequest.Get(url + "login/" + inputAlias.text + "/" + passwordEncrypt);
        yield return peticion.SendWebRequest();
        if (peticion.isNetworkError)
        {
            Debug.Log(peticion.error);
        }
        else
        {
            if (peticion.downloadHandler.text == "ErrorContra")
            {
                //Tras esto mostrar en UI que el usuario se ha equivocado.
                Debug.Log("Error en contraseña");
                resultado.text = "Contraseña incorrecta";
            }else if (peticion.downloadHandler.text == "ErrorUsuario")
            {
                //Tras esto mostrar en UI que el usuario se ha equivocado.
                Debug.Log("Error ese Usuario no existe!");
                resultado.text = "Usuario no existe";
            }
            else
            {
                SetPlayer(peticion);
            }
        }
    }
    public string callEncripter(string password)
    {
        return m.GetHash(password);
    }
    IEnumerator CreatePlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", inputNom.text);
        form.AddField("surname", inputCognom.text);
        string passwordEncrypt = callEncripter(inputPassword.text);
        form.AddField("password", passwordEncrypt);
        UnityWebRequest peticion = UnityWebRequest.Post(url + "players/" + inputAlias.text, form);

        yield return peticion.SendWebRequest();

        if (peticion.isNetworkError)
        {
            Debug.Log(peticion.error);
        }
        else
        {
            setPrefAlias(peticion.downloadHandler.text);
        }
    }


    #region getPlayer
    public void GetPlayer()
    {
        StartCoroutine(GetText());
    }
    IEnumerator GetText()
    {
        UnityWebRequest peticion = UnityWebRequest.Get(url + "players/" + inputAlias.text);
        yield return peticion.SendWebRequest();

        if (peticion.isNetworkError)
        {
            Debug.Log(peticion.error);
        }
        else
        {
            // Show results as text
            Debug.Log(peticion.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = peticion.downloadHandler.data;
        }
    }
    #endregion
}
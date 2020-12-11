using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
class ApiRest : MonoBehaviour
{
    [SerializeField]
    private InputField inputAlias, inputNom, inputCognom;
    public bool localOrHost = false;
    public int position, score;
    private string url;
    void Start()
    {
        if (localOrHost)
        {
            url = "http://samvp.herokuapp.com/players/";
        }
        else
        {
            url = "http://127.0.0.1:4567/players/";
        }
    }
    public void SendPlayer()
    {
        StartCoroutine(Post());
    }
    public void GetPlayer()
    {
        StartCoroutine(GetText());
    }
    IEnumerator Post()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", inputNom.text);
        form.AddField("surname", inputCognom.text);
        Debug.Log(inputAlias.text);
        if (inputAlias.text != null && inputAlias.text  != "")
        {
            UnityWebRequest peticion = UnityWebRequest.Post(url + inputAlias.text, form);
        

            yield return peticion.SendWebRequest();

            if (peticion.isNetworkError)
            {
                Debug.Log(peticion.error);
            }
            else
            {
                Debug.Log(peticion.downloadHandler.text);
            }
        }
        else
        {
            Debug.Log("Falta el alias!");
        }
    }
    IEnumerator GetText()
    {
        
        if (inputAlias.text != null && inputAlias.text != "") {
            Debug.Log("Entro");
            UnityWebRequest peticion = UnityWebRequest.Get(url + inputAlias.text);
            yield return peticion.Send();

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
        else
        {
            Debug.Log("Falta el alias!");
        }
    }
}

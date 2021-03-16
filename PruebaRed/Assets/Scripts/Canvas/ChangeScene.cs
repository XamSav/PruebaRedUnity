using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static ChangeScene cs;
    public void Awake()
    {
        cs = gameObject.GetComponent<ChangeScene>();
    }
    public void chargeScene()
    {
        SceneManager.LoadScene(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaseCamara : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Dividir la pantalla
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 viewportposition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            string horizontal = "Nada";
            string vertical = "Nada";
            if(viewportposition.x > 0.5f)
            {
                horizontal = "Derecha";
            }
            else
            {
                horizontal = "Izquierda";
            }
            vertical = viewportposition.y > 0.5f ? "Arriba" : "Abajo";
            Debug.LogFormat("Tocaste en {0} y {1}", horizontal, vertical);
        }
        //Mostrar la posicion en Coordenadas globales donde esta la esquina de arriba a la derecha de la camara.
        Debug.Log("La parte superior derecha de la camara (1,1) esta en: " + Camera.main.ViewportToWorldPoint(Vector3.one));
        //Si quieres saber cualquier otra parte de la camara entonces editas el Vector3.one

        //Mover un objeto hacia donde haces Click en el raton
        if (Input.GetMouseButton(1))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition);

        }


        //Acelerometro (Solo en dispositivos moviles), sirve para detectar el movimiento del dispositivo.
        Debug.Log("Acelerometro: " + Input.acceleration);
    }
}

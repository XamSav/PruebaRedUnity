using System.Net.Sockets;
using UnityEngine;
using System;
using System.Text;
public class Networking : MonoBehaviour
{
    
    //https://www.youtube.com/watch?v=zVfpNXHP3Mw
    TcpClient cliente = new TcpClient();
    //Lee y escribe los datos del servidor
    NetworkStream stream;

    //Variables de configuracion
    const string host = "192.168.0.44" ;
    const int puerto = 8080;

    //Cantidad de memoria para leer y escribir datos
    const double memoria = 5e+6;    // Son 5 megabytes pasados a bytes

    //Tiempo limite de conexion
    const int tiempoLimite = 5000;  //Se lee en milisegundos por lo que 5000 son 5s
    

    //Variable donde se almacenaran los datos (Array porque es una gran cantidad de datos que vienen desde el servidor)
    public byte[] data = new byte[(int)memoria]; //Usamos (int) porque memoria es de tipo double y eso da error, por lo que hay que pasar a int

    //Variable que indica cuando el cliente esta escuchando el servidor
    public bool escuchando = false;


    private void Start()
    {               //Metodo flecha, investigar
        conectar((bool res) =>
        {
            if (res == true)
            {
                stream = cliente.GetStream(); // Obtenemos la instancia de donde se esta ejecutando en el cliente
                escuchando = true;            // Con escuchando true aremos que siempre este escuchando al servidor
            }
            else
            {
                Debug.Log("No se conecto");
            }
        });
    }

    public void ejecutarComando(string comando)
    {
        if (comando == "Conectado")
        {
            Debug.Log("Conectado");
        }
    }

    private void Update()
    {
        if (escuchando){
            if (stream.DataAvailable) // Si hay datos nuevos en el servidor
            {
                int dataTam = stream.Read(data, 0, data.Length);    //Tamaño que tiene el dato
                string mensaje = Encoding.UTF8.GetString(data,0,dataTam);
                Debug.Log("Conectado");
                ejecutarComando(mensaje);
            }
        }
    }

    //Conectar hace que la conexion no sea de inmediato, si no que espere x tiempo
    private void conectar(Action<bool> callback ) // Ejecuta codigo despues de que nos conectemos al servidor o intentar
    {
        //ConnectAsync intenta conectarse al servidor atraves de la IP y el puerto, Wait nos dice si a superado el tiempo limite de espera
        bool resultado = cliente.ConnectAsync(host, puerto).Wait(tiempoLimite);
        callback(resultado);    //Pregunta si el servidor se conecto
    }

    private void OnApplicationQuit()
    {
        escuchando = false;
    }
}

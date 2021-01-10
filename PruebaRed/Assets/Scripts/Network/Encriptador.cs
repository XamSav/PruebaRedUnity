using UnityEngine;
using System.Text;
using System.Security.Cryptography;
public class Encriptador : MonoBehaviour
{
    public string GetHash(string texto)
    {
        SHA1 sha1 = SHA1CryptoServiceProvider.Create();
        byte[] textOriginal = ASCIIEncoding.Default.GetBytes(texto);
        byte[] hash = sha1.ComputeHash(textOriginal);
        StringBuilder cadena = new StringBuilder();
        foreach (byte i in hash)
        {
            cadena.AppendFormat("{0:x2}", i);
        }
        return cadena.ToString();
    }
}
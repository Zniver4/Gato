using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class RetrieveInfo : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(InfoPHP());
    }

    IEnumerator InfoPHP()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/gato/gato.php?action=1"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error en la solicitud: {www.error}");
            }
            else
            {
                // Mostrar resultado como texto
                string responseText = www.downloadHandler.text;
                Debug.Log($"Respuesta del servidor: {responseText}");

                // O recuperar los datos binarios si es necesario
                byte[] results = www.downloadHandler.data;
            }
        }
    }
}

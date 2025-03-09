using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class RetriveInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InfoPHP());
    }

    IEnumerator InfoPHP()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/gato/gato.php?action=1");
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }

        else
        {
            //Show Result as Text
            Debug.Log(www.downloadHandler.text);

            //Or Retrive Results as Binary Data
            byte[] results = www.downloadHandler.data;
        }
    }
}

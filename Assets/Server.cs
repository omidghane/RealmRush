using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Server : MonoBehaviour
{
    public Text MessageField;
    private const string serverURL = "http://localhost:8000/";

    public IEnumerator LoginInServer(string username, string pass)
    {
        // string username = "hasan";
        string getDataURL = serverURL + "get_data/";

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", pass);

        using (UnityWebRequest www = UnityWebRequest.Post(getDataURL, form))
        {
            yield return www.SendWebRequest();
            Debug.Log("here2");

            if (www.result != UnityWebRequest.Result.Success)
            {
                // Debug.LogError("Error getting data from server: " + www.error);
                MessageField.text = "user not found boooogh";
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log("Data received from server: " + responseText);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public IEnumerator registerInServer(string username, string pass)
    {
        string addDataURL = serverURL + "register/";

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", pass);

        using (UnityWebRequest www = UnityWebRequest.Post(addDataURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error sending data to server: " + www.error);
            }
            else
            {
                Debug.Log("Data sent to server successfully");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

}

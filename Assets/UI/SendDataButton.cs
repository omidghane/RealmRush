using UnityEngine;
using UnityEngine.UI;

public class SendDataButton : MonoBehaviour
{
    Server serverIntegration; // Drag the GameObject with the ServerIntegration script here in the Inspector
    public InputField username;
    public InputField pass;

    void Awake() {
        serverIntegration = FindObjectOfType<Server>();
    }

    void Start()
    {
       
    }

    public void LoginButtonClicked()
    {
        StartCoroutine(serverIntegration.LoginInServer(username.text, pass.text));
        // serverIntegration.GetDataFromServer(username.text, pass.text);
        // StartCoroutine(serverIntegration.SendDataToServer(username.text,score));
    }

    public void RegisterButtonClicked()
    {
        StartCoroutine(serverIntegration.registerInServer(username.text,pass.text));
    }
}

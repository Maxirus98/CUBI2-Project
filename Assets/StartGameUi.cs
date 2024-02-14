using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class StartGameUi : MonoBehaviour
{
    private Button startButton;

    void Start()
    {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(OnClickStart);

        if(!NetworkManager.Singleton.IsHost)
        {
            startButton.gameObject.SetActive(false);
        }
    }

    private void OnClickStart()
    {
        Debug.Log("OnClickStart");
        NetworkLoader.LoadNetwork(NetworkLoader.Scene.LevelMaxime);
    }
}

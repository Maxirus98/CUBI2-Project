using System.IO;
using Unity.Netcode;
using UnityEngine;

public class ResetHandler : MonoBehaviour
{
    public void ClearData()
    {
        string path = Application.persistentDataPath + "/turrets.json";

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}

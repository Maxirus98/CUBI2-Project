using UnityEngine;
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    public static T Instance
    {
        get { return instance; }
    }

    public static bool IsInitialized
    {
        get { return instance != null; }
    }

    protected virtual void Awake()
    {
        if (IsInitialized)
        {
            Debug.LogError("Il n'y a qu'une seule instance possible.");
            return;
        }

        instance = (T)this;
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}
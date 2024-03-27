using TMPro;
using Unity.Tutorials.Core.Editor;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    private float TimeRemaining = 5;

    
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI label;
    private SpawnScript spawnScript;

    void Start()
    {
        /*print("Start timerscript");
        TimeRemaining = 5;
        spawnScript = GameObject.Find("Spawner").GetComponent<SpawnScript>();
        spawnScript.enabled = false;
        text.transform.gameObject.SetActive(true);
        label.transform.gameObject.SetActive(true);*/
    }

    private void OnEnable() {
        print("Start timerscript");
        TimeRemaining = 5;
        spawnScript = GameObject.Find("Spawner").GetComponent<SpawnScript>();
        spawnScript.enabled = false;
        text.transform.gameObject.SetActive(true);
        label.transform.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (TimeRemaining > 0)
        {
            TimeRemaining -= Time.deltaTime;
            var seconds  = Mathf.FloorToInt(TimeRemaining % 60);
            text.text = $"00:{seconds}";
        } else
        {
            text.transform.gameObject.SetActive(false);
            label.transform.gameObject.SetActive(false);

            // TODO: Start Wave after timer
            spawnScript.enabled = true;
        }
    }
}

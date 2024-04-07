using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    private float TimeRemaining = 5;

    
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image clockImage;
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
        clockImage.transform.gameObject.SetActive(true);
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
            clockImage.transform.gameObject.SetActive(false);

            // TODO: Start Wave after timer
            spawnScript.EnableSpawnScript(this);
        }
    }
}

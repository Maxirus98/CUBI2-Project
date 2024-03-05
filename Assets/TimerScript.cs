using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public float TimeRemaining = 30;

    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
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
            text.transform.parent.gameObject.SetActive(false);

            // TODO: Start Wave after timer

        }
    }
}

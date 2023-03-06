using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    TMP_Text timer;
    float startTime;
    float t;

    private void Awake() {
        timer = GetComponent<TMP_Text>();
    }

    private void Start() {
        startTime = Time.time;
    }

    private void Update() {
        t = Time.time - startTime;

        string minutes = ((int) t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timer.text = minutes + ":" + seconds;
    }

    public void RestartTimer()
    {
        startTime = Time.time;
        t = 0;
    }
}

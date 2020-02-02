using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
    [HideInInspector] public Text MessageText;
    [HideInInspector] public Button Button;
    [HideInInspector] public GameObject Timer;
    [HideInInspector] public Text TimerText;
    [HideInInspector] public Slider TimerSlider;

    // Start is called before the first frame update
    public void Init()
    {
        MessageText = GetComponentInChildren<Text>();
        Button = GetComponentInChildren<Button>();
        
        Timer = transform.Find("Timer").gameObject;
        TimerText = transform.Find("Timer/TimerText").gameObject.GetComponent<Text>();
        TimerSlider = transform.Find("Timer/TimerSlider").gameObject.GetComponent<Slider>();
    }

    public void ToggleTimer(bool flag)
    {
        Timer.SetActive(flag);
    }

    public void UpdateTimer(float TimeLeft, float PlayTime)
    {
        if (PlayTime < 0){
            return;
        }

        TimerSlider.value = (TimeLeft/PlayTime);
        TimerText.text = ((int)TimeLeft).ToString();
    }
    // Update is called once per frame
    public void Update()
    {
        
    }
}

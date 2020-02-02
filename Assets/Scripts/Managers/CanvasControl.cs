using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
    [HideInInspector] public Text MessageText;
    [HideInInspector] public Button Button;

    // Start is called before the first frame update
    public void Init()
    {
        MessageText = GetComponentInChildren<Text>();
        Button = GetComponentInChildren<Button>();
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
}

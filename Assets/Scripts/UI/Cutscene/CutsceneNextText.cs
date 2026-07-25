using UnityEngine;
using TMPro;

public class cutsceneText : MonoBehaviour
{
    private TextMeshProUGUI textUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float hh = Mathf.PingPong(Time.time,1f);

        Color c = textUI.color;
        c.a = hh;

        float ss = 1f + Mathf.Sin(Time.time*3f)*0.1f;
        transform.localScale = new Vector3(ss,ss,1);
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextColor : MonoBehaviour
{
    public Color from = new Color(255, 255, 0);
    public Color to = new Color(0, 255, 0);
    public float switchDuration = 1;

    private Color change;
    private TMP_Text myText;

    private void Start()
    {
        myText = GetComponent<TMP_Text>();
        myText.color = from;
    }

    void Update()
    {
        Color change = Color.Lerp(from, to, Mathf.PingPong(Time.time / switchDuration, 1));
        myText.color = change;
    }
}
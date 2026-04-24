using UnityEngine;
using UnityEngine.UI;

public class LightBlink : MonoBehaviour
{
    public bool isCelebrating = false;

    Image img;
    Color normalColor;

    void Start()
    {
        img = GetComponent<Image>();
        normalColor = img.color;
    }

    void Update()
    {
        if (isCelebrating)
        {
            float t =
              Mathf.PingPong(Time.time * 5f, 1);

            img.color =
                Color.Lerp(
                    Color.yellow,
                    Color.red,
                    t
                );
        }
        else
        {
            img.color = normalColor;
        }
    }
}

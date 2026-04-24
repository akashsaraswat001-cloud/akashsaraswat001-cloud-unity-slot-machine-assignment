using UnityEngine;

public class ArrowBlink : MonoBehaviour
{
    public bool isCelebrating = false;

    Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isCelebrating)
        {
            // Stronger pulsing
            float s =
                1f + Mathf.Sin(Time.time * 12f) * 0.35f;

            transform.localScale =
                originalScale * s;
        }
        else
        {
            transform.localScale =
                originalScale;
        }
    }
}

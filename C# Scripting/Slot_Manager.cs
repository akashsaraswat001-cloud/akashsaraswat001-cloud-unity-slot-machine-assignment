using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SlotManager : MonoBehaviour
{
    public Image reel1, reel2, reel3;
    public Sprite[] symbols;

    public TMP_Text creditsText;
    public TMP_Text resultText;

    public AudioSource spinSound;
    public AudioSource winSound;

    public ArrowBlink[] arrows;
    public LightBlink[] lights;

    public Image leverImage;
    public Sprite leverStraight;
    public Sprite leverBent;

    int credits = 100;
    bool spinning = false;

    void Start()
    {
        if (leverImage != null && leverStraight != null)
            leverImage.sprite = leverStraight;

        SetCelebration(false);
    }

    public void Spin()
    {
        SetCelebration(false);

        if (spinning) return;

        if (credits < 5)
        {
            resultText.text = "NOT ENOUGH CREDITS";
            return;
        }

        if (leverImage != null && leverBent != null)
            leverImage.sprite = leverBent;

        StartCoroutine(SpinRoutine());
    }

    IEnumerator SpinRoutine()
    {
        spinning = true;

        if (spinSound != null)
            spinSound.Play();

        credits -= 5;

        resultText.text = "SPINNING...";

        int a = 0, b = 0, c = 0;

        float totalTime = 5.12f;
        float interval = 0.05f;

        float reel1Stop = 3.05f;
        float reel2Stop = 4.08f;
        float reel3Stop = 5.12f;

        float timer = 0f;

        while (timer < totalTime)
        {
            if (timer < reel1Stop)
            {
                a = Random.Range(0, symbols.Length);
                reel1.sprite = symbols[a];
            }

            if (timer < reel2Stop)
            {
                b = Random.Range(0, symbols.Length);
                reel2.sprite = symbols[b];
            }

            if (timer < reel3Stop)
            {
                c = Random.Range(0, symbols.Length);
                reel3.sprite = symbols[c];
            }

            yield return new WaitForSeconds(interval);

            timer += interval;
        }

        CheckWin(a, b, c);

        creditsText.text =
            credits.ToString();

        if (leverImage != null && leverStraight != null)
            leverImage.sprite = leverStraight;

        spinning = false;
    }

    void CheckWin(int a, int b, int c)
    {
        if (a == b && b == c)
        {
            int payout = 20;

            if (a == 0)
                payout = 100;

            credits += payout;

            if (winSound != null)
                winSound.Play();

            // Force celebration ON
            SetCelebration(true);

            resultText.text =
                "YOU WIN +" + payout;
        }
        else
        {
            resultText.text = "TRY AGAIN";
        }
    }

    void SetCelebration(bool state)
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (arrows[i] != null)
                arrows[i].isCelebrating = state;
        }

        for (int i = 0; i < lights.Length; i++)
        {
            if (lights[i] != null)
                lights[i].isCelebrating = state;
        }
    }
}

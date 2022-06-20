using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPR : MonoBehaviour
{
    // Start is called before the first frame update
    private int secElapsed;
    private int taps = 0;
    bool firstClick = true;

    float bpm = 0f;
    float lastBpm = 0f;
    int neededSec = 0;

    float last, now, diff, sum, entries;

    public Slider mainSlider;

    void Start()
    {
        secElapsed = 0;
        taps = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator BPM()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            secElapsed++;
            neededSec = 60 / secElapsed;
            bpm = taps * neededSec;
            Debug.Log("BPM: " + bpm);
        }

       

    }

    public void Tap()
    {
        /* if (firstClick)
         {
             StartCoroutine(BPM());
             firstClick = false;
             taps++;
         }
         else
         {
             taps++;
         }*/
        lastBpm = bpm;

        if (firstClick)
        {
            last = Time.time;
            firstClick = false;
        }

        now = Time.time;
        diff = now - last;
        last = now;
        sum = sum + diff;
        entries++;

        float avg = sum / entries;
        
        Debug.Log(60f / avg);
        bpm = 60f / avg;

        taps++;
        valueAnimation();



    }

    private void valueAnimation()
    {
        if(taps >= 3)
        {
            LeanTween.value(gameObject, lastBpm, bpm, 0.5f).setOnUpdate((value) =>
            {
                mainSlider.value = value;
            });
        }

    }
}

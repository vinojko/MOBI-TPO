using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CPR : MonoBehaviour
{
    // Start is called before the first frame update
    private int secElapsed;
    private int taps;
    bool firstClick = true;

    int bpm = 0;
    int neededSec = 0;

    float last, now, diff, sum, entries;

    List<float> bpms = new List<float>();
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

        Debug.Log(60 / avg);
       


            
    }
}

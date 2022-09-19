using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTime : MonoBehaviour
{
    // Start is called before the first frame update

    public DialogTrigger dialog1;
    public GameObject Hand;
    void Start()
    {
        StartFirstTime();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartFirstTime()
    {
        dialog1.TriggerDialog();
        StartCoroutine(AnimationUp());
    }

    public IEnumerator AnimationUp()
    {
        yield return new WaitForSeconds(5f);
        FindObjectOfType<DialogFirstTime>().AnimationUIOpenUp();
    }
}

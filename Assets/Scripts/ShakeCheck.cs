using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCheck : MonoBehaviour
{

    public static ShakeCheck instance;

    float accelerometerUpdateInterval = 1.0f / 60.0f;
    float lowPassKernelWidthInSeconds = 1.0f;

    float shakeDetectionThreshold = 2.0f;

    float lowPassFilterFactor;
    Vector3 lowPassValue;

    public bool isShaken;

    void Start()
    {
        isShaken = false;
        instance = this;
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;
    }

    void Update()
    {
        isShaken = false;
        Vector3 acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        Vector3 deltaAcceleration = acceleration - lowPassValue;

        if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold && (GameManager.instance.State == GameState.Odzivnost))
        {

            Vibration.Vibrate(200);
            isShaken=true;

            Debug.Log("Shake event detected at time " + Time.time);
        }
    }
}

using System.Collections;
using UnityEngine;

public class EngineAudio : MonoBehaviour
{
    public AudioSource RunningSound;
    public float RunningMaxVolume;
    public float RunningMaxPitch;
    public AudioSource IdleSound;
    public float IdleMaxVolume;
    public float IdleMaxPitch;
    public AudioSource StartingSound;
    public AudioSource ReverseSound;
    public float ReverseMaxVolume;
    public float ReverseMaxPitch;

    private float speedRatio;
    public float revLimiter;
    public float LimiterSound = 1f;
    public float LimiterFrequency = 3f;
    public float LimiterEngage = 0.8f;
    public bool isEngineRunning = false;

    private bool hasStarted = false;
    private WheelController wheelController;

    void Start()
    {
        wheelController = GetComponent<WheelController>();
        IdleSound.volume = 0;
        RunningSound.volume = 0;
        StartingSound.volume = 0;
        ReverseSound.volume = 0;
    }

    void Update()
    {
        float speedSign=0;
        if (wheelController)
        {
            speedRatio = Mathf.Abs(wheelController.currentAcceleration / wheelController.acceleration);
            speedSign = Mathf.Sign(wheelController.currentAcceleration);
        }
        if (speedRatio > LimiterEngage)
        {
            revLimiter = (Mathf.Sin(Time.time * LimiterFrequency)+1f)*LimiterSound*(speedRatio-LimiterEngage);
        }
        
        

        if (!isEngineRunning && Input.GetKeyDown(KeyCode.W) && !hasStarted)
        {
            StartCoroutine(StartEngine());
        }


        if (isEngineRunning)
        {
            if (!RunningSound.isPlaying)
            {
                RunningSound.Play();
            }

            IdleSound.volume = Mathf.Lerp(0.1f, IdleMaxVolume, speedRatio);

            if (speedSign > 0)
            {
                ReverseSound.volume = 0;
                RunningSound.volume = Mathf.Lerp(0.3f, RunningMaxVolume, speedRatio);
                RunningSound.pitch = Mathf.Lerp(RunningSound.pitch, Mathf.Lerp(0.3f, RunningMaxPitch,Mathf.Abs(speedRatio)) + revLimiter, Time.deltaTime);
            }else{
                RunningSound.volume = 0;
                ReverseSound.volume = Mathf.Lerp(0f, ReverseMaxVolume, speedRatio);
                ReverseSound.pitch = Mathf.Lerp(ReverseSound.pitch, Mathf.Lerp(0.3f, ReverseMaxPitch, Mathf.Abs(speedRatio)) + revLimiter, Time.deltaTime);
            }
            
            
        }
        
    }

    private IEnumerator StartEngine()
    {
        hasStarted = true;
        StartingSound.volume = 1.0f; // Set to desired volume
        StartingSound.Play();
        yield return new WaitForSeconds(2f); // Duration for starting sound
        StartingSound.Stop();
        isEngineRunning = true;
        RunningSound.volume = 0.3f; // Set to desired starting volume
        RunningSound.Play();
    }
}

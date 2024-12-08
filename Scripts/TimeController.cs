using Pinwheel.Jupiter;
using System;
using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static event Action Daytime;
    public static event Action Nighttime;

    [SerializeField] JDayNightCycle dayNightCycle;
    [SerializeField] Material starMat;
    [SerializeField] Material constellationLineMat;


    const float disappearTime = 4.0f;
    const float appearTime = 18.0f;


    bool fadingIn = false;
    bool fadingOut = false;

    void Update()
    {
        if (!fadingOut && Mathf.Abs(disappearTime - dayNightCycle.Time) <= .1f)
        {
            StartCoroutine(FadeStarsOut());
        }
        else if(!fadingIn && Mathf.Abs(appearTime - dayNightCycle.Time) <= .1f)
        {
            StartCoroutine(FadeStarsIn());
        }

    }


    WaitForSeconds waitTime = new WaitForSeconds(.08f);
    IEnumerator FadeStarsIn()
    {
        Nighttime?.Invoke();

        fadingIn = true;

        for(int i = 0; i < 30; i++)
        {
            float opacity = i / 30f;

            starMat.SetFloat("_Opacity", opacity);
            constellationLineMat.SetFloat("_Opacity", opacity);
            yield return waitTime;
        }

        fadingIn = false;

    }

    IEnumerator FadeStarsOut()
    {
        fadingOut = true;

        for (int i = 30; i >= 0; i--)
        {
            float opacity = i / 30f ;

            starMat.SetFloat("_Opacity", opacity);
            constellationLineMat.SetFloat("_Opacity", opacity);
            yield return waitTime;
        }

        fadingOut = false;
        Daytime?.Invoke();


    }


}

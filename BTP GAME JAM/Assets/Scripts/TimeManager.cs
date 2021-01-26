using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private void Update()
    {
        Time.timeScale += 0.5f * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale,0f, 1f);
    }

    public void SlowMotion()
    {
        Time.timeScale = 0.05f;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestApp : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(TimeScaleRoutine());
        StartCoroutine(NotScaledRoutine());
    }

    private IEnumerator TimeScaleRoutine()
    {  
        while (true)
        {
            yield return new WaitForSeconds(5f);
            Debug.Log("MESSAGE FROM TIME SCALED");
        }
    }

    private IEnumerator NotScaledRoutine()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(5f);
            Debug.Log("MESSAGE FROM NOT SCALED");
        }
    }
}

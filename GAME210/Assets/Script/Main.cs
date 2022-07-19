using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private void Start()
    {
        GameManager.ResetVariables();
    }

    //async void myAsyncMethod()
    //{
    //    Debug.Log("async started");
    //    await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(3f));
    //}


    //private IEnumerator MySmoothVelocity(int steps, float speed)
    //{
    //    float inTime = 2;
    //    for(int i = 0; i < steps; i++)
    //    {

    //    }

    //    yield return null;
    //}

    //private IEnumerator MyCoroutine()
    //{
        
    //    for(int i =0; i<4; i++)
    //    {
    //        transform.Translate(Vector3.forward);
    //        Debug.Log("step" + i);
    //        yield return null;
    //    }
    //}
}

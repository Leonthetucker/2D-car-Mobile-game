using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject[] objectsToBeActivate; 
    
    public float Time;

    private int index;

    //Use this for initialization
    void Start()
    {
        Invoke("Timer_", Time);

    }

    public void Timer_()

    {
        for (int i = 0; i < objectsToBeActivate.Length; i++)
        {
            objectsToBeActivate[index].SetActive(false);
        }
    }


}

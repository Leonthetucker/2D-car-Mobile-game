using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject[] Activate;
    public GameObject[] Disactivate;
    public float Time;

    //Use this for initialization
    void Start()
    {
        Invoke("Timer_", Time);

    }

    public void Timer_()

    {
        for (int i = 0; i < Activate.Length; i++)
        {
            Activate[i].SetActive(true);
        }

        for (int i = 0; i < Disactivate.Length; i++)
        {
            Disactivate[i].SetActive(false);
        }

    }


}

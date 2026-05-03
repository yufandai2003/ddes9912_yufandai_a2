using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaa : MonoBehaviour
{
    public GameObject a;
    void Start()
    {
        a.SetActive(false );
        Invoke(nameof(BBB), 3f);
    }
    public void BBB()
    {
        a.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

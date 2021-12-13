using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHoly : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    public GameObject c;
    public GameObject d;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Onclick()
    {
        a.SetActive(true);
        b.SetActive(false);
        c.SetActive(false);
        d.SetActive(false);
    }
}

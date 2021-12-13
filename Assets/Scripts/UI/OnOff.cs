using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    public GameObject bigHao;
    public GameObject bigDoakn;
    public GameObject bigHoly;
    public GameObject bigMusashi;
    public GameObject haow;
    public GameObject Dokanw;
    public GameObject Holyw;
    public GameObject Musashiw;

    GameManager gamem;
    // Start is called before the first frame update
    void Start()
    {
        gamem= GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Onclick()
    {
        if (bigHao.activeSelf)
        {
            if (haow.activeSelf)
            {
                haow.SetActive(false);
                gamem.playerCharacters.RemoveAt(0);
                gamem.restart();
            }
            else if (!haow.activeSelf)
            {
                haow.SetActive(true);
                gamem.addHao();
                gamem.restart();
            }
        }
        else if (bigDoakn.activeSelf)
        {
            if (Dokanw.activeSelf)
            {
                Dokanw.SetActive(false);
                gamem.playerCharacters.RemoveAt(1);
                gamem.restart();
            }
            else if (!Dokanw.activeSelf)
            {
                Dokanw.SetActive(true);
                gamem.addDokan();
                gamem.restart();
            }

        }
        else if (bigHoly.activeSelf)
        {
            if (Holyw.activeSelf)
            {
                Holyw.SetActive(false);
                gamem.playerCharacters.RemoveAt(2);
                gamem.restart();
            }
            else if (!Holyw.activeSelf)
            {
                Holyw.SetActive(true);
                gamem.addHoly();
                gamem.restart();
            }
        }
        else if (bigMusashi.activeSelf)
        {
            if (Musashiw.activeSelf)
            {
                Musashiw.SetActive(false);
                gamem.playerCharacters.RemoveAt(3);
                gamem.restart();
            }
            else if (!Musashiw.activeSelf)
            {
                Musashiw.SetActive(true);
                gamem.addMusashi();
                gamem.restart();
            }
        }
    }
}

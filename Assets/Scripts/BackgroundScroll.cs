using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public GameObject backgroundPrefab;
    private GameObject[] backgrounds;
    private int index;
    private float screenLeft = Camera.main.ScreenToWorldPoint(Vector2.zero).x;
    private float screenRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;

    // Start is called before the first frame update
    void Start()
    {
        float backgroundWidth = backgroundPrefab.GetComponent<Renderer>().bounds.size.x;
        int numberOfBackgrounds = (int)Mathf.Ceil((screenRight - screenLeft) / backgroundWidth);

        //instanitate array of backgrounds objects
        backgrounds = new GameObject[numberOfBackgrounds];
        for (int i = 0; i < numberOfBackgrounds; i++)
        {
            backgrounds[i] = Instantiate(backgroundPrefab);
        }

        //place array of backgrounds
        index = 0;
        for (int i = 0; i < numberOfBackgrounds; i++)
        {
            backgrounds[i].transform.position = new Vector3((backgroundWidth / 2 + backgroundWidth * i), 0, 0);
        }
        //bgRight1 = (background1.transform.position.x + backgroundWidth /2);
        //bgLeft1 = (background1.transform.position.x - backgroundWidth /2);


    }

    // Update is called once per frame
    void Update()
    {
        screenLeft = Camera.main.ScreenToWorldPoint(Vector2.zero).x;
        screenRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;

        if(
        // check if right most point of left most bg < screen left
        //if so teleport (right of right most background) (find index of right most (index-1) if - list.length) (modulus length of list)
        //shift index of left most background

    }
}
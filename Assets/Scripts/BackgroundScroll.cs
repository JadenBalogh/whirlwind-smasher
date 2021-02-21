using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public GameObject backgroundPrefab;
    private GameObject[] backgrounds;
    private int leftIndex;
    private float screenLeft;
    private float screenRight;
    private float backgroundWidth;

    void Start()
    {
        UpdateScreenBounds();

        backgroundWidth = backgroundPrefab.GetComponent<Renderer>().bounds.size.x;
        int numberOfBackgrounds = Mathf.CeilToInt((screenRight - screenLeft) / backgroundWidth) + 1;

        // Instantiate array of backgrounds objects
        backgrounds = new GameObject[numberOfBackgrounds];
        for (int i = 0; i < numberOfBackgrounds; i++)
        {
            Vector3 spawnPos = new Vector3(screenLeft + (backgroundWidth / 2 + backgroundWidth * i), backgroundPrefab.transform.position.y, 0);
            backgrounds[i] = Instantiate(backgroundPrefab, spawnPos, Quaternion.identity, transform);
        }

        leftIndex = 0;
    }

    void Update()
    {
        UpdateScreenBounds();

        if (backgrounds[leftIndex].transform.position.x + backgroundWidth / 2 < screenLeft)
        {
            int rightIndex = (leftIndex + backgrounds.Length - 1) % backgrounds.Length;
            Debug.Log(rightIndex);
            backgrounds[leftIndex].transform.position = new Vector3((backgrounds[rightIndex].transform.position.x + backgroundWidth), 0, 0);
            leftIndex = (leftIndex + 1) % backgrounds.Length;
        }
    }

    private void UpdateScreenBounds()
    {
        screenLeft = Camera.main.ScreenToWorldPoint(Vector2.zero).x;
        screenRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
    }
}
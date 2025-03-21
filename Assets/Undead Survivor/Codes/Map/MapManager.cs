using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] maps;

    private void Start()
    {
        int selectedMapIndex = PlayerPrefs.GetInt("SelectedMapIndex");
        for (int i = 0; i < maps.Length; i++)
        {
            if (i == selectedMapIndex)
            {
                maps[i].SetActive(true);
            }
            else
            {
                maps[i].SetActive(false);
            }
        }
    }
}

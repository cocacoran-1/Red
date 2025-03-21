using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] spawners;
    public int boss_level;//테스트
    public Text test_Text;
    // Start is called before the first frame update
    void Start()
    {
        int selectedMapIndex = PlayerPrefs.GetInt("SelectedMapIndex");
        for (int i = 0; i < spawners.Length; i++)
        {
            if (i == selectedMapIndex)
            {
                spawners[i].SetActive(true);
            }
            else
            {
                spawners[i].SetActive(false);
            }
        }
    }

    public void test_boss()
    {
        test_Text.text = "lv" + boss_level;
    }

}

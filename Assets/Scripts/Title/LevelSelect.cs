using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class LevelSelect : MonoBehaviour
{
    public Text endingText;

    void Start()
    {
        
    }

    private void Awake()
    {
        LoadLevels();
    }

    void LoadLevels()
    {
        int finished = 0;
        for (int i = 1; i <= transform.childCount; i++)
            if (PlayerPrefs.GetInt(string.Format("Lvl{0}_done", i), 0) == 1)
                finished++;

        int next = finished + 3;
        if (finished < transform.childCount)
            endingText.text = "";
        // PlayerPrefs.SetInt("Lvl2_done", 1);
        for (int i = 1; i <= transform.childCount; i++)
        {
            var btn = transform.GetChild(i - 1);
            // btn.transform.localPosition = new Vector2(i * 64, 0);
            btn.GetChild(0).GetComponent<Text>().text = i.ToString();
            btn.GetComponent<Button>().onClick.AddListener(WrapLevelSelect(i));

            int fin = PlayerPrefs.GetInt(string.Format("Lvl{0}_done", i), 0);
            if (i > next)
                btn.GetComponent<Button>().interactable = false;
            else if (fin == 1)
            {
                var col = btn.GetComponent<Button>().colors;
                col.normalColor = Color.HSVToRGB(214 / 360f, 0.5f, 1);
                btn.GetComponent<Button>().colors = col;
            }
        }
    }

    UnityAction WrapLevelSelect(int i)
    {
        return () => { SceneManager.LoadScene(string.Format("Lvl{0}", i)); };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}

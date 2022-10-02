using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OrbManager : MonoBehaviour
{
    public GameObject orbHolder;
    public ParticleSystem particle;
    public Text orbText;

    private int totalOrbs = 999;
    private int currentOrbs = 0;
    private int ended = 0;

    void Start()
    {
        totalOrbs = currentOrbs = 0;
        ended = 0;
        for (int i = 0; i < orbHolder.transform.childCount; i++)
        {
            var orb = orbHolder.transform.GetChild(i).GetComponent<Orb>();
            if (!orb) continue;
            orb.manager = this;
            totalOrbs++;
        }
    }

    void Update()
    {
        orbText.text = string.Format("{0}/{1}", currentOrbs, totalOrbs);
        if (ended == 1) return;
        if (Input.GetKeyDown(KeyCode.R))
        {
            particle.Play();
            Invoke("RestartLevel", 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            particle.Play();
            Invoke("BackToTitle", 0.5f);
        }
    }

    public void GetOrb(Orb orb)
    {
        currentOrbs++;
        if (currentOrbs >= totalOrbs)
        {
            PlayerPrefs.SetInt(string.Format("{0}_done", SceneManager.GetActiveScene().name), 1);
            if (ended == 1) return;
            particle.Play();
            Invoke("BackToTitle", 0.5f);
        }
    }

    private void RestartLevel()
    {
        if (ended == 1) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void BackToTitle()
    {
        if (ended == 1) return;
        SceneManager.LoadScene("Title");
    }
}

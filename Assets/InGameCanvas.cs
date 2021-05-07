using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InGameCanvas : MonoBehaviour
{
    [SerializeField] GameObject panel;
    AudioSource radio;
    PlayerController pc;

    private void Start()
    {
        panel.SetActive(false);
        radio = FindObjectOfType<RadioScript>().GetComponent<AudioSource>();
        pc = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if(!Input.GetKeyDown(KeyCode.Escape)) { return ;}
        panel.SetActive(!panel.activeSelf);
        if (panel.activeSelf)
        {
            Cursor.visible = true;
            Time.timeScale = 0;
            pc.enabled = false;
            radio.Pause();
        }

        if (!panel.activeSelf)
        {
            Cursor.visible = false;
            Time.timeScale = 1;
            pc.enabled = true;
            radio.UnPause();
        }
    }

    public void Resume()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
        pc.enabled = true;
        Cursor.visible = false;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        pc.enabled = true;
        Cursor.visible = false;
        SceneManager.LoadScene("ItamarLevel");
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}

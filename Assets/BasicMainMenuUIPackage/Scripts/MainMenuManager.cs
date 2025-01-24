using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class MainMenuManager : MonoBehaviour
{
    [SerializeField] List<GameObject> menuList;

    [SerializeField] AudioMixer audioMixer;

    private void Awake() {

        if(PlayerPrefs.HasKey("Username"))
        {
            OpenMenu(menuList[1]);
        }
        else{
            OpenMenu(menuList[0]);
        }

        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", -20f);
        }

        
        
    }

    private void Start() {
        audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
    }

    public void OpenMenu(GameObject menu)
    {
        foreach (var item in menuList)
        {
            item.SetActive(false);
        }
        menu.SetActive(true);
    }

    void OpenMenu(GameObject[] menus)
    {
        foreach (var item in menuList)
        {
            item.SetActive(false);
        }

        foreach (var item in menus)
        {
            item.SetActive(true);
        }        
    }

    
}

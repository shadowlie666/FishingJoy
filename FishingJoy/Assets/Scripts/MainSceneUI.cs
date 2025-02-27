using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneUI : MonoBehaviour
{
    public Toggle muteToggle;
    public GameObject settingPanel;

    private void Start()
    {
        muteToggle.isOn = !AudioManager.Instance.IsMute;
        
    }

    public void SwitchMute(bool isOn)
    {
        AudioManager.Instance.SwitchMuteState(isOn);
    }

    public void OnBackButtonDown()
    {
        PlayerPrefs.SetInt("gold", GameController.Instance.gold);
        PlayerPrefs.SetInt("lv", GameController.Instance.lv);
        PlayerPrefs.SetFloat("scd", GameController.Instance.smallTimer);
        PlayerPrefs.SetFloat("bcd", GameController.Instance.bigTimer);
        PlayerPrefs.SetInt("exp", GameController.Instance.exp);
        int temp = (AudioManager.Instance.IsMute == false) ? 0 : 1;
        PlayerPrefs.SetInt("mute",temp);
        SceneManager.LoadScene(0);
    }

    public void OnSettingButtonDown()
    {
        settingPanel.SetActive(true);
    }

    public void OnCloseButtonDown()
    {
        settingPanel.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderUI : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    public TextMeshProUGUI bgmText;
    public TextMeshProUGUI sfxText;

    public Button resetButton;

    private void Start()
    {
        float savedBGM = PlayerPrefs.GetFloat("BGMVolume");
        float savedSFX = PlayerPrefs.GetFloat("SFXVolume");

        bgmSlider.value = savedBGM;
        sfxSlider.value = savedSFX;

        AudioManager.Instance.SetBGMVolume(savedBGM);
        AudioManager.Instance.SetSFXVolume(savedSFX);

        bgmText.text = $"{(int)(savedBGM * 100)}%";
        sfxText.text = $"{(int)(savedSFX * 100)}%";

        bgmSlider.onValueChanged.AddListener(OnBGMChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXChanged);

        resetButton.onClick.AddListener(ResetVolume);
    }

    private void OnBGMChanged(float value)
    {
        AudioManager.Instance.SetBGMVolume(value);
        PlayerPrefs.SetFloat("BGMVolume", value);
        bgmText.text = $"{(int)(value * 100)}%";
    }

    private void OnSFXChanged(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
        PlayerPrefs.SetFloat("SFXVolume", value);
        sfxText.text = $"{(int)(value * 100)}%";
    }

    private void ResetVolume()
    {
        AudioManager.Instance.ResetVolume();
        bgmText.text = $"{(int)(1 * 100)}%";
        sfxText.text = $"{(int)(1 * 100)}%";
    }
}

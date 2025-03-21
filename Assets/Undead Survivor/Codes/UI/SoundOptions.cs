using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundOptions : MonoBehaviour
{
    // 오디오 믹서
    public AudioMixer audioMixer;
    public Image[] bgmSegments;
    public Image[] sfxSegments;
    // 슬라이더
    public Slider bgmSlider;
    public Slider sfxSlider;

    // 볼륨 조절
    public void SetBgmVolume()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(bgmSlider.value) * 20);
        UpdateBgmSegments();
    }

    public void SetSFXVolume()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxSlider.value) * 20);
        UpdateSfxSegments();
    }
    private void UpdateBgmSegments()
    {
        float bgmPercentage = bgmSlider.value;
        for (int i = 0; i < bgmSegments.Length; i++)
        {
            if (bgmPercentage >= (i + 1f) / bgmSegments.Length)
            {
                bgmSegments[i].enabled = true;
            }
            else
            {
                bgmSegments[i].enabled = false;
            }
        }
    }
    private void UpdateSfxSegments()
    {
        float sfxPercentage = sfxSlider.value;
        for (int i = 0; i < sfxSegments.Length; i++)
        {
            if (sfxPercentage >= (i + 1f) / sfxSegments.Length)
            {
                sfxSegments[i].enabled = true;
            }
            else
            {
                sfxSegments[i].enabled = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSound : MonoBehaviour
{
    public AudioSource bgm; // 배경음
    public AudioSource[] sfxPlayer;  // 효과음
    public AudioClip[] sfxClip;
    public enum Sfx { ButtonClick };
    int sfxCursor;
    private void Start()
    {
        bgm.Play();
    }
    public void TurnOnAudio()
    {
        bgm.mute = true;
    }
    public void TurnOffAudio()
    {
        bgm.mute = false;
    }
    public void SfxPlay(Sfx type)
    {
        switch (type)
        {
            case Sfx.ButtonClick:
                sfxPlayer[sfxCursor].clip = sfxClip[0];
                break;
        }
        // 타입에 맞는 소리 실행
        sfxPlayer[sfxCursor].Play();
        sfxCursor = (sfxCursor + 1) % sfxPlayer.Length;

    }

    public void ButtonClickSound()
    {
        SfxPlay(Sfx.ButtonClick);
    }
}

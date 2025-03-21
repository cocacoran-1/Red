using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSound : MonoBehaviour
{
    [Header("Sound")]

    public AudioSource bgmPlayer; // 배경음
    public AudioSource[] sfxPlayer;  // 효과음
    public AudioClip[] sfxClip;
    public enum Sfx { LevelUp, PickUp, FlipPage, Over, playerHit, ButtonClick };
    int sfxCursor;

    private void Start()
    {
        bgmPlayer.Play(); // BGM 실행
    }

    public void SfxPlay(Sfx type)
    {
        switch (type)
        {
            // 레벨얼 소리
            case Sfx.LevelUp:
                sfxPlayer[sfxCursor].clip = sfxClip[0];
                break;
            // 아이템 획득
            case Sfx.PickUp:
                sfxPlayer[sfxCursor].clip = sfxClip[1];
                break;
            // 책 넘기기
            case Sfx.FlipPage:
                sfxPlayer[sfxCursor].clip = sfxClip[2];
                break;
            // 게임 오버
            case Sfx.Over:
                sfxPlayer[sfxCursor].clip = sfxClip[3];
                break;
            // 플레이어 피격
            case Sfx.playerHit:
                sfxPlayer[sfxCursor].clip = sfxClip[4];
                break;
            case Sfx.ButtonClick:
                sfxPlayer[sfxCursor].clip = sfxClip[5];
                break;
        }

        // 타입에 맞는 소리 실행
        sfxPlayer[sfxCursor].Play();
        sfxCursor = (sfxCursor + 1) % sfxPlayer.Length;
    }
}

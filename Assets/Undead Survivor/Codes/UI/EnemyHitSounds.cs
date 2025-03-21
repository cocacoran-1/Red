using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitSounds : MonoBehaviour
{
    public AudioSource[] sfxPlayer;  // 효과음
    public AudioClip sfxClip;
    int sfxCursor;
    float maxVolume = 0.3f; // 최대 볼륨을 설정합니다.

    public void EnemyHitPlay()
    {
        sfxPlayer[sfxCursor].clip = sfxClip;

        sfxPlayer[sfxCursor].volume = Mathf.Min(sfxPlayer[sfxCursor].volume, maxVolume); // 최대 볼륨과 현재 볼륨 중 작은 값을 선택합니다.

        sfxPlayer[sfxCursor].Play();

        sfxCursor = (sfxCursor + 1) % sfxPlayer.Length;
    }
}

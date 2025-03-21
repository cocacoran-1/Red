using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSounds : MonoBehaviour
{

    public AudioSource[] sfxPlayer;  // 효과음
    public AudioClip[] sfxClip;
    public enum Sfx { Ray, FireCharging, FirePillar, Tornado, Stomp, StoneFall, Wave }
    int sfxCursor;

    public void SkillSoundPlay(Sfx type)
    {
        float volume = 0.3f;

        switch (type)
        {
            case Sfx.Ray:
                sfxPlayer[sfxCursor].clip = sfxClip[0];
                break;
            case Sfx.FireCharging:
                sfxPlayer[sfxCursor].clip = sfxClip[1];
                break;
            case Sfx.FirePillar:
                sfxPlayer[sfxCursor].clip = sfxClip[2];
                break;
            case Sfx.Tornado:
                sfxPlayer[sfxCursor].clip = sfxClip[3];
                break;
            case Sfx.Stomp:
                sfxPlayer[sfxCursor].clip = sfxClip[4];
                volume = 0.8f;
                break;
            case Sfx.StoneFall:
                sfxPlayer[sfxCursor].clip = sfxClip[5];
                break;
            case Sfx.Wave:
                sfxPlayer[sfxCursor].clip = sfxClip[6];
                break;
        }

        sfxPlayer[sfxCursor].volume = volume;

        // 타입에 맞는 소리 실행
        sfxPlayer[sfxCursor].Play();
        sfxCursor = (sfxCursor + 1) % sfxPlayer.Length;
    }
}

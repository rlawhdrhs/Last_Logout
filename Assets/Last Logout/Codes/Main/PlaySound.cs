using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource;  // 사운드 플레이어
    public AudioClip moveSound;      // 이동 효과음

    void Start()
    {
        // AudioSource 가져오기
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }
    public void Play()
    {
        if (moveSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(moveSound);  // 효과음 1회 재생
        }
    }
    public void StopSound()
    {
        if (audioSource.isPlaying)  // 소리가 재생 중이면
        {
            audioSource.Stop();  // 즉시 멈춤
        }
    }
}

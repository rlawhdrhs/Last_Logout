using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource;  // ���� �÷��̾�
    public AudioClip moveSound;      // �̵� ȿ����

    void Start()
    {
        // AudioSource ��������
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }
    public void Play()
    {
        if (moveSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(moveSound);  // ȿ���� 1ȸ ���
        }
    }
    public void StopSound()
    {
        if (audioSource.isPlaying)  // �Ҹ��� ��� ���̸�
        {
            audioSource.Stop();  // ��� ����
        }
    }
}

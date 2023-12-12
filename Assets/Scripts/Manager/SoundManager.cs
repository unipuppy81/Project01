using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();

                if (instance == null)
                {
                    GameObject go = new GameObject("SoundManager");
                    instance = go.AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            // AudioManager가 싱글톤으로 설정되었을 때 필요한 초기화 작업을 여기에 추가할 수 있습니다.
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // 현재 재생 중인 사운드를 중지합니다.
    public void StopSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    // 다른 스크립트에서 이 메서드를 사용하여 사운드를 재생할 수 있습니다.
    public void PlaySound(AudioClip soundClip)
    {
        if (audioSource != null && soundClip != null)
        {
            StopSound(); // 현재 재생 중인 사운드를 중지하고 새로운 사운드를 재생합니다.
            audioSource.PlayOneShot(soundClip);
        }
    }
}
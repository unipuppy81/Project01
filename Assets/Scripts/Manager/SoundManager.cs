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

            // AudioManager�� �̱������� �����Ǿ��� �� �ʿ��� �ʱ�ȭ �۾��� ���⿡ �߰��� �� �ֽ��ϴ�.
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // ���� ��� ���� ���带 �����մϴ�.
    public void StopSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    // �ٸ� ��ũ��Ʈ���� �� �޼��带 ����Ͽ� ���带 ����� �� �ֽ��ϴ�.
    public void PlaySound(AudioClip soundClip)
    {
        if (audioSource != null && soundClip != null)
        {
            StopSound(); // ���� ��� ���� ���带 �����ϰ� ���ο� ���带 ����մϴ�.
            audioSource.PlayOneShot(soundClip);
        }
    }
}
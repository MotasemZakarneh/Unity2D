using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance = null;

    [SerializeField] AudioSource sfxTemplate = null;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static void PlaySFX(AudioClip clip)
    {
        SFXManager manager = null;
        if(instance == null)
        {
            manager = FindObjectOfType<SFXManager>();
        }
        else
        {
            manager = instance;
        }

        AudioSource sfx = UnityEngine.Object.Instantiate(manager.sfxTemplate);
        sfx.clip = clip;
        Destroy(sfx.gameObject, 2);
        DontDestroyOnLoad(sfx);
        sfx.Play();
        sfx.transform.position = Camera.main.transform.position;
    }
}
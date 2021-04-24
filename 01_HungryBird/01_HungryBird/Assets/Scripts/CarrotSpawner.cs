using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotSpawner : MonoBehaviour
{
    public GameObject carrot = null;
    public GameObject spawnPoint = null;

    public float coolDownMin = 0.5f;
    public float coolDownMax = 1.5f;

    public AudioClip carrotSpawnAudio = null;

    float counter;

    
    void Start()
    {
        counter = GetRandomCoolDown();
    }
    void Update()
    {
        float newCounterValue = counter - Time.deltaTime;
        counter = newCounterValue;
        
        if (counter < 0)
        {
            counter = GetRandomCoolDown();
            GameObject clonedCarrot = Instantiate(carrot);
            clonedCarrot.transform.position = spawnPoint.transform.position;
            SFXManager.PlaySFX(carrotSpawnAudio);
        }

    }

    private float GetRandomCoolDown()
    {
        float rnd = Random.Range(coolDownMin,coolDownMax);

        return rnd;
    }
}

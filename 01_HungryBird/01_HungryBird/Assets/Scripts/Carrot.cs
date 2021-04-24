using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    public GameObject pickUpEffect = null;
    public AudioClip clip = null;

    GameController controllerComponent = null;

    void Start()
    {
        controllerComponent = FindObjectOfType<GameController>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Player p = other.GetComponent<Player>();
        if (p != null)
        {
            PickUp();
        }
    }
    public void PickUp()
    {
        GameObject clonedObj = Instantiate(pickUpEffect);
        clonedObj.transform.position = transform.position;
        SFXManager.PlaySFX(clip);

        controllerComponent.OnCarrotPicked();
        
        Destroy(gameObject);
    }
    public void DestroyCarrot()
    {
        controllerComponent.OnCarrotDestroyed();

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.5f;
    public float boostedSpeed = 7;
    public Rigidbody2D rb2d = null;

    public float totalEnergy = 100.0f;
    public float energyLossSpeeed = 15;//Per One Second, Every One Second
    public float energyGainSpeed = 10;

    [Header("Read Only")]
    public Vector2 dir = Vector2.zero;
    public Vector2 vel = Vector2.zero;
    public float currentEnergy = 0;
    public bool isShiftPressed = false;

    void Start()
    {
        dir.x = 0;
        dir.y = 0;
        vel.x = 0;
        vel.y = 0;
        currentEnergy = totalEnergy;
        //print(dir);
    }
    void Update()
    {
        UpdateCurrentEnergy();
        UpdateMovementDirection();

        vel = dir * GetUsableSpeed();
        rb2d.velocity = vel;
    }

    float GetUsableSpeed()
    {
        float usableSpeed = speed;
        
        if (currentEnergy > 0 && isShiftPressed)
        {
            usableSpeed = boostedSpeed;
        }

        return usableSpeed;
    }
    void UpdateCurrentEnergy()
    {
        isShiftPressed = Input.GetKey(KeyCode.LeftShift);
        if (isShiftPressed)
        {
            currentEnergy = currentEnergy - Time.deltaTime * energyLossSpeeed;
            if (currentEnergy <= 0)
                currentEnergy = 0;
        }
        else
        {
            currentEnergy = currentEnergy + Time.deltaTime * energyGainSpeed;
            if (currentEnergy >= totalEnergy)
                currentEnergy = totalEnergy;
        }
    }
    void UpdateMovementDirection()
    {
        dir.y = 0;
        dir.x = 0;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            dir.x = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir.x = -1;
        }
    }
}

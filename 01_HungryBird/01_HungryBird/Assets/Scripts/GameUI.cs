using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RTLTMPro;

public class GameUI : MonoBehaviour
{
    public RTLTextMeshPro scoreTextComponent = null;
    public RTLTextMeshPro energyTextComponent = null;
    public GameObject winScreen = null;
    public GameObject loseScreen = null;

    Player playerComponent;
    GameController gameControllerComponent;

    void Start()
    {
        scoreTextComponent.text = "النقاط";
        playerComponent = FindObjectOfType<Player>();
        gameControllerComponent = FindObjectOfType<GameController>();

        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }
    void Update()
    {
        int cs = gameControllerComponent.currentPickedCarrots;
        string textPart = "النقاط ";
        scoreTextComponent.text = textPart + cs;

        int ce = (int)playerComponent.currentEnergy;
        string energyTextPart = "الطاقة : ";
        energyTextComponent.text = energyTextPart + ce;
    }

    public void ActivateWinScreen()
    {
        winScreen.SetActive(true);
    }
    public void ActivateLoseScreen()
    {
        loseScreen.SetActive(true);
    }
}
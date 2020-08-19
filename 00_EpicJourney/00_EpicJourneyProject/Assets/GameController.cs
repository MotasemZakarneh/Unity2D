using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    enum StoryState { Riding, Thirsty, AttackedByEnemies, Bed, OnGround, Knife, EnemiesAreBack, FightThem, NoCamel }
    StoryState currentStoryState;


    void Start()
    {
        currentStoryState = StoryState.Riding;
        Writer.CreateWriter("Red");
        Writer.SetTitle("T");
    }

    void Update()
    {
        if (currentStoryState == StoryState.Riding)
        {
            Writer.Write("00");
            Writer.AddDecision("I");
            if (Input.GetKeyDown(KeyCode.I))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.Thirsty;
            }
        }
        else if(currentStoryState == StoryState.Thirsty)
        {
            print("01");
            Writer.AddDecision("A");
            if (Input.GetKeyDown(KeyCode.A))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.AttackedByEnemies;
            }
        }
        else if(currentStoryState == StoryState.AttackedByEnemies)
        {
            print("02");
            Writer.AddDecision("B");
            Writer.AddDecision("K");
            Writer.AddDecision("O");
            if (Input.GetKeyDown(KeyCode.B))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.Bed;
            }
            else if(Input.GetKeyDown(KeyCode.K))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.Knife;
            }
            else if(Input.GetKeyDown(KeyCode.O))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.OnGround;
            }
        }
        else if(currentStoryState == StoryState.Bed)
        {
            print("03_B");
            Writer.AddDecision("A");
            if (Input.GetKeyDown(KeyCode.A))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.AttackedByEnemies;
            }
        }
        else if(currentStoryState == StoryState.Knife)
        {
            print("03_K");
            Writer.AddDecision("E");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.EnemiesAreBack;
            }
        }
        else if(currentStoryState == StoryState.OnGround)
        {
            print("03_O");
            Writer.AddDecision("A");
            if (Input.GetKeyDown(KeyCode.A))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.AttackedByEnemies;
            }
        }
        else if(currentStoryState == StoryState.EnemiesAreBack)
        {
            print("04");
            Writer.AddDecision("F");
            if (Input.GetKeyDown(KeyCode.F))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.FightThem;
            }
        }
        else if(currentStoryState == StoryState.FightThem)
        {
            print("05");
            Writer.AddDecision("N");
            if (Input.GetKeyDown(KeyCode.N))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.NoCamel;
            }
        }
        else if(currentStoryState == StoryState.NoCamel)
        {
            print("06");
            Writer.AddDecision("R");
            if (Input.GetKeyDown(KeyCode.R))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.Riding;
            }
        }

    }



}

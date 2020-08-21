using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    enum StoryState { Riding, Thirsty, AttackedByEnemies,StuckInRoom, Bed, OnGround, Knife, EnemiesAreBack, FightThem }
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
            Writer.Write("01");
            Writer.AddDecision("A");
            if (Input.GetKeyDown(KeyCode.A))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.AttackedByEnemies;
            }
        }
        else if(currentStoryState == StoryState.AttackedByEnemies)
        {
            Writer.Write("02");
            Writer.AddDecision("S");
            if(Input.GetKeyDown(KeyCode.S))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.StuckInRoom;
            }
        }
        else if(currentStoryState == StoryState.StuckInRoom)
        {
            Writer.Write("03");
            Writer.AddDecision("B");
            Writer.AddDecision("K");
            Writer.AddDecision("O");
            if (Input.GetKeyDown(KeyCode.B))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.Bed;
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.Knife;
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.OnGround;
            }
        }
        else if(currentStoryState == StoryState.Bed)
        {
            Writer.Write("04_A");
            Writer.AddDecision("A");
            if (Input.GetKeyDown(KeyCode.A))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.StuckInRoom;
            }
        }
        else if(currentStoryState == StoryState.Knife)
        {
            Writer.Write("04_B");
            Writer.AddDecision("E");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.EnemiesAreBack;
            }
        }
        else if(currentStoryState == StoryState.OnGround)
        {
            Writer.Write("04_C");
            Writer.AddDecision("A");
            if (Input.GetKeyDown(KeyCode.A))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.StuckInRoom;
            }
        }
        else if(currentStoryState == StoryState.EnemiesAreBack)
        {
            Writer.Write("05");
            Writer.AddDecision("F");
            if (Input.GetKeyDown(KeyCode.F))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.FightThem;
            }
        }
        else if(currentStoryState == StoryState.FightThem)
        {
            Writer.Write("06");
            Writer.AddDecision("R");
            if (Input.GetKeyDown(KeyCode.R))
            {
                Writer.ClearDecisions();
                currentStoryState = StoryState.Riding;
            }
        }
    }


}

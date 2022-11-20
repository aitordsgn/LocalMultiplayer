using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLapCounter : MonoBehaviour
{
    int passedCheckPointNumber = 0;
    float timeAtLastPassedCheckPoint = 0;


    int numberOfPassedCheckpoints = 0;

    int lapsCompleted = 0;
    const int lapstocomplete = 2;

    bool RaceIsCompleted = false;
    int carPosition = 0;

    public event Action<CarLapCounter> OnPassCheckpoint;
    public event Action<CarLapCounter> OnFinish;

    public void SetCarPosition(int position)
    {
        carPosition = position;
    }

    public int GetNumberOfCheckpointPass()
    {
        return numberOfPassedCheckpoints;
    }

    public float GetTimeAtLastCheckpoint()
    {
        return timeAtLastPassedCheckPoint;
    }
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("Checkpoint"))
        {
            if (RaceIsCompleted)
                return;
            Checkpoint checkpoint = collider2D.GetComponent<Checkpoint>();
            //Make sure that the car is passing the checkpoints in the correct order
            if(passedCheckPointNumber +1 == checkpoint.CheckpointOrder)
            {
                passedCheckPointNumber = checkpoint.CheckpointOrder;

                numberOfPassedCheckpoints++;
                //Store the time at the checkpoint
                timeAtLastPassedCheckPoint = Time.time;

                if(checkpoint.isFinishLine)
                {
                    passedCheckPointNumber = 0;
                    lapsCompleted++;

                    if(lapsCompleted >= lapstocomplete)
                    {
                        RaceIsCompleted = true;
                        OnFinish?.Invoke(this);
                    }
                }
                OnPassCheckpoint?.Invoke(this);
            }
        }
    }
}

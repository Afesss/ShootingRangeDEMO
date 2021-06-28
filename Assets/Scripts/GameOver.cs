using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class GameOver : IInitializable
{
    public event System.Action OnGameOver;
    private int shapeDestroyedAmount { get; set; }
    [Inject] private ShapeSettings shapeSettings;
    public void Initialize()
    {
        shapeDestroyedAmount = 0;
    }

    private void OnGameOverInvoke()
    {
        OnGameOver?.Invoke();
    }
    public void IncrementShapeDestoyedAmount()
    {
        shapeDestroyedAmount++;
        Debug.Log(shapeDestroyedAmount);
        if(shapeDestroyedAmount == shapeSettings.maxShapeNumberOnLevel)
        {
            OnGameOver();
        }
    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class ShapeController : MonoBehaviour
{
    public event System.Action<ShapeController> OnShapeDestroy;

    private ShotController shotController;
    private ShapeSettings shapeSettings;
    private GameOver gameOver;
    [Inject]
    private void Construct(ShotController shotController, ShapeSettings shapeSettings, GameOver gameOver)
    {
        this.shotController = shotController;
        this.shapeSettings = shapeSettings;
        this.gameOver = gameOver;
    }

    public List<Shape> shapeList = new List<Shape>();
    public void SubscribeOnShapeTriggers()
    {
        for(int i = 0;i < shapeList.Count; i++)
        {
            shapeList[i].OnTriggered += ShapeController_OnTriggered;
        }
    }

    private void ShapeController_OnTriggered()
    {
        gameOver.IncrementShapeDestoyedAmount();
        OnShapeDestroy?.Invoke(this);
        for (int i = 0; i < shapeList.Count; i++)
        {
            shapeList[i].OnTriggered -= ShapeController_OnTriggered;
            shapeList[i].AddImpulse(shotController.CurrentProjectile.transform.position);
        }
    }
}

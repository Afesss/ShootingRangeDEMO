using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class ShapeGenerator : MonoBehaviour
{
    [Inject] private ShapeSettings shapeSettings;

    private GameObject[] shapePool;
    private void Start()
    {
        int number;
        shapePool = new GameObject[shapeSettings.maxShapeNumberOnLevel];
        for(int i = 0; i < shapeSettings.maxShapeNumberOnLevel; i++)
        {
            number = Random.Range(1, 4);
            int randomNum;
            switch (number)
            {
                case 1:

                    shapePool[i] = CreateBox(Random.Range(5, 7), Random.Range(8, 9), Random.Range(5, 7), Random.Range(0.2f, 0.3f));
                    break;
                case 2:
                    randomNum = Random.Range(5, 9);
                    shapePool[i] = CreatePyramid(randomNum, randomNum, Random.Range(0.2f, 0.3f));
                    break;
                case 3:
                    randomNum = Random.Range(5, 9);
                    shapePool[i] = CreateDiamond(randomNum, randomNum, Random.Range(0.2f, 0.3f));
                    break;
                default: return;
            }
            shapePool[i].SetActive(false);
        }
        for (int i = 0; i < shapeSettings.startShapeNumber; i++)
        {
            ActivateTargets();
        }
    }

    private GameObject GetFreeShape()
    {
        for (int i = 0; i < shapePool.Length; i++)
        {
            if (!shapePool[i].activeSelf)
            {
                return shapePool[i];
            }
        }
        return null;
    }
    private void ActivateTargets()
    {
        GameObject obj = GetFreeShape();

        if(obj == null)
        {
            return;
        }

        Vector3 pos = GeneretePosition();

        while (!CheckDistance(pos))
        {
            pos = GeneretePosition();
        }

        obj.transform.localPosition = pos;
        obj.SetActive(true);
    }
    private Vector3 GeneretePosition()
    {
        Vector3 pos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized
            * shapeSettings.distanceToPlayer;
        pos += Vector3.up * 2;
        return pos;
    }
    private bool CheckDistance(Vector3 pos)
    {
        for (int i = 0; i < shapePool.Length; i++)
        {
            if (shapePool[i].activeSelf)
            {
                float distance = (pos - shapePool[i].transform.position).magnitude;
                if(distance < shapeSettings.minDistanceBetweenShapes)
                {
                    return false;
                }
            }
        }
        return true;
    }
    private void Controller_OnShapeDestroy(ShapeController shapeController)
    {
        shapeController.OnShapeDestroy -= Controller_OnShapeDestroy;
        ActivateTargets();
    }
    private GameObject CreateBox(int xBorder, int yBorder, int zBorder, float scale)
    {
        GameObject box = Instantiate(shapeSettings.parent, transform);
        var controller = box.GetComponent<ShapeController>();
        GameObject prefab = Random.Range(0, 2) == 0 ? shapeSettings.cubePrefab : shapeSettings.spherePrefab;
        for (int x = 0; x < xBorder; x++)
        {
            for (int z = 0; z < zBorder; z++)
            {
                for (int y = 0; y < yBorder; y++)
                {
                    controller.shapeList.Add(Instantiate(prefab, new Vector3(x, y, z), 
                        Quaternion.identity, box.transform).GetComponent<Shape>());
                }
            }
        }
        box.name = "pyramid";
        box.transform.localPosition = Vector3.zero;
        box.transform.localScale = Vector3.one * scale;
        controller.SubscribeOnShapeTriggers();
        controller.OnShapeDestroy += Controller_OnShapeDestroy;
        return box;
    }
    private GameObject CreatePyramid(int xBorder, int zBorder, float scale)
    {
        GameObject pyramid = Instantiate(shapeSettings.parent, transform);
        var controller = pyramid.GetComponent<ShapeController>();
        GameObject prefab = Random.Range(0, 2) == 0 ? shapeSettings.cubePrefab : shapeSettings.spherePrefab;
        
        int increment = 0;
        int yBorder = xBorder % 2 == 0? xBorder / 2 : xBorder / 2 + 1;

        for (int y = 0; y < yBorder; y++)
        {
            for (int x = 0 + increment; x < xBorder; x++)
            {
                for (int z = 0 + increment; z < zBorder; z++)
                {
                    controller.shapeList.Add(Instantiate(prefab, new Vector3(x, y, z), 
                        Quaternion.identity, pyramid.transform).GetComponent<Shape>());
                }
            }
            xBorder--;
            zBorder--;
            increment++;
        }
        pyramid.name = "pyramid";
        pyramid.transform.localPosition = Vector3.zero;
        pyramid.transform.localScale = Vector3.one * scale;
        controller.SubscribeOnShapeTriggers();
        controller.OnShapeDestroy += Controller_OnShapeDestroy;
        return pyramid;
    }

    

    private GameObject CreateDiamond(int xBorder, int zBorder, float scale)
    {
        GameObject diamond = Instantiate(shapeSettings.parent, transform);
        var controller = diamond.GetComponent<ShapeController>();
        GameObject prefab = Random.Range(0, 2) == 0 ? shapeSettings.cubePrefab : shapeSettings.spherePrefab;

        int increment = 0;
        int yBorder = xBorder;
        for (int y = 0; y < yBorder; y++)
        {
            for (int x = 0 + increment; x < xBorder; x++)
            {
                for (int z = 0 + increment; z < zBorder; z++)
                {
                    controller.shapeList.Add(Instantiate(prefab, new Vector3(x, y, z),
                        Quaternion.identity, diamond.transform).GetComponent<Shape>());
                    controller.shapeList.Add(Instantiate(prefab, new Vector3(x, -y, z),
                        Quaternion.identity, diamond.transform).GetComponent<Shape>());
                }
            }
            
            if(y % 2 == 0)
            {
                increment++;
                xBorder--;
                zBorder--;
            }
        }
        diamond.name = "diamond";
        diamond.transform.localPosition = Vector3.zero;
        diamond.transform.localScale = Vector3.one * scale;
        controller.SubscribeOnShapeTriggers();
        controller.OnShapeDestroy += Controller_OnShapeDestroy;
        return diamond;
    }    
}

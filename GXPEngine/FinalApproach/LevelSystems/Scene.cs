using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
using GXPEngine.FinalApproach;

public class Scene:GameObject
{
    public Action OnSceneOpen=null;

    List<GameObject> tempGameObjects = new List<GameObject>();

    public Scene() {
    }


    public void AddTemporaryObject(GameObject obj) {
        tempGameObjects.Add(obj);
        AddChild(obj);
    }

    public void RemoveTemporaryObjects() {
        foreach (var obj in tempGameObjects) {
            obj.LateRemove();
        }
        tempGameObjects = new List<GameObject>();
    }
}


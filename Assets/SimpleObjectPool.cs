using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectPool : MonoBehaviour {

    public GameObject prefab;

    Stack<GameObject> inActiveInstances = new Stack<GameObject>();

    public GameObject GetObject()
    {
        GameObject spawnedGameObject;

        if (inActiveInstances.Count > 0)
            spawnedGameObject = inActiveInstances.Pop();
        else
        {
            spawnedGameObject = Instantiate(prefab);
            PooledObject pooledObject = spawnedGameObject.AddComponent<PooledObject>();
            pooledObject.pool = this;
        }
        spawnedGameObject.transform.SetParent(null);
        spawnedGameObject.SetActive(true);

        return spawnedGameObject;
    }
    
    public void ReturnObject(GameObject toReturn)
    {
        PooledObject pooledObject = toReturn.GetComponent<PooledObject>();
        pooledObject.pool = this;

        if (pooledObject != null && pooledObject.pool == this)
        {
            toReturn.transform.SetParent(transform);
            toReturn.SetActive(false);
            inActiveInstances.Push(toReturn);
        }
        else
        {
            print(toReturn.name + "You are not my son");
            Destroy(toReturn);
        }
    }

}

public class PooledObject : MonoBehaviour // 컴포넌트로 들어가려면 MonoBehaviour를 상속 받아야만 함!
{
    public SimpleObjectPool pool;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NUT_Pooler : MonoBehaviour
{
    public GameObject Hex;

    int numHex = 100;
    public List<MaterialVariable> hexMaterials;
    public List<GameObject> hexPool;




    void PoolNuts()
    {
        for(int i=0;i<numHex;++i)
        {
            GameObject HexInst = Instantiate(Hex);
            Hex.SetActive(false);
            hexPool.Add(Hex);
        }
    }

    public void SpawnHex(int poolID, Transform aTransform)
    {
        
    }
}

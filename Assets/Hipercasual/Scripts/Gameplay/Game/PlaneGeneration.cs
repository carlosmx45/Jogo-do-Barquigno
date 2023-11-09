using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGeneration : MonoBehaviour
{

    public GameObject plane;
    public GameObject player;

    [Range(1, 8)]
    [SerializeField] int radius = 3;

    [Range(2, 20)]
    [SerializeField] int planeOffset = 10;

    private Vector3 startPos = Vector3.zero;

    private int XPlayerMove => (int)(player.transform.position.x - startPos.x);
    private int ZPlayerMove => (int)(player.transform.position.x - startPos.z);

    private int XPlayerLocation => (int)Mathf.Floor(player.transform.position.x / planeOffset) * planeOffset;
    private int ZPlayerLocation => (int)Mathf.Floor(player.transform.position.z / planeOffset) * planeOffset;

    private Hashtable tilePlane = new Hashtable();

    void Update()
    {
        if(startPos == Vector3.zero)
        {
            for(int x = -radius; x < radius; x++)
            {
                for(int z = -radius; z < radius; z++)
                {
                    Vector3 pos = new Vector3((x * planeOffset + XPlayerLocation),
                    0,
                    (z *planeOffset + ZPlayerLocation));

                    if(!tilePlane.Contains(pos))
                    {
                        GameObject _plane = Instantiate(plane, pos, Quaternion.identity);
                        tilePlane.Add(pos, _plane);
                    }
                }
            }

            if(hasPlayerMoved())
            {
                for (int x = -radius; x < radius; x++)
                {
                    for (int z = -radius; z < radius; z++)
                    {
                        Vector3 pos = new Vector3((x * planeOffset + XPlayerLocation),
                        0,
                        (z * planeOffset + ZPlayerLocation));

                        if (!tilePlane.Contains(pos))
                        {
                            GameObject _plane = Instantiate(plane, pos, Quaternion.identity);
                            tilePlane.Add(pos, _plane);
                        }
                    }
                }
            }
        }
    }

    bool hasPlayerMoved()
    {
        
        if(Mathf.Abs(XPlayerMove) >= planeOffset || Mathf.Abs(ZPlayerMove) >= planeOffset)
        {
            return true;
        }
        return false;
    }
}

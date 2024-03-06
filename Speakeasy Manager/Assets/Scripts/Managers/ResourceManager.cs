using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance { get; private set; }
    public List<ResourceData> resources;
    private void Awake()
    {
        // if there is no instance of this object set it to this object
        // else destroy this object
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
}

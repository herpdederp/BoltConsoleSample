using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : Bolt.EntityEventListener<IBuildingState>
{

    public override void Attached()
    {
        state.SetTransforms(state.Transform, transform);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

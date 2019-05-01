using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : Bolt.EntityEventListener<IBuildingState>
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
        if (entity.IsOwner)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(transform.forward * Time.deltaTime * 10f);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(transform.forward * Time.deltaTime * -10f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(transform.right * Time.deltaTime * -10f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(transform.right * Time.deltaTime * 10f);
            }

        }
    }
}

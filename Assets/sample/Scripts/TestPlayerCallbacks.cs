using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerCallbacks : Bolt.GlobalEventListener
{
    public override void SceneLoadLocalDone(string scene)
    {
        if (HeadlessServerManager.IsHeadlessMode() == false)
            BoltNetwork.Instantiate(BoltPrefabs.Player);
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

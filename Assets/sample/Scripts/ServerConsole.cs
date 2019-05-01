using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Bolt;

public class ServerConsole : Bolt.GlobalEventListener
{
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN

    Windows.ConsoleWindow console = new Windows.ConsoleWindow();
    Windows.ConsoleInput input = new Windows.ConsoleInput();

    string strInput;
#pragma warning disable 0649
    public bool consoleInNonHeadlessMode;
#pragma warning restore 0649
    bool quitting;

    public override void BoltShutdownBegin(AddCallback registerDoneCallback)
    {
        if (quitting == true)
            registerDoneCallback(Test0);
    }


    void Test0()
    {
        System.Console.ForegroundColor = ConsoleColor.White;
        Application.Quit();
    }

    //
    // Create console window, register callbacks
    //
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (HeadlessServerManager.IsHeadlessMode() == false && consoleInNonHeadlessMode == false)
        {

        }
        else
        {
            console.Initialize();
            console.SetTitle("Bolt Server");

            input.OnInputText += OnInputText;

            Application.RegisterLogCallback(HandleLog);

            Debug.Log("Console Started");
        }
    }

    //
    // Text has been entered into the console
    // Run it as a console command
    //
    void OnInputText(string obj)
    {
        Debug.Log("ECHO " + obj);
        //ConsoleSystem.Run(obj, true);

        if (obj == "Quit")
        {
            if (BoltNetwork.IsRunning)
            {
                quitting = true;              
                BoltNetwork.ShutdownImmediate();

            }
        }
        else if (obj == "Cube")
        {
            BoltNetwork.Instantiate(BoltPrefabs.Cube, 
                new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10)), 
                Quaternion.identity);          
        }
    }

    //
    // Debug.Log* callback
    //
    void HandleLog(string message, string stackTrace, LogType type)
    {
        if (type == LogType.Warning)
            System.Console.ForegroundColor = ConsoleColor.Yellow;
        else if (type == LogType.Error)
            System.Console.ForegroundColor = ConsoleColor.Red;
        else
            System.Console.ForegroundColor = ConsoleColor.White;

        // We're half way through typing something, so clear this line ..
        if (Console.CursorLeft != 0)
            input.ClearLine();

        System.Console.WriteLine(message);

        // If we were typing something re-add it.
        input.RedrawInputLine();
    }

    //
    // Update the input every frame
    // This gets new key input and calls the OnInputText callback
    //
    void Update()
    {
        input.Update();
    }

    //
    // It's important to call console.ShutDown in OnDestroy
    // because compiling will error out in the editor if you don't
    // because we redirected output. This sets it back to normal.
    //
    void OnDestroy()
    {
        console.Shutdown();
    }

#endif
}

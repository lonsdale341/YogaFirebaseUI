﻿using System.Collections;
using System.Collections.Generic;
using Firebase.Unity.Editor;
using UnityEngine;
using Google;
using Facebook.Unity;

public class MainManager : MonoBehaviour
{
    [HideInInspector]
    public States.StateManager stateManager = new States.StateManager();
    //Google auth variables
    private string webClientId = "118238300341-41n408f3k1j67moamh4kmqrhssa9f07g.apps.googleusercontent.com";
    private void Awake()
    {
        // Setup for Google Sign In

        CommonData.configuration = new GoogleSignInConfiguration
        {
            WebClientId = webClientId,
            RequestIdToken = true
        };
    }
    // Use this for initialization
    void Start()
    {
        InitializeAndStart();
    }

    // Update is called once per frame
    void Update()
    {
        stateManager.Update();
    }
    void FixedUpdate()
    {
        stateManager.FixedUpdate();
    }
    void InitializeAndStart()
    {
        InitializeFirebaseAndStart();


    }
    // When the app starts, check to make sure that we have
    // the required dependencies to use Firebase, and if not,
    // add them if possible.
    void InitializeFirebaseAndStart()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init();
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
        Firebase.DependencyStatus dependencyStatus = Firebase.FirebaseApp.CheckDependencies();

        if (dependencyStatus != Firebase.DependencyStatus.Available)
        {
            Firebase.FirebaseApp.FixDependenciesAsync().ContinueWith(task =>
            {
                dependencyStatus = Firebase.FirebaseApp.CheckDependencies();
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    //InitializeFirebaseComponents();

                    StartGame();
                }
                else
                {
                    Debug.LogError(
                        "Could not resolve all Firebase dependencies: " + dependencyStatus);
                    Application.Quit();
                }
            });
        }
        else
        {
            //InitializeFirebaseComponents();
            StartGame();
        }
    }

    void StartGame()
    {


        CommonData.prefabs = FindObjectOfType<PrefabList>();
        CommonData.canvasHolder = GameObject.Find("CanvasHolder");
        CommonData.mainManager = this;

        Firebase.AppOptions ops = new Firebase.AppOptions();
        CommonData.app = Firebase.FirebaseApp.Create(ops);
        CommonData.app.SetEditorDatabaseUrl("https://yogaolga.firebaseio.com/");

        Screen.orientation = ScreenOrientation.Portrait;




        //#if UNITY_EDITOR
        //      UserData temp = new UserData();
        //      temp.nameUser = StringConstants.DefaultUserName;
        //      temp.id = StringConstants.DefaultUserId;
        //      
        //     
        //      CommonData.currentUser = new DBStruct<UserData>(
        //        CommonData.DBUserTablePath + StringConstants.DefaultUserId, CommonData.app);
        //      CommonData.currentUser.Initialize(temp);
        //      //stateManager.SwapState(new States.SelectModeState());
        //#else
        stateManager.PushState(new States.Startup());
        //#endif
    }
}

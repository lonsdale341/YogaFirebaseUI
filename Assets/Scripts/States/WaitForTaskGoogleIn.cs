using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google;
using System.Threading.Tasks;
using System;

using Facebook.Unity;

namespace States
{
    class WaitForTaskGoogleIn : BaseState
    {

        protected bool isComplete = false;
        string message;
        bool useDots;
        System.Threading.Tasks.Task<GoogleSignInUser> task;
        string imageUrl;
        Menus.LoadedLabelGUI menuComponent;

        public WaitForTaskGoogleIn()
        {

            
            
        }

        public override void Initialize()
        {
            
            menuComponent = SpawnUI<Menus.LoadedLabelGUI>(StringConstants.PrefabLoadedeLabel);
            GoogleSignIn.Configuration = CommonData.configuration;
            GoogleSignIn.Configuration.UseGameSignIn = false;
            GoogleSignIn.Configuration.RequestIdToken = true;
            this.task = GoogleSignIn.DefaultInstance.SignIn();
            GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnGoogleAuthenticationFinished);
        }
        //Handle when Google Sign In successfully
        void OnGoogleAuthenticationFinished(Task<GoogleSignInUser> task)
        {

            if (task.IsFaulted)
            {
                using (IEnumerator<System.Exception> enumerator =
                        task.Exception.InnerExceptions.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        GoogleSignIn.SignInException error =
                                (GoogleSignIn.SignInException)enumerator.Current;
                        message = "Got Error: " + error.Status + " " + error.Message;
                        manager.PopState();
                    }
                    else
                    {
                        message = "Got Unexpected Exception?!?" + task.Exception;
                        manager.PopState();
                    }
                }
            }
            else if (task.IsCanceled)
            {
                message = "Canceled";
                manager.PopState();
            }
            else
            {
                this.task = task;
                imageUrl = task.Result.ImageUrl.AbsoluteUrlOrEmptyString();
                //imageUrl = task.Result.ImageUrl.Ab
                manager.PopState();
            }
        }
        // Called once per frame when the state is active.
        public override void Update()
        {
           // if (task.IsCompleted)
           // {
           //     manager.PopState();
           // }
            
        }

        

        public override StateExitValue Cleanup()
        {
            DestroyUI();
            return new StateExitValue(typeof(WaitForTaskGoogleIn), new ResultsGoogleIn(task,message, this.imageUrl));
        }

        // Class for encapsulating the results of the database load, as
        // well as information about whether the load was successful
        // or not.
        public class ResultsGoogleIn
        {
            public System.Threading.Tasks.Task <GoogleSignInUser> task;
            public string message;
            public string iamgeURL;
            public ResultsGoogleIn(System.Threading.Tasks.Task<GoogleSignInUser> task,string message, string url)
            {
                this.iamgeURL = url;
                this.task = task;
                this.message = message;
            }
        }
    }

}
 

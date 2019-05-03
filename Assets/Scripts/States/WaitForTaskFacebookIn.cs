using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google;
using System.Threading.Tasks;
using System;
using Facebook.Unity;

namespace States
{
    class WaitForTaskFacebookIn : BaseState
    {

       
        string accessToken;
        string imageUrl;



        Menus.LoadedLabelGUI menuComponent;

        public WaitForTaskFacebookIn()
        {
            


        }

        public override void Initialize()
        {
            FB.LogInWithReadPermissions(new List<string>() { "public_profile", "email" }, OnFacebookAuthenticationFinished);
        }
        void OnFacebookAuthenticationFinished(IResult result)
        {
            if (FB.IsLoggedIn)
            {

                //Firebase Auth
                accessToken = AccessToken.CurrentAccessToken.TokenString;
                manager.PopState();
            }
            else
            {
                imageUrl = String.Format("https://graph.facebook.com/{0}/picture?type=large&width=100&height=100", AccessToken.CurrentAccessToken.UserId);
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
            return new StateExitValue(typeof(WaitForTaskFacebookIn), new ResultsFacebookIn(accessToken,this.imageUrl));
        }

        // Class for encapsulating the results of the database load, as
        // well as information about whether the load was successful
        // or not.
        public class ResultsFacebookIn
        {
           
            public string accessToken;
            public string iamgeURL;
            public ResultsFacebookIn(string message,string url)
            {
                this.iamgeURL = url;
                this.accessToken = message;
            }
        }
    }

}
 

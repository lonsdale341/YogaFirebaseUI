using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
namespace States
{

    class Startup : BaseState
    {

        Firebase.Auth.FirebaseAuth auth;
        public const string DefaultDesktopID = "XYZZY";
        public override void Initialize()
        {
            Debug.Log("Init StartUp");
            //Time.timeScale = 0.0f;
            // When the game starts up, it needs to either download the user data
            // or create a new profile.
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            if (auth.CurrentUser != null)
            {
                
                Debug.Log("Init StartUp ID=" + auth.CurrentUser.UserId);
                Debug.Log("Init StartUp DisplayName= " + auth.CurrentUser.DisplayName);
            }
            else
            {
                Debug.Log("Init StartUp auth.CurrentUser=null");
            }
            
            // On mobile, we go through the full auth system, to request a user id.
            // If we need to sign in, do that.  Otherwise, if we know who we are,
            // so fetch the user data.
            if (true||auth.CurrentUser == null)
            {
                manager.PushState(new RegistrationForm());
            }
            else
            {
                manager.PushState(new PanelSigned(auth.CurrentUser.DisplayName, auth.CurrentUser.PhotoUrl.AbsoluteUrlOrEmptyString()));
                //manager.PushState(new States.FetchUserData(auth.CurrentUser.UserId));
            }
        }
       public override void Resume(StateExitValue results)
       {
           Debug.Log("resume StartUp");
           if (results.sourceState == typeof(RegistrationForm) ||
               results.sourceState == typeof(SignIn))
           {
               Debug.Log("resume1 StartUp");
               // We just got back from trying to sign in anonymously.
               // Did it work?
               if (auth.CurrentUser != null)
               {
                    Debug.Log("resume1 StartUp");
                    Debug.Log("resume1 StartUp DisplayName= "+auth.CurrentUser.DisplayName);
                    // Yes!  Continue!
                    //manager.PushState(new ErrorMassage("Message", "SignIn."));
                    // manager.PushState(new FetchUserData(auth.CurrentUser.UserId));
                    RegistrationForm.RegistrationFormResult taskResults = results.data as RegistrationForm.RegistrationFormResult;
                    manager.PushState(new PanelSigned(auth.CurrentUser.DisplayName, taskResults.imageURL));
                }
               else
               {
                    // Nope.  Couldn't sign in.
                    manager.PushState(new ErrorMassage("Error Message", "Couldn't sign in."));
                    CommonData.isNotSignedIn = true;
                   //manager.SwapState(new RegistrationForm());
               }
           }
          // else if (results.sourceState == typeof(FetchUserData))
          // {
          //     
          //     
          //         Debug.Log("resume5 StartUp");
          //         manager.SwapState(new States.SelectModeState());
          //
          //         if (CommonData.currentUser == null)
          //         {
          //             Debug.Log("resume4 StartUp");
          //             //  If we can't fetch data, tell the user.
          //             manager.PushState(new BasicDialog(StringConstants.CouldNotFetchUserData));
          //         }
          //     
          // }
          // else
          // {
          //     throw new System.Exception("Returned from unknown state: " + results.sourceState);
          // }
       }
    }


}

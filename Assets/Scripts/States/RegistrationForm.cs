using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace States
{
    class RegistrationForm : BaseState
    {

        Firebase.Auth.FirebaseAuth auth;
        Menus.RegistartionFormGUI dialogComponent;
        bool canceled = false;

        public override void Initialize()
        {
            Debug.Log("Init CreateAccount");
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            dialogComponent = SpawnUI<Menus.RegistartionFormGUI>(StringConstants.PrefabsRegistrationFormtMenu);
        }

         public override void Resume(StateExitValue results)
         {
             Debug.Log("Resume CreateAccount");
             ShowUI();
             if (results != null)
             {
                 Debug.Log("Resume 1 CreateAccount");
                 if (results.sourceState == typeof(WaitForTask))
                 {
                     Debug.Log("Resume 2 CreateAccount");
                     WaitForTask.Results taskResults = results.data as WaitForTask.Results;
                     if (taskResults.task.IsFaulted)
                     {
                         Debug.Log("Resume 3 CreateAccount");
                        manager.PushState(new ErrorMassage("Error Message", "Could not create account."));
                        // manager.PushState(new BasicDialog("Could not create account."));
                    }
                     else
                     {
                         Debug.Log("Resume 4 CreateAccount");
                         if (!string.IsNullOrEmpty(dialogComponent.SignUpUsername.text))
                         {
                             Debug.Log("Resume 5 CreateAccount");
                             Firebase.Auth.UserProfile profile =
                               new Firebase.Auth.UserProfile();
                             profile.DisplayName = dialogComponent.SignUpUsername.text;
                             // We are fine with this happening in the background,
                             // so just return to the previous state after triggering the update.
                             auth.CurrentUser.UpdateUserProfileAsync(profile);
                         }
                         manager.PopState();
                     }
                 }
             }
         }

        public override void Suspend()
        {
           // HideUI();
        }

          public override StateExitValue Cleanup()
          {
              DestroyUI();
              return new StateExitValue(typeof(RegistrationForm), null);
          }
        public static bool validateEmail(string email)
        {
            if (email != null)
                return Regex.IsMatch(email, Utils.MatchEmailPattern);
            else
                return false;
        }
        public override void HandleUIEvent(GameObject source, object eventData)
        {
            if (source == dialogComponent.SignIN_Go.gameObject)
            {
               // manager.PopState();
                 manager.SwapState(new SignIn());
            }
             else if (source == dialogComponent.Registration.gameObject)
             {
                Debug.Log(dialogComponent.SignUpEmail.text);
                Debug.Log(dialogComponent.SignUpPwd.text);
                Debug.Log(dialogComponent.SignUpUsername.text);
                if (string.IsNullOrEmpty(dialogComponent.SignUpEmail.text.Trim()) || string.IsNullOrEmpty(dialogComponent.SignUpPwd.text.Trim()) || string.IsNullOrEmpty(dialogComponent.SignUpUsername.text.Trim()))
                {

                    manager.PushState(new ErrorMassage("Error Message", "Email or Pwd or Username is null"));
                    

                   
                }
                else if (!validateEmail(dialogComponent.SignUpEmail.text.Trim()))
                {
                    manager.PushState(new ErrorMassage("Error Message", "Email is incorrect"));




                }
                else if (dialogComponent.SignUpPwd.text.Trim().Length < 6)
                {
                    manager.PushState(new ErrorMassage("Error Message", "Password must be 6 or more characters."));




                }
                else
                {
                    manager.PushState(new WaitForTask(auth.CreateUserWithEmailAndPasswordAsync(
                   dialogComponent.SignUpEmail.text, dialogComponent.SignUpPwd.text)));
                }
                
            }
        }
    }
    public class RegistrationFormResult
    {
        public bool Canceled = false;
        public RegistrationFormResult(bool canceled)
        {
            this.Canceled = canceled;
        }

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace States
{
    class SignIn : BaseState
    {

        Firebase.Auth.FirebaseAuth auth;
        Menus.SignInGUI dialogComponent;
        bool canceled = false;

        public override void Initialize()
        {
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            dialogComponent = SpawnUI<Menus.SignInGUI>(StringConstants.PrefabsSignInMenu);
        }

        public override void Suspend()
        {
           // HideUI();
        }

        public override void Resume(StateExitValue results)
        {
            ShowUI();
            if (results != null)
            {
                if (results.sourceState == typeof(WaitForTask))
                {
                    WaitForTask.Results taskResults = results.data as WaitForTask.Results;
                    if (taskResults.task.IsFaulted)
                    {
                        manager.PushState(new ErrorMassage("Error Message", "Sign in failed."));
                    }
                    else
                    {
                        manager.PopState();
                    }
                }
            }
        }

        public override StateExitValue Cleanup()
        {
            DestroyUI();
            return new StateExitValue(typeof(SignIn), null);
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
            if (source == dialogComponent.Register_Go.gameObject)
            {
                
                manager.SwapState(new RegistrationForm());
            }

            else if (source == dialogComponent.Login.gameObject)
            {
                if (string.IsNullOrEmpty(dialogComponent.SignUpEmail.text.Trim()) || string.IsNullOrEmpty(dialogComponent.SignUpPwd.text.Trim()) )
                {

                    manager.PushState(new ErrorMassage("Error Message", "Email or Pwd  is null"));



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
            // else if (source == dialogComponent.ContinueButton.gameObject)
            // {
            //     manager.PushState(new WaitForTask(auth.SignInWithEmailAndPasswordAsync(
            //         dialogComponent.Email.text, dialogComponent.Password.text)));
            // }
            // else if (source == dialogComponent.ForgotPasswordButton.gameObject)
            // {
            //     manager.PushState(new PasswordReset());
            // }
        }
    }
    public class SignInResult
    {
        public bool Canceled = false;
        public SignInResult(bool canceled)
        {
            this.Canceled = canceled;
        }

    }

}
 
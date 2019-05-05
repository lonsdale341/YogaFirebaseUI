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
        string imageURL;
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
                if (results.sourceState == typeof(WaitForTaskGoogleIn))
                {
                    Debug.Log("Resume 2 CreateAccount");
                    WaitForTaskGoogleIn.ResultsGoogleIn taskResults = results.data as WaitForTaskGoogleIn.ResultsGoogleIn;
                    if (taskResults.task.IsFaulted)
                    {

                        manager.PushState(new ErrorMassage("Error Message", taskResults.message));
                        // manager.PushState(new BasicDialog("Could not create account."));
                    }
                    else if (taskResults.task.IsCanceled)
                    {
                        manager.PushState(new ErrorMassage("Error Message", taskResults.message));
                    }
                    else
                    {
                        Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(taskResults.task.Result.IdToken, null);
                        auth.SignInWithCredentialAsync(credential).ContinueWith(t =>
                        {
                            if (t.IsCanceled)
                            {
                                manager.PushState(new ErrorMassage("Error Message", "SignInWithCredentialAsync was canceled."));

                                return;
                            }
                            if (t.IsFaulted)
                            {
                                manager.PushState(new ErrorMassage("Error Message", "SignInWithCredentialAsync encountered an error: " + t.Exception));

                                return;
                            }
                            imageURL = taskResults.iamgeURL;
                            manager.PopState();

                        });

                    }
                }
                if (results.sourceState == typeof(WaitForTaskFacebookIn))
                {
                    Debug.Log("Resume 2 CreateAccount");
                    WaitForTaskFacebookIn.ResultsFacebookIn taskResults = results.data as WaitForTaskFacebookIn.ResultsFacebookIn;
                    if (string.IsNullOrEmpty(taskResults.accessToken))
                    {
                        manager.PushState(new ErrorMassage("Error Message", "User cancelled login"));

                    }
                    else
                    {
                        Firebase.Auth.Credential credential = Firebase.Auth.FacebookAuthProvider.GetCredential(taskResults.accessToken);
                        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
                        {
                            if (task.IsCanceled)
                            {
                                manager.PushState(new ErrorMassage("Error Message", "SignInWithCredentialAsync was canceled."));

                                return;
                            }
                            if (task.IsFaulted)
                            {
                                manager.PushState(new ErrorMassage("Error Message", "SignInWithCredentialAsync encountered an error: " + task.Exception));

                                return;
                            }
                            imageURL = taskResults.iamgeURL;
                            manager.PopState();
                        });
                    }

                }
                if (results.sourceState == typeof(WaitForTaskTwitterIn))
                {
                    Debug.Log("Resume 2 CreateAccount");
                    WaitForTaskTwitterIn.ResultsTwitterIn taskResults = results.data as WaitForTaskTwitterIn.ResultsTwitterIn;
                    if (taskResults.session == null)
                    {
                        manager.PushState(new ErrorMassage("Error Message", taskResults.message));

                    }
                    else
                    {
                        Firebase.Auth.Credential credential = Firebase.Auth.TwitterAuthProvider.GetCredential(taskResults.session.authToken.token, taskResults.session.authToken.secret);
                        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
                        {
                            if (task.IsCanceled)
                            {
                                manager.PushState(new ErrorMassage("Error Message", "Please install Twitter application"));
                                return;
                            }
                            if (task.IsFaulted)
                            {
                                manager.PushState(new ErrorMassage("Error Message", "Please install Twitter application"));
                                return;
                            }
                            imageURL = taskResults.iamgeURL;
                            manager.PopState();

                        });
                    }

                }
            }
        }

        public override StateExitValue Cleanup()
        {
            DestroyUI();
            return new StateExitValue(typeof(SignIn), new SignInResult(canceled,this.imageURL));
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
                    manager.SwapState(new PanelTrainingCatalogList());
                   // manager.PushState(new WaitForTask(auth.SignInWithEmailAndPasswordAsync(
                   //     dialogComponent.SignUpEmail.text, dialogComponent.SignUpPwd.text)));
                }
            }
            else if (source == dialogComponent.GoogleUP.gameObject)
            {

                // manager.PushState(new WaitForTaskGoogleIn());
                manager.SwapState(new PanelTrainingCatalogList());
            }
            else if (source == dialogComponent.FacebookUP.gameObject)
            {

                //manager.PushState(new WaitForTaskFacebookIn());
                manager.SwapState(new PanelTrainingCatalogList());
            }
            else if (source == dialogComponent.TwitterUP.gameObject)
            {

                //manager.PushState(new WaitForTaskTwitterIn());
                manager.SwapState(new PanelTrainingCatalogList());
            }
            // else if (source == dialogComponent.Login.gameObject)
            // {
            //     manager.PushState(new WaitForTask(auth.SignInWithEmailAndPasswordAsync(
            //         dialogComponent.SignUpEmail.text, dialogComponent.SignUpEmail.text)));
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
        public string imageURL;
        public SignInResult(bool canceled, string url)
        {
            this.imageURL = url;
            this.Canceled = canceled;
        }

    }

}
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using Google;

namespace States
{
    class RegistrationForm : BaseState
    {

        Firebase.Auth.FirebaseAuth auth;
        Menus.RegistartionFormGUI dialogComponent;
        bool canceled = false;
        string imageURL;
        public override void Initialize()
        {
            Debug.Log("Init CreateAccount");
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            dialogComponent = SpawnUI<Menus.RegistartionFormGUI>(StringConstants.PrefabsRegistrationFormtMenu);
        }

        public override void Resume(StateExitValue results)
        {
            Debug.Log("Resume RegistrationForm");
            ShowUI();
            if (results != null)
            {
                
                if (results.sourceState == typeof(WaitForTask))
                {
                    Debug.Log("Resume RegistrationForm ");
                    WaitForTask.Results taskResults = results.data as WaitForTask.Results;
                    if (taskResults.task.IsFaulted)
                    {
                        
                        manager.PushState(new ErrorMassage("Error Message", "Could not create account."));
                        // manager.PushState(new BasicDialog("Could not create account."));
                    }
                    else
                    {
                        
                        if (!string.IsNullOrEmpty(dialogComponent.SignUpUsername.text))
                        {
                            Debug.Log("Resume RegistrationForm UpdateUserProfile");
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

        public override void Suspend()
        {
            // HideUI();
        }

        public override StateExitValue Cleanup()
        {
            DestroyUI();
            return new StateExitValue(typeof(RegistrationForm), new RegistrationFormResult(canceled, this.imageURL));
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
               // manager.SwapState(new PanelSigned("USER",""));
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
                    //  manager.PushState(new WaitForTask(auth.CreateUserWithEmailAndPasswordAsync(
                    // dialogComponent.SignUpEmail.text, dialogComponent.SignUpPwd.text)));
                    manager.SwapState(new PanelTrainingCatalogList());
                }

            }
            else if (source == dialogComponent.GoogleUP.gameObject)
            {

                // manager.PushState(new WaitForTaskGoogleIn());
                manager.SwapState(new PanelTrainingCatalogList());

            }
            else if (source == dialogComponent.FacebookUP.gameObject)
            {

                // manager.PushState(new WaitForTaskFacebookIn());
                manager.SwapState(new PanelTrainingCatalogList());

            }
            else if (source == dialogComponent.TwitterUP.gameObject)
            {

                //manager.PushState(new WaitForTaskTwitterIn());
                manager.SwapState(new PanelTrainingCatalogList());

            }
        }
        public class RegistrationFormResult
        {
            public bool Canceled = false;
            public string imageURL;
            public RegistrationFormResult(bool canceled, string url)
            {
                this.imageURL = url;
                this.Canceled = canceled;
            }

        }
    }

}


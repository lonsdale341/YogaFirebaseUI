using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    class WaitingSignInLoad<T> : BaseState
    {

        protected bool isComplete = false;
        System.Threading.Tasks.Task task;
        protected int failedFetches = 0;
        const int MaxDatabaseRetries = 5;
        Firebase.Auth.FirebaseAuth auth;
        string email;
        string pwd;

        protected bool wasSuccessful = false;
        protected T result = default(T);
       
       

        Firebase.Database.FirebaseDatabase database;

        Menus.LoadedLabelGUI menuComponent;

        public WaitingSignInLoad(string email, string pwd)
        {
            this.email =email;
            this.pwd = pwd;
        }

        // Initialization method.  Called after the state
        // is added to the stack.
        public override void Initialize()
        {
            Debug.Log("Init WaitingForDB");
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            menuComponent = SpawnUI<Menus.LoadedLabelGUI>(StringConstants.PrefabLoadedeLabel);
            task = auth.SignInWithEmailAndPasswordAsync(email, pwd);
            auth.SignInWithEmailAndPasswordAsync(email, pwd).ContinueWith(HandleEmailSigninResult);
        }
        void HandleEmailSigninResult(System.Threading.Tasks.Task<Firebase.Auth.FirebaseUser> authTask)
        {
            

            if (authTask.IsCanceled)
            {
                HandleFaultedFetch(authTask);
                return;
            }
            if (authTask.IsFaulted)
            {
                HandleFaultedFetch(authTask);
                return;
            }
            else if (authTask.IsCompleted)
            {
                Debug.Log("HandleResult_2 WaitingForDB");
                wasSuccessful = true;
                
            }
            isComplete = true;



        }
        

        // Called once per frame when the state is active.
        public override void Update()
        {
            Debug.Log("UPDATE");
            if (isComplete)
            {
                manager.PopState();
            }
            
        }

        

        // If a fetch from the database comes back failed, try again, until the
        // maximum number of retries have been reached.  Failures are most often
        // caused by connectivity issues or database access rules.
        //Если авторизация не удалась, повторите попытку, 
        //пока не будет достигнуто максимальное количество попыток.
        // Отказы чаще всего возникают из-за проблем с подключением или правил доступа к базе данных.
        protected void HandleFaultedFetch(
            System.Threading.Tasks.Task<Firebase.Auth.FirebaseUser>task)
        {
           
            //Debug.LogError("Database exception!  Path = [" + path + "]\n" + task.Exception);
            // Retry after failure.
            if (failedFetches++ < MaxDatabaseRetries)
            {
                task = auth.SignInWithEmailAndPasswordAsync(email, pwd);
                auth.SignInWithEmailAndPasswordAsync(email, pwd).ContinueWith(HandleEmailSigninResult);
            }
            else
            {
                // Too many failures.  Exit the state, with wasSuccessful set to false.
                isComplete = true;
            }
        }

        public override StateExitValue Cleanup()
        {
            DestroyUI();
            return new StateExitValue(
              typeof(WaitingSignInLoad<T>), new Results(task));
        }

        // Class for encapsulating the results of the database load, as
        // well as information about whether the load was successful
        // or not.
        public class Results
        {

            public System.Threading.Tasks.Task task;

            public Results(System.Threading.Tasks.Task task)
            {
                this.task = task;
            }
        }
    }


}
 
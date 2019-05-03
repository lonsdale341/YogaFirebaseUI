using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using TwitterKit.Unity;

namespace States
{
    class WaitForTaskTwitterIn : BaseState
    {

       
        string message;
        TwitterSession session;
        string imageUrl;

        Menus.LoadedLabelGUI menuComponent;

        public WaitForTaskTwitterIn()
        {
            


        }

        public override void Initialize()
        {
            session = null;
            Twitter.Init();
            Twitter.LogIn(TwitterLoginComplete, (ApiError error) =>
            {
                message = "Please install Twitter application";
                manager.PopState();
            });
        }

        public void TwitterLoginComplete(TwitterSession session)
        {
            if (session == null || session.ToString().Length == 0)
            {
                message = "Session null or blank.";
                manager.PopState();
               
                return;
            }
            this.session = session;
            imageUrl = String.Format("https://twitter.com/{0}/profile_image?size=normal", session.userName);
            manager.PopState();


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
            return new StateExitValue(typeof(WaitForTaskTwitterIn), new ResultsTwitterIn(this.session,message,this.imageUrl));
        }

        // Class for encapsulating the results of the database load, as
        // well as information about whether the load was successful
        // or not.
        public class ResultsTwitterIn
        {
           
            public string message;
            public TwitterSession session;
            public string iamgeURL;
            public ResultsTwitterIn(TwitterSession session, string message, string url)
            {
                this.iamgeURL = url;
                this.session=session;
                this.message = message;
            }
        }
    }

}
 

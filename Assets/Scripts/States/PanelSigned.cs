using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    // State for basic dialog boxes on the screen.
    // Simply displays a message, and waits for the user to click the
    // button, and then returns to the previous state.
    class PanelSigned : BaseState
    {
        string userName;
        string imageURL;
        Firebase.Auth.FirebaseAuth auth;
        Menus.PanelSignedGUI dialogComponent;

        public PanelSigned(string userName, string imageURL)
        {
            this.userName = userName;
            this.imageURL = imageURL;
        }

        public override void Initialize()
        {
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            Debug.Log("Init BasicDialog");
            dialogComponent = SpawnUI<Menus.PanelSignedGUI>(StringConstants.PrefabPanelSigned);
            dialogComponent.UserName.text = userName;
            if (!string.IsNullOrEmpty(imageURL))
            {
                dialogComponent.StartCoroutine("LoadImage");
            }
            
        }

        public override void Resume(StateExitValue results)
        {
            ShowUI();
        }

        public override void Suspend()
        {
            HideUI();
        }

        public override StateExitValue Cleanup()
        {
            DestroyUI();
            return new StateExitValue(typeof(PanelSigned), null);
        }
        IEnumerator LoadImage()
        {


            WWW www = new WWW(imageURL);
            yield return www;


            dialogComponent.Avata.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        }

        public override void HandleUIEvent(GameObject source, object eventData)
        {
            if (source == dialogComponent.LogOut.gameObject)
            {
                auth.SignOut();
                manager.ClearStack(new Startup());
            }
        }

    }


}

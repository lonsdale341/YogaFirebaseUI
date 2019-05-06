using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace States
{
    class PanelSocialMain : BaseState
    {


        Menus.PanelSocialMainGUI dialogComponent;
        public PanelSocialMain()
        {



        }
        public override void Initialize()
        {

            dialogComponent = SpawnUI<Menus.PanelSocialMainGUI>(StringConstants.PrefabsPanelSocialMain);

        }

        public override void Suspend()
        {
            HideUI();
        }

        public override void Resume(StateExitValue results)
        {
            ShowUI();

        }

        public override StateExitValue Cleanup()
        {
            DestroyUI();
            return new StateExitValue(typeof(PanelSocialMain), null);
        }

        public override void HandleUIEvent(GameObject source, object eventData)
        {
            if (source == dialogComponent.Favourites.gameObject)
            {

                manager.PushState(new PanelSocialFav());
            }

            else if (source == dialogComponent.FeedScreen.gameObject)
            {

                manager.PushState(new PanelSocialFeed());
            }
            
            
            else if (source == dialogComponent.Training.gameObject)
            {

                manager.ClearStack(new PanelTrainingCatalogList());
            }
            else if (source == dialogComponent.Instructor.gameObject)
            {

                //manager.PopState();
            }
            else if (source == dialogComponent.Costomize.gameObject)
            {

                manager.ClearStack(new PanelCustomize());
            }
            else if (source == dialogComponent.Profile.gameObject)
            {

                manager.ClearStack(new PanelProfile());
            }


        }
    }


}

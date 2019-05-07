using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace States
{
    class PanelSocialFeed : BaseState
    {


        Menus.PanelSociaFeedGUI dialogComponent;
        public PanelSocialFeed()
        {



        }
        public override void Initialize()
        {

            dialogComponent = SpawnUI<Menus.PanelSociaFeedGUI>(StringConstants.PrefabsPanelSocialFeed);

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
            return new StateExitValue(typeof(PanelSocialFeed), null);
        }

        public override void HandleUIEvent(GameObject source, object eventData)
        {
            if (source == dialogComponent.Back.gameObject)
            {

                 manager.PopState();
            }

            else if (source == dialogComponent.NewPost.gameObject)
            {

                manager.PushState(new PanelSocialCreatedPos());
            }

            else if (source == dialogComponent.Training.gameObject)
            {

                manager.ClearStack(new PanelTrainingCatalogList());
            }
            else if (source == dialogComponent.Instructor.gameObject)
            {

                manager.ClearStack(new PanelInstructor());
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

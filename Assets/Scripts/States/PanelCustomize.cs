using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace States
{
    class PanelCustomize : BaseState
    {


        Menus.PanelCustomizeGUI dialogComponent;
        public PanelCustomize()
        {



        }
        public override void Initialize()
        {

            dialogComponent = SpawnUI<Menus.PanelCustomizeGUI>(StringConstants.PrefabsPanelCustomize);

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
            return new StateExitValue(typeof(PanelCustomize), null);
        }

        public override void HandleUIEvent(GameObject source, object eventData)
        {
            if (source == dialogComponent.ChatMaster.gameObject)
            {

                manager.PushState(new PanelCustomizeChooseMaster());
            }

           
            else if (source == dialogComponent.Training.gameObject)
            {

                manager.ClearStack(new PanelTrainingCatalogList());
            }
            else if (source == dialogComponent.Instructor.gameObject)
            {

                manager.ClearStack(new PanelInstructor());
            }
            else if (source == dialogComponent.Profile.gameObject)
            {

                manager.ClearStack(new PanelProfile());
            }
            else if (source == dialogComponent.Social.gameObject)
            {

                manager.ClearStack(new PanelSocialMain());
            }


        }
    }


}

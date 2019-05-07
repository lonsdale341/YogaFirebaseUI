using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace States
{
    class PanelInstructor : BaseState
    {


        Menus.PanelInstructorGUI dialogComponent;
        public PanelInstructor()
        {



        }
        public override void Initialize()
        {

            dialogComponent = SpawnUI<Menus.PanelInstructorGUI>(StringConstants.PrefabsPanelInstructor);

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
            return new StateExitValue(typeof(PanelInstructor), null);
        }

        public override void HandleUIEvent(GameObject source, object eventData)
        {
            

           
            if (source == dialogComponent.Training.gameObject)
            {

                manager.ClearStack(new PanelTrainingCatalogList());
            }
            else if (source == dialogComponent.Costomize.gameObject)
            {

                manager.ClearStack(new PanelCustomize());
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

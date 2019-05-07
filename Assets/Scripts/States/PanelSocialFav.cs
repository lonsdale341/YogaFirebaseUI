using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace States
{
    class PanelSocialFav : BaseState
    {


        Menus.PanelSociaFavGUI dialogComponent;
        public PanelSocialFav()
        {



        }
        public override void Initialize()
        {

            dialogComponent = SpawnUI<Menus.PanelSociaFavGUI>(StringConstants.PrefabsPanelSocialFav);

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
            return new StateExitValue(typeof(PanelSocialFav), null);
        }

        public override void HandleUIEvent(GameObject source, object eventData)
        {
            if (source == dialogComponent.Back.gameObject)
            {

                manager.PushState(new PanelSocialMain());
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

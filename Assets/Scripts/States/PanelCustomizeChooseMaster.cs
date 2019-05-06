using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace States
{
    class PanelCustomizeChooseMaster : BaseState
    {


        Menus.PanelCustomizeChooseMasterGUI dialogComponent;
        public PanelCustomizeChooseMaster()
        {



        }
        public override void Initialize()
        {

            dialogComponent = SpawnUI<Menus.PanelCustomizeChooseMasterGUI>(StringConstants.PrefabsPanelChoseMaster);

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
            return new StateExitValue(typeof(PanelCustomizeChooseMaster), null);
        }

        public override void HandleUIEvent(GameObject source, object eventData)
        {
            if (source == dialogComponent.Back.gameObject)
            {

                manager.PopState();
            }

            else if (source == dialogComponent.AdBlack.gameObject)
            {

                manager.PushState(new PanelCustomizeProfileMaster());
            }
            else if (source == dialogComponent.ErikWood.gameObject)
            {

                manager.PushState(new PanelCustomizeProfileMaster());
            }
            else if (source == dialogComponent.Curry.gameObject)
            {

                manager.PushState(new PanelCustomizeProfileMaster());
            }
            else if (source == dialogComponent.Garner.gameObject)
            {

                manager.PushState(new PanelCustomizeProfileMaster());
            }
            else if (source == dialogComponent.Garrett.gameObject)
            {

                manager.PushState(new PanelCustomizeProfileMaster());
            }
            else if (source == dialogComponent.Training.gameObject)
            {

                manager.ClearStack(new PanelTrainingCatalogList());
            }
            else if (source == dialogComponent.Instructor.gameObject)
            {

                //manager.PopState();
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

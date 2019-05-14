using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace States
{
    class PanelTrainingCatalogBlock : BaseState
    {

        
        Menus.PanelTrainingCatalogBlockGUI dialogComponent;
       
        public override void Initialize()
        {
           
            dialogComponent = SpawnUI<Menus.PanelTrainingCatalogBlockGUI>(StringConstants.PrefabsPanelTrainingCatalogBlock);
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
            return new StateExitValue(typeof(PanelTrainingCatalogBlock), null);
        }
        
        public override void HandleUIEvent(GameObject source, object eventData)
        {
            if (source == dialogComponent.ListUP.gameObject)
            {
                
                manager.SwapState(new PanelTrainingCatalogList());
            }

            else if (source == dialogComponent.Asana_1.gameObject)
            {
                CommonData.currentAssana = StringConstants.AnimationAsana_1;
                manager.PushState(new PanelAssanInfo(1));
            }
            else if (source == dialogComponent.Asana_2.gameObject)
            {
                CommonData.currentAssana = StringConstants.AnimationAsana_2;
                manager.PushState(new PanelAssanInfo(2));
            }
            else if (source == dialogComponent.Asana_3.gameObject)
            {
                CommonData.currentAssana = StringConstants.AnimationAsana_3;
                manager.PushState(new PanelAssanInfo(3));
            }
            else if (source == dialogComponent.Asana_4.gameObject)
            {
                CommonData.currentAssana = StringConstants.AnimationAsana_4;
                manager.PushState(new PanelAssanInfo(4));
            }
            else if (source == dialogComponent.Asana_5.gameObject)
            {
                CommonData.currentAssana = StringConstants.AnimationAsana_5;
                manager.PushState(new PanelAssanInfo(5));
            }

            if (source == dialogComponent.Profile.gameObject)
            {

                manager.SwapState(new PanelProfile());
            }
            if (source == dialogComponent.Instructor.gameObject)
            {

                manager.ClearStack(new PanelInstructor());
            }
            if (source == dialogComponent.Costomize.gameObject)
            {

                manager.ClearStack(new PanelCustomize());
            }
            if (source == dialogComponent.Social.gameObject)
            {

                manager.ClearStack(new PanelSocialMain());
            }

        }
    }
   

}
 
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
                manager.PushState(new PanelAssanInfo("Asana 1"));
            }
            else if (source == dialogComponent.Asana_2.gameObject)
            {
                manager.PushState(new PanelAssanInfo("Asana 2"));
            }
            else if (source == dialogComponent.Asana_3.gameObject)
            {
                manager.PushState(new PanelAssanInfo("Asana 3"));
            }
            else if (source == dialogComponent.Asana_4.gameObject)
            {
                manager.PushState(new PanelAssanInfo("Asana 4"));
            }
            else if (source == dialogComponent.Asana_5.gameObject)
            {
                manager.PushState(new PanelAssanInfo("Asana 5"));
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
 
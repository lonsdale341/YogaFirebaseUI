using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace States
{
    class PanelTrainingCatalogList : BaseState
    {

        
        Menus.PanelTrainingCatalogListGUI dialogComponent;
       
        public override void Initialize()
        {
           
            dialogComponent = SpawnUI<Menus.PanelTrainingCatalogListGUI>(StringConstants.PrefabsPanelTrainingCatalogList);
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
            return new StateExitValue(typeof(PanelTrainingCatalogList), null);
        }
        
        public override void HandleUIEvent(GameObject source, object eventData)
        {
            if (source == dialogComponent.BlockUP.gameObject)
            {
                
                manager.SwapState(new PanelTrainingCatalogBlock());
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

                //manager.PopState();
            }
            if (source == dialogComponent.Costomize.gameObject)
            {

                //manager.PopState();
            }
            if (source == dialogComponent.Social.gameObject)
            {

                //manager.PopState();
            }

        }
    }
   

}
 
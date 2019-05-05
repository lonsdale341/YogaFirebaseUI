using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace States
{
    class PanelAssanInfo : BaseState
    {

        string assanaName;
        Menus.PanelAssanInfoGUI dialogComponent;
        public PanelAssanInfo(string nameAssana)
        {

            assanaName = nameAssana;

        }
        public override void Initialize()
        {
           
            dialogComponent = SpawnUI<Menus.PanelAssanInfoGUI>(StringConstants.PrefabsPanelAssanInfo);
            dialogComponent.Asana_Name.text = assanaName;
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
            return new StateExitValue(typeof(PanelAssanInfo), null);
        }
        
        public override void HandleUIEvent(GameObject source, object eventData)
        {
            if (source == dialogComponent.Back.gameObject)
            {

                manager.PopState();
            }

           //else if (source == dialogComponent.Asana_1.gameObject)
           //{
           //    
           //}
            
        }
    }
   

}
 
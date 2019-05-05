using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace States
{
    class PanelProfile_Payment : BaseState
    {

       
        Menus.PanelProfile_PaymentGUI dialogComponent;
        public PanelProfile_Payment()
        {

           

        }
        public override void Initialize()
        {
           
            dialogComponent = SpawnUI<Menus.PanelProfile_PaymentGUI>(StringConstants.PrefabsPanelProfile_Payment);
            
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
            return new StateExitValue(typeof(PanelProfile_Payment), null);
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
 
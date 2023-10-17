using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class TriggerEventsBehaviour : MonoEventsBehaviour
{
     public GameAction triggerEnterAction, triggerEnterRepeatAction, triggerEnterEndAction, triggerExitAction;
     public UnityEvent triggerEnterEvent, triggerEnterRepeatEvent, triggerEnterEndEvent, triggerExitEvent;
     private WaitForSeconds waitForTriggerEnterObj, waitForTriggerRepeatObj;
     public float triggerHoldTime = 0.01f, repeatHoldTime = 0.01f, exitHoldTime = 0.01f;
     public bool canRepeat;
     public int repeatTimes = 10;

     protected override void Awake()
     {
          base.Awake();
          waitForTriggerEnterObj = new WaitForSeconds(triggerHoldTime);
          waitForTriggerRepeatObj = new WaitForSeconds(repeatHoldTime);
     }

     private IEnumerator OnTriggerEnter(Collider other)
     {
          yield return waitForTriggerEnterObj;
          triggerEnterEvent.Invoke();
          if (triggerEnterAction != null) triggerEnterAction.Raise();

          if (canRepeat)
          {
               var i = 0;
               while (i < repeatTimes)
               {
                    yield return waitForTriggerEnterObj;
                    i++;
                    triggerEnterRepeatEvent.Invoke();
                    if (triggerEnterRepeatAction != null) triggerEnterRepeatAction.Raise();
               }
          }
          yield return waitForTriggerRepeatObj;
          triggerEnterEndEvent.Invoke();
          if (triggerEnterEndAction != null) triggerEnterEndAction.Raise();
     }

     private void OnTriggerExit(Collider other)
     {
          triggerExitEvent.Invoke();
          if (triggerExitAction != null) triggerExitAction.Raise();
     }
}
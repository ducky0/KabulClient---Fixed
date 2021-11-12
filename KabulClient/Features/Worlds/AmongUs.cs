using VRC.Udon;
using UnityEngine;

namespace KabulClient.Features.Worlds
{
    class AmongUs
    {
        public static bool worldLoaded = false;
        public static bool emergencyAnnoyEnabled = false;
        public static bool reportbodyAnnoyEnabled = false;
        public static bool SabotageHudEnabled = false;
        public static bool Killloop = false;
        public static bool Lightloop = false;
        public static float update = 0f;
        public static UdonBehaviour gameLogic = null;

        public static void Initialize(string sceneName)
        {
            // TODO: Check world ID aswell.
            if (sceneName == "Skeld")
            {
                gameLogic = GameObject.Find("Game Logic")?.GetComponent<UdonBehaviour>();

                if (gameLogic != null)
                {
                    worldLoaded = true;
                }
            }
            else
            {
                worldLoaded = false;
            }
        }

        public static void OnUpdate()
        {
            if (emergencyAnnoyEnabled)
            {
                EmergencyButton();
            }
            if (reportbodyAnnoyEnabled)
            {
                ReportbodyButton();

            }
            if (Killloop)
            {
                Bodykill();
            }
           
            
            
            //  ToggleSabotageHud(SabotageHudEnabled);
        }

        public static void ToggleSabotageHud(bool value)
        {
            GameObject sabotageHud = GameObject.Find("Game Logic/Sabotage HUD");

            if (sabotageHud == null)
            {
                return;
            }

            sabotageHud.SetActive(value);
        }

        /// <summary>
        /// Starts an emergency meeting, this can be spammed and the results are VERY annoying to others aswell.
        /// </summary>
        public static void EmergencyButton()
        {
            CallUdonEvent("StartMeeting");
            CallUdonEvent("SyncEmergencyMeeting");
        }

        public static void ReportbodyButton()
        {
            CallUdonEvent("SyncBodyFound");
        }
        public static void Bodykill()
        {

           CallUdonEvent("OnLocalPlayerKillsOther");

        }
        public static void SabotageLights()
        {

            CallUdonEvent("SyncTrySabotageLights");

        }
        public static void fixlight()
        {
            CallUdonEvent("SyncRepairComms");
        }




        /// <summary>
        /// Calls a UDON event.
        /// </summary>
        /// <param name="eventName">The name of the event to call.</param>
        public static void CallUdonEvent(string eventName)
        {
            Udon.CallUdonEvent(gameLogic, eventName);
        }

        /// Available UDON events:
        /// GetLocalPlayerNode
        /// SyncTrySabotageLights
        /// SyncBodyFound
        /// SyncRepairComms
        /// SyncRepairOxygenB
    }
}

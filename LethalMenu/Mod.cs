using System.Reflection;
using GameNetcodeStuff;
using MelonLoader;
using UnityEngine;

namespace LethalMenu
{
    public class Mod : MelonMod
    {
        private Rect _windowRect = new Rect(0, 0, 120, 84);
        private static bool _windowOpen = false;
        
        public override void OnGUI()
        {
            if (!_windowOpen)
                return;
            
            _windowRect = GUI.Window(0, _windowRect, WindowRoutine, "Lethal Menu");
        }

        public override void OnUpdate()
        {
            var menu = Object.FindObjectOfType<QuickMenuManager>();

            _windowOpen = false;
            
            if (menu)
                _windowOpen = menu.isMenuOpen;
        }

        private static void WindowRoutine(int windowId)
        {
            if (!_windowOpen)
                return;
            
            if (GUI.Button(new Rect(10, 20, 100, 20), "Money +$1,000"))
            {
                var field = typeof(HUDManager).GetField("terminalScript",
                    BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance);

                if (!field.Equals(null))
                    (field.GetValue(HUDManager.Instance) as Terminal).groupCredits += 1000;
            }

            if (GUI.Button(new Rect(10, 60, 100, 20), "Quota +$1,000"))
            {

                if (!TimeOfDay.Instance.Equals(null))
                {
                    TimeOfDay.Instance.quotaFulfilled += 1000;
                    TimeOfDay.Instance.UpdateProfitQuotaCurrentTime();
                }
            }
            
            GUI.DragWindow();
        }
    }
}
using FrooxEngine;
using HarmonyLib;
using NeosModLoader;
using System;

namespace OculusSwapMenuAndSecondary
{
    public class OculusSwapMenuAndSecondary : NeosMod
    {
        public override string Name => "OculusSwapMenuAndSecondary";
        public override string Author => "badhaloninja";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/badhaloninja/OculusSwapMenuAndSecondary";
        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony("me.badhaloninja.OculusSwapMenuAndSecondary");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(TouchController), "Bind")]
        class TouchController_Bind_Patch
        {
            public static bool Prefix(TouchController __instance, InputGroup group)
            {
                Chirality? side = group.Side;
                Chirality side2 = __instance.Side;
                if (!(side.GetValueOrDefault() == side2 & side != null))
                {
                    return true;
                }
                CommonToolInputs commonToolInputs = group as CommonToolInputs;
                if (commonToolInputs != null)
                {
                    commonToolInputs.Interact.AddBinding(InputNode.Digital(__instance.TriggerClick), __instance);
                    commonToolInputs.Grab.AddBinding(InputNode.Digital(__instance.GripClick), __instance);
                    commonToolInputs.Menu.AddBinding(InputNode.Digital(__instance.ButtonYB), __instance);
                    commonToolInputs.Strength.AddBinding(InputNode.Analog(__instance.Trigger), __instance);
                    commonToolInputs.Axis.AddBinding(InputNode.Analog2D(__instance.Joystick), __instance);


                    //Swap
                    commonToolInputs.Secondary.AddBinding(InputNode.Digital(__instance.ButtonXA), __instance);
                    commonToolInputs.UserspaceToggle.AddBinding(InputNode.Digital(__instance.JoystickClick), __instance);
                    return false;
                }
                return true;
            }
        }
    }
}
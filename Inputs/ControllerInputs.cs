using System;

namespace CodysModTemplate.Inputs
{
    public class ControllerInputs
    {
        // Right Controller Buttons
        public static bool RightPrimary, RightSecondary, RightIndex, RightGrip;

        // Left Controller Buttons
        public static bool LeftPrimary, LeftSecondary, LeftIndex, LeftGrip;

        void OnGameInitialized(object sender, EventArgs e)
        {
            // Right Controller Buttons
            RightPrimary = ControllerInputPoller.instance.rightControllerPrimaryButton;
            RightSecondary = ControllerInputPoller.instance.rightControllerSecondaryButton;
            RightIndex = ControllerInputPoller.instance.rightControllerIndexFloat > .5f;
            RightGrip = ControllerInputPoller.instance.rightGrab;

            // Left Controller Buttons
            LeftPrimary = ControllerInputPoller.instance.leftControllerPrimaryButton;
            LeftSecondary = ControllerInputPoller.instance.leftControllerSecondaryButton;
            LeftIndex = ControllerInputPoller.instance.leftControllerIndexFloat > .5f;
            LeftGrip = ControllerInputPoller.instance.leftGrab;
        }
    }
}
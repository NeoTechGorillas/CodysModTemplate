using System.Text;
using BananaOS;
using BananaOS.Pages;
using UnityEngine;

namespace CodysModTemplate
{
    public class Page : WatchPage
    {
        // Constants to avoid magic strings and to make the code more maintainable
        private const string PageTitle = "Example";
        private const string ButtonLabel1 = "Test Button";
        private const string ButtonLabel2 = "Test Button 2";
        private const string NotificationMessage = "<align=center><size=5>Notification</size></align>";
        private const int NotificationDuration = 1;

        // Default colors for the notification
        private readonly Color notificationColor = Color.blue;
        private readonly Color textColor = Color.white;

        // Main Menu Display Control
        public override bool DisplayOnMainMenu => true;

        // What will be shown on the main menu if DisplayOnMainMenu is set to true
        public override string Title => PageTitle;

        // This method is called after the watch is completely setup
        public override void OnPostModSetup()
        {
            // Set max index for the selection so the indicator stays on the screen
            selectionHandler.maxIndex = 1;
        }

        // This method returns the content to be displayed on the watch screen
        public override string OnGetScreenContent()
        {
            var stringBuilder = new StringBuilder();

            // Adding colorized page title
            stringBuilder.AppendLine($"<color=yellow>==</color> {PageTitle} <color=yellow>==</color>");

            // Adding button options with selection indicators
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, ButtonLabel1));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(1, ButtonLabel2));

            return stringBuilder.ToString();
        }

        // This method handles button press events
        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            switch (buttonType)
            {
                case WatchButtonType.Up:
                    selectionHandler.MoveSelectionUp();
                    break;

                case WatchButtonType.Down:
                    selectionHandler.MoveSelectionDown();
                    break;

                case WatchButtonType.Enter:
                    HandleEnterButtonPress();
                    break;

                case WatchButtonType.Back:
                    ReturnToMainMenu();
                    break;
            }
        }

        // This method handles the Enter button press logic
        private void HandleEnterButtonPress()
        {
            switch (selectionHandler.currentIndex)
            {
                case 0:
                    // Add logic for the first button (Test Button)
                    HandleTestButtonAction();
                    break;

                case 1:
                    // Add logic for the second button (Test Button 2)
                    HandleTestButton2Action();
                    break;

                default:
                    // Add fallback logic or notification if needed
                    break;
            }
        }

        // Example action for Test Button 1
        private void HandleTestButtonAction()
        {
            // This can trigger a notification or some logic
            BananaNotifications.DisplayNotification($"{NotificationMessage} Button 1 clicked!", notificationColor, textColor, NotificationDuration);
        }

        // Example action for Test Button 2
        private void HandleTestButton2Action()
        {
            // This can trigger a different notification or logic
            BananaNotifications.DisplayNotification($"{NotificationMessage} Button 2 clicked!", notificationColor, textColor, NotificationDuration);
        }
    }
}
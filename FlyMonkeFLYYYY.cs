using Photon.Pun;
using System.Text;
using UnityEngine;

namespace BananaOS.Pages
{
    public class FlyPage : WatchPage
    {
        //What will be shown on the main menu if DisplayOnMainMenu is set to true
        public override string Title => "<color=yellow>Slingshot Fly</color>";

        //Enabling will display your page on the main menu if you're nesting pages you should set this to false
        public override bool DisplayOnMainMenu => true;
        float speed = 1200f;

        public bool FlyMonke { get; private set; }

        //This method will be ran after the watch is completely setup
        public override void OnPostModSetup()
        {
            //max selection index so the indicator stays on the screen
            selectionHandler.maxIndex = 1;
        }

        //What you return is what is drawn to the watch screen the screen will be updated everytime you press a button
        public override string OnGetScreenContent()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<color=yellow>==</color> Slingshot Fly <color=yellow>==</color>");
            if (FlyMonke)
            {
                stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, "Enabled"));
            }
            else
            {
                stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, "Disabled"));
            }
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(1, "Speed: " + speed));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(2, ""));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(3, "Made By FireGiraffe"));
            stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(4, "Help From Striker :D"));
            return stringBuilder.ToString();
        }

        void Update()
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString().Contains("MODDED_"))
            {
                if (FlyMonke == true)
                {
                    if (ControllerInputPoller.instance.rightControllerPrimaryButton)
                    {
                        GorillaTagger.Instance.rigidbody.AddForce(GorillaTagger.Instance.headCollider.transform.forward * speed);
                    }
                }
            }
        }

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

                case WatchButtonType.Right:
                    if (selectionHandler.currentIndex == 2)
                    {
                        speed += 10f;
                    }
                    break;

                case WatchButtonType.Left:
                    if (selectionHandler.currentIndex == 2)
                    {
                        if (speed > 500)
                        {
                            speed -= 10f;
                        }

                    }
                    break;

                case WatchButtonType.Enter:
                    if (selectionHandler.currentIndex == 0)
                    {
                        FlyMonke = true;
                    }
                    if (selectionHandler.currentIndex == 1)
                    {
                        FlyMonke = false;
                    }
                    return;

                //It is recommended that you keep this unless you're nesting pages if so you should use the SwitchToPage method
                case WatchButtonType.Back:
                    ReturnToMainMenu();
                    break;
            }
        }
    }
}
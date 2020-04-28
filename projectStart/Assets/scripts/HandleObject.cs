using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class HandleObject : MonoBehaviour
    {
        // Start is called before the first frame update
        GameManager manager;
        void Start()
        {
            manager = GameManager.GetInstance();

        }

        // Update is called once per frame
        void Update()
        {

        }
        [EnumFlags]
        public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand;
        private void HandHoverUpdate(Hand hand)
        {

            GrabTypes startingGrabType = hand.GetGrabStarting();
            if (startingGrabType != GrabTypes.None)
            {
                hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
                manager.SetLevelEnd();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class RewardController : MonoBehaviour
    {
        public float speed = 0.2f;
        public GameObject[] effects;
        GameManager manager;

        //public final position;
        // Start is called before the first frame update
        void Start()
        {
            manager = GameManager.GetInstance();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 v = new Vector3(0, 1, 0); //calculate position of the player
            if (transform.position.y < 2)
            {
                Debug.Log(transform.position);
                transform.Translate(v * Time.deltaTime * speed);
            }
        }
        [EnumFlags]
        public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand;
        private void HandHoverUpdate(Hand hand)
        {

            GrabTypes startingGrabType = hand.GetGrabStarting();
            if (startingGrabType != GrabTypes.None)
            {
                hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
                for(int i = 0; i < effects.Length; i++)
                {
                    Destroy(effects[i]);
                }
                manager.SetLevelEnd();
            }
        }
    }
}

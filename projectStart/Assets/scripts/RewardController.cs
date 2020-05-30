using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class RewardController : MonoBehaviour
    {
        public float speed = 0.03f;
        public GameObject[] effects;
        GameManager manager;
        public GameObject player;
        private GameObject effectsObject = null;

        //public final position;
        // Start is called before the first frame update
        void Start()
        {
            manager = GameManager.GetInstance();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 other = player.transform.position - transform.position;
            other.y = 0;
            other.x = 0;
            Vector3 v = new Vector3(0, -0.2f, -1); //calculate position of the player
            if (Vector3.Magnitude(other) > 1 && manager.GetLevel() == GameManager.LEVEL.FINAL)
            {
                //Debug.Log(transform.position);
                transform.Translate(v * Time.deltaTime * speed);
            }else if (manager.GetLevel() == GameManager.LEVEL.FINAL && effectsObject == null)
            {
                effectsObject = manager.LoadInstance("Effects");
            }
        }
        void onDestroy()
        {
            for (int i = 0; i < effects.Length; i++)
            {
                Destroy(effects[i]);
            }
            Destroy(effectsObject);

            manager.SetLevelEnd();
        }
        /*[EnumFlags]
        public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand;
        private void HandHoverUpdate(Hand hand)
        {

            GrabTypes startingGrabType = hand.GetGrabStarting();
            if (startingGrabType != GrabTypes.None)
            {
                hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
                manager.SetLevelEnd();
            }
        }*/
    }
}

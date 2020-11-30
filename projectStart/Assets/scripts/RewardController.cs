using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class RewardController : MonoBehaviour
    {
        public float speed = 0.03f;
        public GameObject[] effects;
        GameManager manager;
        public GameObject player;
        public GameObject cam;
        private GameObject effectsObject = null;
        int time = -1;
        bool d = true;

        //public final position;
        // Start is called before the first frame update
        void Start()
        {
            manager = GameManager.GetInstance();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 other = transform.position - cam.transform.position;
            //Debug.Log("other vector: " + other);
            Vector3 v1 = new Vector3(0, -0.3f, 0); //calculate position of the player
            Vector3 v2 = new Vector3(0, 0, -1f); //calculate position of the player
            if ((other.y > 0.1 || other.z > 0.1) && manager.GetLevel() == GameManager.LEVEL.FINAL)
            {
                if (other.y > 0.1 )
                {
                    transform.Translate(v1 * Time.deltaTime * speed);
                } if (other.z > 1)
                {
                    transform.Translate(v2 * Time.deltaTime * speed);
                }
            }
            else if (manager.GetLevel() == GameManager.LEVEL.FINAL && effectsObject == null)
            {
                effectsObject = manager.LoadInstance("EndEffects");
            }
            else if (manager.GetLevel() == GameManager.LEVEL.FINAL && effectsObject != null)
            {
                time++;
                if (time == 50)
                {
                    manager.SetLevelEnd();
                    //Destroy(gameObject);

                }
            }
        }
        void onDestroy()
        {
            for (int i = 0; i < effects.Length; i++)
            {
                Destroy(effects[i]);
            }
            Destroy(effectsObject);

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

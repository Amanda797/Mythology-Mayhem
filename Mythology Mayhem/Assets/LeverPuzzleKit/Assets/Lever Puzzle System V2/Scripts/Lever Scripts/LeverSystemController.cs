/// REMEMBER: This script has a custom editor called "LeverSystemControllerEditor", found in the "Editor" folder. You will need to add new properties to this
/// if you create new variables / fields in this script. Contact me if you have any troubles at all!

using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace LeverSystem
{
    public class LeverSystemController : MonoBehaviour
    {
        //In-game order - Used for debugging (Can be hidden)
        private string playerOrder = null;

        //Order of which the levers should be pulled
        [Tooltip("Order the levers should be pulled")]
        [SerializeField] private string leverOrder = "12345";

        [Tooltip("Pull Limit - Match this with the number of values in the leverOrder")]
        [SerializeField] private int pullLimit = 5;
        private int pulls;

        [Tooltip("Time before pulling lever after interacting")]
        [SerializeField] private float pullTimer = 1.0f;
        private bool canPull = true;
        private bool resetting;

        [Tooltip("Add each of the levers and buttons that you will interact with")]
        [SerializeField] private GameObject[] interactiveObjects = null;

        //Control Box Switches (Animated)
        [SerializeField] private Animator readySwitch = null;
        [SerializeField] private Animator limitReachedSwitch = null;
        [SerializeField] private Animator acceptedSwitch = null;
        [SerializeField] private Animator resettingSwitch = null;


        //Control Unit Lights
        [SerializeField] private GameObject readyLight = null;
        [SerializeField] private GameObject limitReachedLight = null;
        [SerializeField] private GameObject acceptedLight = null;
        [SerializeField] private GameObject resettingLight = null;
        private Material readyBtnMat;
        private Material resettingBtnMat;
        private Material acceptedBtnMat;
        private Material limitBtnMat;

        //Accept / Reset Buttons
        [SerializeField] private Animator testButton = null;
        [SerializeField] private Animator resetButton = null;

        //Switch Object - Animation Names
        [SerializeField] private string switchOnName = "Switch_On";
        [SerializeField] private string switchOffName = "Switch_Off";
        [SerializeField] private string redButtonName = "RedButton_Push";

        //Sound Effects
        [SerializeField] private Sound switchPullSO = null;
        [SerializeField] private Sound switchFailSO = null;
        [SerializeField] private Sound switchDoorSO = null;

        //Unity Events
        [SerializeField] private UnityEvent LeverPower = null;

        private void Start()
        {
            readyBtnMat = readyLight.GetComponent<Renderer>().material;
            resettingBtnMat = resettingLight.GetComponent<Renderer>().material;
            acceptedBtnMat = acceptedLight.GetComponent<Renderer>().material;
            limitBtnMat = limitReachedLight.GetComponent<Renderer>().material;
            readyBtnMat.color = Color.green;

            readySwitch.Play(switchOnName, 0, 0.0f);
            resettingSwitch.Play(switchOffName, 0, 0.0f);
            acceptedSwitch.Play(switchOffName, 0, 0.0f);
            limitReachedSwitch.Play(switchOffName, 0, 0.0f);
        }

        void LeverInteraction()
        {
            LeverPower.Invoke();
        }

        IEnumerator Timer()
        {
            canPull = false;
            yield return new WaitForSeconds(pullTimer);
            canPull = true;
        }

        public void initLeverPull(LeverItem _leverItem, int leverNumber)
        {
            if (canPull)
            {
                if (pulls <= pullLimit - 1)
                {
                    _leverItem.HandleAnimation();
                    LeverPull(leverNumber);
                }
            }
        }

        public void LeverPull(int leverNumber)
        {
            playerOrder = playerOrder + leverNumber;
            pulls++;

            if (canPull)
            {
                StartCoroutine(Timer());
                SwitchPullAudio();
                if (pulls >= pullLimit)
                {
                    readyBtnMat.color = Color.red;
                    limitBtnMat.color = Color.green;

                    readySwitch.Play(switchOffName, 0, 0.0f);
                    limitReachedSwitch.Play(switchOnName, 0, 0.0f);
                }
            }
        }

        public void LeverReset()
        {
            pulls = 0;
            playerOrder = "";
            SwitchFailAudio();
            resetButton.Play(redButtonName, 0, 0.0f);

            StartCoroutine(Timer(1.0f));
            if (resetting)
            {
                readyBtnMat.color = Color.red;
                resettingBtnMat.color = Color.green;
                acceptedBtnMat.color = Color.red;
                limitBtnMat.color = Color.red;

                readySwitch.Play(switchOffName, 0, 0.0f);
                resettingSwitch.Play(switchOnName, 0, 0.0f);
                acceptedSwitch.Play(switchOffName, 0, 0.0f);
                limitReachedSwitch.Play(switchOffName, 0, 0.0f);
            }
        }

        public void LeverCheck()
        {
            testButton.Play(redButtonName, 0, 0.0f);
            if (playerOrder == leverOrder)
            {
                pulls = 0;
                SwitchDoorOpenAudio();

                LeverInteraction();

                for (int i = 0; i < interactiveObjects.Length; i++)
                {
                    interactiveObjects[i].gameObject.tag = "Untagged";
                }

                readyBtnMat.color = Color.red;
                resettingBtnMat.color = Color.red;
                acceptedBtnMat.color = Color.green;
                limitBtnMat.color = Color.red;

                readySwitch.Play(switchOffName, 0, 0.0f);
                resettingSwitch.Play(switchOffName, 0, 0.0f);
                acceptedSwitch.Play(switchOnName, 0, 0.0f);
                limitReachedSwitch.Play(switchOffName, 0, 0.0f);
            }

            else
            {
                LeverReset();
            }
        }

        IEnumerator Timer(float waitTime)
        {
            resetting = true;
            yield return new WaitForSeconds(waitTime);
            readyBtnMat.color = Color.green;
            resettingBtnMat.color = Color.red;
            
            readySwitch.Play(switchOnName, 0, 0.0f);
            resettingSwitch.Play(switchOffName, 0, 0.0f);

            resetting = false;
        }

        void SwitchPullAudio()
        {
            LeverAudioManager.instance.Play(switchPullSO.name);

        }

        void SwitchFailAudio()
        {
            LeverAudioManager.instance.Play(switchFailSO.name);
        }

        void SwitchDoorOpenAudio()
        {
            LeverAudioManager.instance.Play(switchDoorSO.name);
        }


        private void OnDestroy()
        {
            Destroy(readyBtnMat);
            Destroy(resettingBtnMat);
            Destroy(acceptedBtnMat);
            Destroy(limitBtnMat);
        }
    }
}

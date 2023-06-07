/// REMEMBER: This script has a custom editor called "LeverItemEditor", found in the "Editor" folder. You will need to add new properties to this
/// if you create new variables / fields in this script. Contact me if you have any troubles at all!

using UnityEngine;

namespace LeverSystemTwo
{
public class TwoDLeverItem : MonoBehaviour
{
        public ObjectType _objectType = ObjectType.None;
        public enum ObjectType { None, Lever, TestButton, ResetButton }

        public bool inRange = false;

        [SerializeField] private int leverNumber = 0;

        //This is the name of the handle animation, which is usually a child of the object which has this script
        [SerializeField] private string animationName = "Lever";

        [SerializeField] private TwoDLeverSystem _twoDLeverSystem = null;

        private Animator handleAnimation;

        private void Start()
        {
            handleAnimation = GetComponentInChildren<Animator>();
        }

        public void ObjectInteract()
        {
            if (_objectType == ObjectType.Lever)
            {
                LeverNumber();
            }

            else if (_objectType == ObjectType.TestButton)
            {
                LeverCheck();
            }

            else if (_objectType == ObjectType.ResetButton)
            {
                LeverReset();
            }
        }

        public void LeverNumber()
        {
            _twoDLeverSystem.initLeverPull(this, leverNumber);
        }

        public void HandleAnimation()
        {
            handleAnimation.Play(animationName, 0, 0.0f);
        }

        public void LeverReset()
        {
            _twoDLeverSystem.LeverReset();
        }

        public void LeverCheck()
        {
            _twoDLeverSystem.LeverCheck();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ( gameObject.name == "2Dlever")
            {
                
                Debug.Log(" : " + gameObject.name);
            }
            
        }

    }
}
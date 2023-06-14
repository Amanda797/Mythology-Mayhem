/// REMEMBER: This script has a custom editor called "LeverItemEditor", found in the "Editor" folder. You will need to add new properties to this
/// if you create new variables / fields in this script. Contact me if you have any troubles at all!

using UnityEngine;

namespace LeverSystem
{
    public class LeverItem : MonoBehaviour
    {
        public ObjectType _objectType = ObjectType.None;
        public enum ObjectType { None, Lever, TestButton, ResetButton }

        [SerializeField] private int leverNumber = 0;

        //This is the name of the handle animation, which is usually a child of the object which has this script
        [SerializeField] private string animationName = "Handle_Pull";

        [SerializeField] private LeverSystemController _leverSystemController = null;

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
            _leverSystemController.initLeverPull(this, leverNumber);
        }

        public void HandleAnimation()
        {
            handleAnimation.Play(animationName, 0, 0.0f);
        }

        public void LeverReset()
        {
            _leverSystemController.LeverReset();
        }

        public void LeverCheck()
        {
            _leverSystemController.LeverCheck();
        }
    }
}
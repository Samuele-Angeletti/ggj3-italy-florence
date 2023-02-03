using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
    [TaskCategory("Unity/GameObject")]
    [TaskDescription("Sends a message to the target GameObject. Returns Success.")]
    public class SendMessage : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("The message to send")]
        public SharedString message;
        [Tooltip("The value to send")]
        public SharedString value;
        Damager Damager;
        public override void OnAwake()
        {
            base.OnAwake();

        }

        

        public override void OnReset()
        {
            targetGameObject = null;
            message = "";
        }
    }


    [TaskCategory("Unity/GameObject")]
    [TaskDescription("Sends a message to the target GameObject. Returns Success.")]
    public class SendMessageSubCategoryAttackType : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("The message to send")]
        public SharedString message;
        [Tooltip("The value to send")]
        public SharedString value;
        Damager Damager;
        public override void OnAwake()
        {
            base.OnAwake();

        }

       

        public override void OnReset()
        {
            targetGameObject = null;
            message = "";
        }
    }
}
using System;

using UnityEngine;

namespace JUnity.Actions
{
    public class JRotateTo : JFiniteTimeAction
    {

        #region Constructors
	
		public new float Duration{get;private set;}
		//public Vector3 TargetAngle{get;private set;}
        public Quaternion TargetQuaternion { get; private set; }

		public JRotateTo(float duration,Vector3 toAngle):base(duration)
		{
			Duration = duration;
			TargetQuaternion =Quaternion.Euler( toAngle);
		}

        public JRotateTo(float duration,Quaternion toQuater):base(duration)
        {
            Duration = duration;
            TargetQuaternion = toQuater;
        }

        #endregion Constructors

        protected internal override JActionState StartAction(GameObject target)
        {
            return new JRotateToState (this, target);
        }

        public override JFiniteTimeAction Reverse()
        {
            throw new NotImplementedException();
        }
    }


    public class JRotateToState : JFiniteTimeActionState
    {


        public JRotateToState (JRotateTo action, GameObject target)
            : base (action, target)
        { 
			if(Target == null)
			{
				return;
			}
			FromAngle = Target.transform.localRotation;
            ToAngle = action.TargetQuaternion;// Quaternion.Euler( action.TargetAngle);
			InTime = action.Duration;
		}

		Quaternion FromAngle;
		Quaternion ToAngle;
		float InTime;
		float curTime = 0f;

        public override void Update (float time)
        {
			if(Target != null ){
				Target.transform.localRotation = Quaternion.Lerp(FromAngle,ToAngle,curTime/InTime);
			}

			curTime += Time.deltaTime;

        }

    }
}
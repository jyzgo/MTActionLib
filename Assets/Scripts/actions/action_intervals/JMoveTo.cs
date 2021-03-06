using UnityEngine;

namespace JUnity.Actions
{
    public class JMoveTo : JMoveBy
    {
        protected Vector3 EndPosition;

		bool _isWorld = false;
        #region Constructors

		public JMoveTo (float duration, Vector3 position,bool isWorld = false) : base (duration, position)
        {
            EndPosition = position;
			_isWorld = isWorld;
        }

        #endregion Constructors

        public Vector3 PositionEnd {
            get { return EndPosition; }
        }

        protected internal override JActionState StartAction(GameObject target)
        {
			return new JMoveToState (this, target,_isWorld);

        }
    }

    public class JMoveToState : JMoveByState
	{
		public bool IsWorld {
			get;
			protected set;
		}

		public JMoveToState (JMoveTo action, GameObject target,bool isWorld)
            : base (action, target)
        { 
			if(target == null)
			{
				return;
			}
			IsWorld = isWorld;
			if(isWorld)
			{
				StartPosition = target.transform.position;
				PositionDelta = action.PositionEnd - target.transform.position;
			}else
			{
				StartPosition = target.transform.localPosition;
				PositionDelta = action.PositionEnd - target.transform.localPosition;
			}
        }

        public override void Update (float time)
        {
            if (Target != null)
            {
				Vector3 newPos = StartPosition + PositionDelta * time;
				PreviousPosition = newPos;
				if(IsWorld)
				{
					Target.transform.position = newPos;
				}else
				{
					Target.transform.localPosition = newPos;
				}
            }
        }
    }

}
using UnityEngine;

namespace JUnity.Actions
{
    public class MTCameraOSizeChangeTo : JFiniteTimeAction
    {
        private Camera _camera;
        protected float Endsize;

        #region Constructors
        public MTCameraOSizeChangeTo (float duration, float size, Camera camera) : base (duration)
        {
            this._camera = camera;
            Endsize = size;
        }
        #endregion Constructors

        public float SizeEnd {
            get { return Endsize; }
        }

        protected internal override JActionState StartAction(GameObject target)
        {
            return new MTCameraOSizeChangeToState (this, target, this._camera);
        }

        public override JFiniteTimeAction Reverse(){
            return new MTCameraOSizeChangeTo(Duration, - Endsize, this._camera);
        }
    }

    public class MTCameraOSizeChangeToState : JFiniteTimeActionState
	{
        Camera camera;
        protected float SizeDelta;
        protected float EndSize;
        protected float StartSize;
        public float PreviousSize {
            get;
            protected set;
        }

        public MTCameraOSizeChangeToState (MTCameraOSizeChangeTo action, GameObject target, Camera camera)
            : base (action, target)
        { 
            if(target == null)
            {
                return;
            }
            this.camera = camera;
            var curSize = camera.orthographicSize;
            SizeDelta = action.SizeEnd - curSize;
            PreviousSize = StartSize = curSize;
        }

        public override void Update (float time)
        {
            if (Target != null)
            {
                float newSize = StartSize + SizeDelta * time;
                PreviousSize = newSize;
                camera.orthographicSize = newSize;
            }
        }

    }

}
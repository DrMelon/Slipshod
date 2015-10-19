using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace Slipshod
{
    class Player : Entity
    {

        // No Spritesheet Yet, Using Image
        public Image mySprite;

        // Components
        public PlatformingMovement myPlatforming;
        public BoxCollider myCollider;

        // Movement Stuff
        public enum MoveType { GROUND, FALL };

        public MoveType myMoveType = MoveType.GROUND;


        public Player(float x=0, float y=0)
        {
            X = x;
            Y = y;

            // Create and center sprite
            mySprite = Image.CreateRectangle(16, 32, Color.White);
            mySprite.CenterOrigin();
            AddGraphic(mySprite);

            // Set up Collider
            myCollider = new BoxCollider(16, 32, Tags.PLAYER_COLLISION);
            myCollider.CenterOrigin();
            AddCollider(myCollider);

            // Set up PlatformingMovement Component
            myPlatforming = new PlatformingMovement(200, 800, 9.5f);
            myPlatforming.UseAxis = true;
            myPlatforming.Axis = Global.theController.DPad;
            myPlatforming.JumpButton = Global.theController.A;
            myPlatforming.AddCollision(Tags.GROUND_SOLID_COLLISION);
            myPlatforming.JumpStrength = 350.0f;
            myPlatforming.JumpDampening = 0.00001f;
            myPlatforming.LedgeBufferMax = 15;
            myPlatforming.Acceleration[AccelType.Ground] = 50;
            myPlatforming.Acceleration[AccelType.Air] = 50;
            myPlatforming.VariableJumpHeight = true;
            myPlatforming.Collider = myCollider;
            AddComponent(myPlatforming);

            // Set up Layer
            Layer = Layers.PLAYER;

        }

        public void HandleInput()
        {

        }

        public void UpdateMoveState()
        {

        }

        public void UpdateAnimations()
        {

        }

        public override void Update()
        {
            base.Update();

            HandleInput();
            UpdateMoveState();
            UpdateAnimations();

        }
    }
}

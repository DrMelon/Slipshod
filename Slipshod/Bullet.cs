using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace Slipshod
{
    class Bullet : Entity
    {
        // Bullet class is for all fireable projectiles in the game - friendly and enemy.
        public int Affiliation = Tags.BULLET_FRIENDLY;
        public BoxCollider myCollider;
        public Image mySprite;
        public Vector2 mySpeed;
        public Vector2 maxSpeed;
        public int NumBounces = 0; // number of bounces/ricochets
        public Vector2 Gravity = new Vector2(0, 0);
        

        public Bullet(Vector2 fired, float x = 0, float y = 0)
        {
            X = x;
            Y = y;
            if(fired == null)
            {
                fired = new Vector2(1, 0);
            }
            maxSpeed.X = 50;
            maxSpeed.Y = 50;
            mySpeed = fired;

            // Create sprite
            mySprite = Image.CreateRectangle(16, 16, Color.Yellow);
            mySprite.CenterOrigin();
            mySprite.Angle = Util.Angle(mySpeed);
            AddGraphic(mySprite);

            // Create collider
            myCollider = new BoxCollider(16, 16, Tags.BULLET_COLLISION_PLAYER);
            myCollider.CenterOrigin();
            AddCollider(myCollider);

            Layer = Layers.BULLET;

            LifeSpan = 600.0f;
        }

        public override void Update()
        {
            base.Update();

            // Apply Gravity!
            mySpeed.X += Gravity.X;
            mySpeed.Y += Gravity.Y;

            // Clamp to max
            Util.Clamp(mySpeed, maxSpeed);

            // Travel in direction fired!
            X += mySpeed.X;
            Y += mySpeed.Y;

            

            // Check collisions
            // Check ground collision first

            Collider groundCollision = Collide(X, Y, Tags.GROUND_SOLID_COLLISION);

            if (groundCollision != null && groundCollision.Entity != null)
            {
                if (NumBounces > 0)
                {
                    NumBounces--;
                    if(groundCollision.Entity.Y < Y)
                    {
                        mySpeed.Y = -mySpeed.Y;
                    }
                    if(groundCollision.Entity.X - X > mySpeed.X)
                    {
                        mySpeed.X = -mySpeed.X;
                    }
                    
                    
                }
                else
                {
                    HitAnimation();
                }
            }
            if (Affiliation == Tags.BULLET_FRIENDLY)
            {
                // Check ENEMY collision etc
            }



        }

        public void HitAnimation()
        {
            // Go foof! 
            // After animation, remove self.
            RemoveSelf();
            // Add particles.
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace Slipshod
{
    class Weapon
    {
        // Default peashooter gun!

        public Player myOwner;
        public string WeaponName = "Popgun";
        public bool Automatic = false;
        public int AmmoType = 0; // 0 normal, 1 fire, 2 energy, 3 rocket
        public bool RegenAmmo = true;
        public int RegenFrames = 15;
        public int CurRegenFrame = 15;
        public int MaxAmmo = 16;
        public int Ammo = 16;
        public int BulletType = 0;
        public float FireSpread = 0.5f;

        public Weapon(Player own)
        {
            myOwner = own;
        }

        public void Update()
        {
            if(RegenAmmo)
            {
                CurRegenFrame--;
                if(CurRegenFrame < 0)
                {
                    CurRegenFrame = RegenFrames;
                    Ammo++;
                    if(Ammo > MaxAmmo)
                    {
                        Ammo = MaxAmmo;
                    }
                }
            }
        }


        public void TryFireWeapon()
        {
            if(Ammo > 0)
            {
                Ammo--;
                // Fire!

                int facingRight = 1;
                if (myOwner.mySprite.FlippedX)
                {
                    facingRight = -1;
                }
                Bullet newBullet;
                if (Global.theController.DPad.Y < 0)
                {
                    newBullet = new Bullet(new Vector2(0, -10), myOwner.X, myOwner.Y);
                    newBullet.X += Rand.Float(-FireSpread, FireSpread);
                    newBullet.Y += Rand.Float(-FireSpread, FireSpread);
                    newBullet.mySpeed.X += Rand.Float(-FireSpread, FireSpread);
                    newBullet.mySpeed.Y += Rand.Float(-FireSpread, FireSpread);
                    newBullet.BulletType = BulletType;
                }
                else
                {
                    newBullet = new Bullet(new Vector2(facingRight * 10, 0), myOwner.X, myOwner.Y);
                    newBullet.X += Rand.Float(-FireSpread, FireSpread);
                    newBullet.Y += Rand.Float(-FireSpread, FireSpread);
                    newBullet.mySpeed.X += Rand.Float(-FireSpread, FireSpread);
                    newBullet.mySpeed.Y += Rand.Float(-FireSpread, FireSpread);
                    newBullet.BulletType = BulletType;
                    //myOwner.myPlatforming.ExtraSpeed.X = facingRight * -100f;
                    //myOwner.myPlatforming.ExtraSpeed.Y = -30f;
                }

                newBullet.NumBounces = 1;
                newBullet.Gravity = new Vector2(0, 0.5f);


                myOwner.Scene.Add(newBullet);
            }
            else
            {
                // Out of ammo!
            }
        }


    }
}

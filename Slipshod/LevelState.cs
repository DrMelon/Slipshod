using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using Otter.TiledLoader;

namespace Slipshod
{
    class LevelState : Scene
    {

        public String CurrentLevel = "";
        public TiledProject LoadedLevel;

        // Levels are formatted as follows:
        // [0] Background Layer - Not Parallax Background, but things like walls or pillars. 
        // [1] Solid Layer - Things what is solid.
        // [2] Decoration Layer - Things that are on top of the player, like hidden walls and stuff.
        // [3] Objects Layer - Metadata objects and stuff. Also contains info for parallax objects.
        public Tilemap BackgroundLayer;
        public Tilemap SolidLayer;
        public Tilemap DecorationLayer;
        public GridCollider CollisionLayer;
        

        // Reference to Player
        public Player thePlayer;

        // Target camera location, useful for player tracking & cutscene stuff
        public Vector2 TargetCamera;
        public int CameraMode;
        public float CameraLerpSpeed = 4.0f;

        public LevelState(String LevelToLoad = null)
        {
            LoadLevel(LevelToLoad);

            // Verify player exists.
            if (thePlayer == null)
            {
                Util.Log("ERROR! Player does not exist.");
            }

            // Initialize Camera Pos and Mode
            CameraMode = Tags.CAMERA_PLAYERFOLLOW;
            TargetCamera = new Vector2(0, 0);

        }
        
        public void LoadLevel(String LevelToLoad)
        {
            Global.theGame.Color = new Color("9cd9e2");
            CurrentLevel = LevelToLoad;
            LoadedLevel = new TiledProject(LevelToLoad);
            

            // Load Layers
            BackgroundLayer = LoadedLevel.CreateTilemap((TiledTileLayer)LoadedLevel.Layers[0]);
            SolidLayer = LoadedLevel.CreateTilemap((TiledTileLayer)LoadedLevel.Layers[1]);
            DecorationLayer = LoadedLevel.CreateTilemap((TiledTileLayer)LoadedLevel.Layers[2]);

            // Create collision layer
            CollisionLayer = LoadedLevel.CreateGridCollider((TiledTileLayer)LoadedLevel.Layers[1], Tags.GROUND_SOLID_COLLISION);
            Entity CollisionEnt = new Entity();
            CollisionEnt.AddCollider(CollisionLayer);
            Add(CollisionEnt);

            // Add Graphics for lower layers
            AddGraphic(BackgroundLayer);
            AddGraphic(SolidLayer);

            // Objects go between the solid and decoration layers
            CreateLevelObjects();

            // Set camera bounds based on map size.
            UseCameraBounds = true;
            ApplyCamera = true;
            CameraBounds.X = 0;
            CameraBounds.Y = 0;
            CameraBounds.Width = LoadedLevel.PixelWidth;
            CameraBounds.Height = LoadedLevel.PixelHeight;


            // Finish up
            Entity ForegroundEnt = new Entity();
            ForegroundEnt.AddGraphic(DecorationLayer);
            ForegroundEnt.Layer = Layers.FG;
            Add(ForegroundEnt);
        }

        public void CreateLevelObjects()
        {
            // Cycle through all objects in map file.
            TiledObjectGroup MapObjects = (TiledObjectGroup)LoadedLevel.Layers[3];

            for (int i = 0; i < MapObjects.Objects.Count; i++)
            {
                TiledObject CurrentObject = MapObjects.Objects[i];
                if(CurrentObject.Name == "PlayerStart")
                {
                    thePlayer = new Player(CurrentObject.X, CurrentObject.Y);
                    Add(thePlayer);
                }
            }
        }

        public void UpdateCamera()
        {
            // Update the camera.
            
            // If we're in follow mode:
            if(CameraMode == Tags.CAMERA_PLAYERFOLLOW)
            {
                // Facing Left
                if(thePlayer.mySprite.FlippedX)
                {
                    TargetCamera.X = (thePlayer.X) - 80.0f;
                }
                else // Facing Right
                {
                    TargetCamera.X = (thePlayer.X) + 80.0f;
                }

                // Put camera above player a little.
                TargetCamera.Y = thePlayer.Y + 16.0f;

                // Smoothly lerp camera.
                CenterCamera(Util.Approach(CameraCenterX, TargetCamera.X, CameraLerpSpeed), Util.Approach(CameraCenterY, TargetCamera.Y, CameraLerpSpeed));
            }


            // Move camera towards camera position.
        }

        public override void Begin()
        {
            base.Begin();
        }

        public override void End()
        {
            base.End();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void UpdateLast()
        {
            UpdateCamera();
            base.UpdateLast();
        }

        public override void Render()
        {
            base.Render();
        }


    }
}

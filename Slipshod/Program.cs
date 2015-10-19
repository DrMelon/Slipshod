using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace Slipshod
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create game and window
            Global.theGame = new Game("Slipshod", 400, 240, 60, false);
            Global.theGame.SetWindowScale(2);

            // Create (load?) player session
            Global.thePlayerSession = Global.theGame.AddSession("Player1");

            // Set up controller and control bindings for keyboard
            Global.thePlayerSession.Controller = new ControllerXbox360(0);
            Global.theController = Global.thePlayerSession.GetController<ControllerXbox360>();

            // Leftstick/Motion
            Global.theController.DPad.AddKeys(new Key[] { Otter.Key.Up, Otter.Key.Right, Otter.Key.Down, Otter.Key.Left });
            Global.theController.DPad.AddAxis(Global.theController.LeftStick);

            // Jump
            Global.theController.A.AddKey(Otter.Key.Z);

            // Fire
            Global.theController.X.AddKey(Otter.Key.X);

            // Start/Pause
            Global.theController.Start.AddKey(Otter.Key.Return);
            Global.theController.Start.AddKey(Otter.Key.Escape);
            Global.theGame.EnableQuitButton = false;

            // Switch Weapon
            Global.theController.LB.AddKey(Otter.Key.A);
            Global.theController.RB.AddKey(Otter.Key.S);

            // Open Weapod Mod Dialog
            Global.theController.Y.AddKey(Otter.Key.Tab);

            // Interact in other way
            Global.theController.B.AddKey(Otter.Key.C);



            // Initialize first scene
            Global.theGame.AddScene(new LevelState(Assets.MAP_TESTONE));



            // Begin loop!
            Global.theGame.Start();

        }
    }
}

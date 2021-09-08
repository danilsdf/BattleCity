using System;
using System.Collections.Generic;
using System.Drawing;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BattleCity.Enums;
using BattleCity.Shared;

namespace BattleCity.Game
{
    class StartMenu
    {
        private MenuState _menuState;
        private static Label menuControl;
        private Label player;
        private Label marker;
        private int position = 0;

        /// <summary>
        /// конструктор екрана игры
        /// </summary>
        /// <param name="control">принимает родительский Control</param>
        public StartMenu(Control control)
        {
            //using var resxSet = new ResXResourceSet(Constants.Path.ResxFile);
            _menuState = MenuState.Game;

            menuControl = new Label();
            menuControl.Parent = control;
            menuControl.Location = new Point(0, 0);
            //menuControl.Image = (Bitmap)resxSet.GetObject("Menu", true);
            //menuControl.Size = menuControl.Image.Size;

            player = new Label();
            player.Parent = menuControl;
            player.Location = new Point(265, 770);
            player.BackColor = Color.Transparent;
            player.Font = new Font("COURER", 20, FontStyle.Bold);
            player.ForeColor = Color.White;
            player.TextAlign = ContentAlignment.MiddleLeft;
            player.Size = new Size(200, 30);
            player.Text = "1 PLAYER";

            marker = new Label();
            marker.Parent = menuControl;
            //marker.Image = (Bitmap)resxSet.GetObject("Marker", true);
            //marker.Size = marker.Image.Size;
        }

        public MenuState MenuState{ get; set; }

        public static Control MenuControl => menuControl;

        public void ResetPosition()
        {
            position = 0;
        }

        public void Update()
        {
            if (menuControl.Top > menuControl.Parent.Height - menuControl.Height - 30)
            {
                position -= 4;
            }
            else if (Keyboard.Down)
            {
                if (MenuState < MenuState.LoadGame)
                {
                    MenuState++;
                    Thread.Sleep(200);
                }
            }
            else if (Keyboard.Up)
            {
                if (MenuState > MenuState.Game)
                {
                    MenuState--;
                    Thread.Sleep(200);
                }
            }

            switch (MenuState)
            {
                case MenuState.Game:
                    marker.Location = new Point(210, 765);
                    break;
                case MenuState.SaveGame:
                    marker.Location = new Point(210, 795);
                    break;
                case MenuState.LoadGame:
                    marker.Location = new Point(210, 825);
                    break;
            }
        }

        public void Draw()
        {
            menuControl.Location = new Point(-40, position);
        }
    }
}

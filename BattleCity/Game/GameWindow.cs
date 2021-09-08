using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using BattleCity.Shared;

namespace BattleCity.Game
{
    public sealed class GameWindow : Label
    {
        public GameWindow(Control control)
        {
            Parent = control;
            //this.BackgroundImage = Properties.Resources.PlayingField;
            Location = new Point();
            ClientSize = new Size(Constants.Size.WindowClientWidth, Constants.Size.WindowClientWidth);
            BackColor = Color.Black;
            GraphicsOption();
        }

        private void GraphicsOption()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            var offset = new Point(Constants.Size.WidthTile, Constants.Size.HeightTile);

            base.OnPaint(e);
            var g = e.Graphics;

            BackgroundPainting(g);

            foreach (var list in CurrentLevel.DictionaryObjGame.Values)
            {
                foreach (var item in list)
                {
                    item.Draw(g, offset);
                }
            }

            foreach (var item in CurrentLevel.ListInformation)
            {
                item.Draw(g, offset);
            }
        }

        private static void BackgroundPainting(Graphics g)
        {

            var dashboard = Image.FromFile(Constants.Path.Content + $"Dashboard.png");
            g.TranslateTransform(Constants.Size.HeightBoard + Constants.Size.WidthBoard, 0);
            g.DrawImage(dashboard, 0, 0, Constants.Size.WidthTile * 4, Constants.Size.HeightBoard + Constants.Size.HeightTile * 2);
            g.ResetTransform();

            Image borderHorizontal = Image.FromFile(Constants.Path.Content + $"BorderHorizontal.png");
            g.TranslateTransform(0, 0);
            g.DrawImage(borderHorizontal, 0, 0, Constants.Size.WidthTile + Constants.Size.HeightTile * 2, Constants.Size.HeightTile);
            g.ResetTransform();

            g.TranslateTransform(0, Constants.Size.HeightBoard + Constants.Size.HeightTile);
            g.DrawImage(borderHorizontal, 0, 0, Constants.Size.WidthBoard + Constants.Size.HeightTile * 2, Constants.Size.HeightTile);
            g.ResetTransform();

            Image borderVertical = Image.FromFile(Constants.Path.Content + $"BorderVertical.png");
            g.TranslateTransform(0, 0);
            g.DrawImage(borderVertical, 0, 0, Constants.Size.WidthTile, Constants.Size.HeightBoard + Constants.Size.HeightTile * 2);
            g.ResetTransform();
        }
    }
}
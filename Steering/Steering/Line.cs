using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Steering
{
    
    class Line
    {
        private static readonly VertexPositionColor[] PointList = new VertexPositionColor[10000];
        private static readonly BasicEffect BasicEffect = new BasicEffect(XNAGame.Instance().GraphicsDevice);
        static int _currentLine;

        static Line()
        {
            BasicEffect.VertexColorEnabled = true;
        }

        static public void DrawLine(Vector3 start, Vector3 end, Color color)
        {
            BasicEffect.World = Matrix.Identity;
            BasicEffect.View = XNAGame.Instance().Camera.getView();
            BasicEffect.Projection = XNAGame.Instance().Camera.getProjection();
            PointList[_currentLine ++] = new VertexPositionColor(start, color);
            PointList[_currentLine ++] = new VertexPositionColor(end, color);
            
        }

        static public void Draw()
        {
            if (_currentLine != 0)
            {
                foreach (EffectPass pass in BasicEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    XNAGame.Instance().GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, PointList, 0, _currentLine / 2);
                }
                _currentLine = 0;
            }
        }

    }
}

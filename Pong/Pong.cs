using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

namespace Pong;

/// @author matcha
/// @version 24.09.2024
/// <summary>
/// 
/// </summary>
public class Pong : PhysicsGame
{
    public override void Begin()
    {
        PhysicsObject pallo = new PhysicsObject(40.0, 40.0, Shape.Circle);
        pallo.X = -200.0;
        pallo.Y = 0.0;
        pallo.Restitution = 1.0;

        Add(pallo);
        
        LuoKentta();
        
        Vector impulssi = new Vector(500.0, 0.0);
        pallo.Hit(impulssi * pallo.Mass);
        
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }

    private void LuoKentta()
    {
        Level.CreateBorders(1.0, false);
        Level.Background.Color = Color.Black;
        
        Camera.ZoomToLevel();
    }
    
}
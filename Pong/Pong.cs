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
        PhysicsObject pallo = LuoPallo(this, -200, 0);
        
        LuoKentta();
        AloitaPeli(pallo);
        
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }

    private void LuoKentta()
    {
        PhysicsObject maila = PhysicsObject.CreateStaticObject(20.0, 100.0, Shape.Rectangle);
        maila.X = Level.Left + 20.0;
        maila.Y = 0.0;
        maila.Restitution = 1.0;
        Add(maila);
        
        Level.CreateBorders(1.0, false);
        Level.Background.Color = Color.Black;
        
        Camera.ZoomToLevel();
    }
    
    public static PhysicsObject LuoPallo(PhysicsGame peli, double x, double y)
    {
        PhysicsObject pallo = new PhysicsObject(40.0, 40.0, Shape.Circle);
        pallo.X = x;
        pallo.Y = y;
        pallo.Restitution = 1.0;
        pallo.KineticFriction = 0.0;
        pallo.MomentOfInertia = double.PositiveInfinity;
        peli.Add(pallo);
        return pallo;
    }
    
    private static void AloitaPeli(PhysicsObject pallo)
    {
        Vector impulssi = new Vector(500.0, 0.0);
        pallo.Hit(impulssi * pallo.Mass);
    }
    
}
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
        Add(pallo);
        
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
}
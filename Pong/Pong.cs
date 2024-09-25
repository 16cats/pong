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
    private readonly Vector nopeusYlos = new Vector(0, 200);
    private readonly Vector nopeusAlas = new Vector(0, -200);
    public override void Begin()
    {
        PhysicsObject pallo = LuoPallo(this, -200, 0);
        PhysicsObject maila1 = LuoMaila(this,Level.Left + 20.0, 0.0);
        PhysicsObject maila2 = LuoMaila(this,Level.Right - 20.0, 0.0);
        
        LuoKentta();
        AsetaOhjaimet(maila1, maila2);
        
        AloitaPeli(pallo);
        
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }

    private void LuoKentta()
    {
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
    
    public static PhysicsObject LuoMaila(PhysicsGame peli, double x, double y)
    {
        PhysicsObject maila = PhysicsObject.CreateStaticObject(20.0, 100.0, Shape.Rectangle);
        maila.X = x;
        maila.Y = y;
        maila.Restitution = 1.0;
        peli.Add(maila);
        return maila;
    }
    
    private static void AloitaPeli(PhysicsObject pallo)
    {
        Vector impulssi = new Vector(500.0, 0.0);
        pallo.Hit(impulssi * pallo.Mass);
    }
    private void AsetaOhjaimet(PhysicsObject maila1, PhysicsObject maila2)
    {
        Keyboard.Listen(Key.A, ButtonState.Down, AsetaNopeus, "Pelaaja 1: Liikuta mailaa ylös", maila1, nopeusYlos);
        Keyboard.Listen(Key.A, ButtonState.Released, AsetaNopeus, null, maila1, Vector.Zero);
        Keyboard.Listen(Key.Z,    ButtonState.Down,     AsetaNopeus, "Pelaaja 1: Liikuta mailaa alas", maila1, nopeusAlas);
        Keyboard.Listen(Key.Z,    ButtonState.Released, AsetaNopeus, null,                             maila1, Vector.Zero);

        Keyboard.Listen(Key.Up,   ButtonState.Down,     AsetaNopeus, "Pelaaja 2: Liikuta mailaa ylös", maila2, nopeusYlos);
        Keyboard.Listen(Key.Up,   ButtonState.Released, AsetaNopeus, null,                             maila2, Vector.Zero);
        Keyboard.Listen(Key.Down, ButtonState.Down,     AsetaNopeus, "Pelaaja 2: Liikuta mailaa alas", maila2, nopeusAlas);
        Keyboard.Listen(Key.Down, ButtonState.Released, AsetaNopeus, null,                             maila2, Vector.Zero);
        Keyboard.Listen(Key.F1, ButtonState.Pressed, ShowControlHelp, "Näytä ohjeet");



        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }    
    public static void AsetaNopeus(PhysicsObject maila, Vector nopeus)
    {
        maila.Velocity = nopeus;
    }
    
}
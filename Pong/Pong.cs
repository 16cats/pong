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
        
        LuoKenttaJaAsetaTormaykset(pallo);
        AsetaOhjaimet(maila1, maila2);
        
        AloitaPeli(pallo);
        
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }

    private void LuoKenttaJaAsetaTormaykset(PhysicsObject pallo)
    {
        PhysicsObject vasenReuna = Level.CreateLeftBorder();
        vasenReuna.KineticFriction = 0.0;
        vasenReuna.Restitution = 1.0;
        vasenReuna.IsVisible = false;
        
        PhysicsObject oikeaReuna = Level.CreateRightBorder();
        oikeaReuna.KineticFriction = 0.0;
        oikeaReuna.Restitution = 1.0;
        oikeaReuna.IsVisible = false;
        
        PhysicsObject alaReuna = Level.CreateBottomBorder();
        alaReuna.KineticFriction = 0.0;
        alaReuna.Restitution = 1.0;
        alaReuna.IsVisible = false;
        
        PhysicsObject ylaReuna = Level.CreateTopBorder();
        ylaReuna.KineticFriction = 0.0;
        ylaReuna.Restitution = 1.0;
        ylaReuna.IsVisible = false;
        
        Level.Background.Color = Color.Black;
        
        IntMeter pelaajan1Pisteet = LuoPisteLaskuri(this, Screen.Left + 100.0, Screen.Top - 100.0);
        IntMeter pelaajan2Pisteet = LuoPisteLaskuri(this, Screen.Right - 100.0, Screen.Top - 100.0);
        
        AddCollisionHandler(pallo, oikeaReuna, (_, _) => pelaajan1Pisteet.Value += 1);
        AddCollisionHandler(pallo, vasenReuna, (_, _) => pelaajan2Pisteet.Value += 1);
        
        Camera.ZoomToLevel();
    }
    
    public delegate PhysicsObject Luontifunktio();
    
    public static PhysicsObject LuoReuna(Luontifunktio luontifuktio)
    {
        PhysicsObject reuna = luontifuktio();
        reuna.Restitution = 1.0;
        reuna.KineticFriction = 0.0;
        reuna.IsVisible = false;
        return reuna;
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
    
    public static IntMeter LuoPisteLaskuri(PhysicsGame peli,  double x, double y)
    {
        IntMeter laskuri = new IntMeter(0);
        laskuri.MaxValue = 10;

        Label naytto = new Label();
        naytto.X = x;
        naytto.Y = y;
        naytto.TextColor = Color.White;
        naytto.BorderColor = peli.Level.BackgroundColor;
        naytto.Color = peli.Level.BackgroundColor;
        
        naytto.BindTo(laskuri);
        peli.Add(naytto);

        return laskuri;
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
        Keyboard.Listen(Key.A, ButtonState.Down, AsetaNopeus, "Pelaaja 1: Liikuta mailaa ylös", maila1, nopeusYlos );
        Keyboard.Listen(Key.A, ButtonState.Released, AsetaNopeus, null, maila1, Vector.Zero );
        Keyboard.Listen(Key.Z, ButtonState.Down, AsetaNopeus, "Pelaaja 1: Liikuta mailaa alas", maila1, nopeusAlas );
        Keyboard.Listen(Key.Z, ButtonState.Released, AsetaNopeus, null, maila1, Vector.Zero );
    
        Keyboard.Listen(Key.Up, ButtonState.Down, AsetaNopeus, "Pelaaja 2: Liikuta mailaa ylös", maila2, nopeusYlos );
        Keyboard.Listen(Key.Up, ButtonState.Released, AsetaNopeus, null, maila2, Vector.Zero );
        Keyboard.Listen(Key.Down, ButtonState.Down, AsetaNopeus, "Pelaaja 2: Liikuta mailaa alas", maila2, nopeusAlas );
        Keyboard.Listen(Key.Down, ButtonState.Released, AsetaNopeus, null, maila2, Vector.Zero );
    
        Keyboard.Listen(Key.F1, ButtonState.Pressed, ShowControlHelp, "Näytä ohjeet" );
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli" );
    
        ControllerOne.Listen(Button.DPadUp, ButtonState.Down, AsetaNopeus, "Liikuta mailaa ylös", maila1, nopeusYlos );
        ControllerOne.Listen(Button.DPadUp, ButtonState.Released, AsetaNopeus, null, maila1, Vector.Zero );
        ControllerOne.Listen(Button.DPadDown, ButtonState.Down, AsetaNopeus, "Liikuta mailaa alas", maila1, nopeusAlas );
        ControllerOne.Listen(Button.DPadDown, ButtonState.Released, AsetaNopeus, null, maila1, Vector.Zero );
    
        ControllerTwo.Listen(Button.DPadUp, ButtonState.Down, AsetaNopeus, "Liikuta mailaa ylös", maila2, nopeusYlos );
        ControllerTwo.Listen(Button.DPadUp, ButtonState.Released, AsetaNopeus, null, maila2, Vector.Zero );
        ControllerTwo.Listen(Button.DPadDown, ButtonState.Down, AsetaNopeus, "Liikuta mailaa alas", maila2, nopeusAlas );
        ControllerTwo.Listen(Button.DPadDown, ButtonState.Released, AsetaNopeus, null, maila2, Vector.Zero );
    
        ControllerOne.Listen(Button.Back, ButtonState.Pressed, ConfirmExit, "Lopeta peli" );
        ControllerTwo.Listen(Button.Back, ButtonState.Pressed, ConfirmExit, "Lopeta peli" );
    }
    private void AsetaNopeus(PhysicsObject maila, Vector nopeus)
    {
        if ((nopeus.Y < 0) && (maila.Bottom < Level.Bottom))
        {
            maila.Velocity = Vector.Zero;
            return;
        }
        if ((nopeus.Y > 0) && (maila.Top > Level.Top))
        {
            maila.Velocity = Vector.Zero;
            return;
        }
    
        maila.Velocity = nopeus;
    }
    
    
    
}
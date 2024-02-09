using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Spotlights : IEvent
{
    public IEvent next = null;
    public IEvent Next { set => next = value; }
    private static PointF EndMap = new PointF(
        15 * TileSets.spriteMapSize.Width,
        17 * TileSets.spriteMapSize.Height);
    public static List<PointF> Illuminators = new List<PointF>(){
        new PointF(0, EndMap.Y * .2f),
        new PointF(EndMap.X, EndMap.Y * .2f),
        new PointF(0, (EndMap.Y * .2f) + (EndMap.Y * .8f * .5f)),
        new PointF(EndMap.X, (EndMap.Y * .2f) + (EndMap.Y * .8f * .5f)),
        new PointF(0, EndMap.Y),
        new PointF(EndMap.X, EndMap.Y)
    };
    private static List<PointF> GetIluminators(int quant)
    {
        var list = new List<PointF>();
        for (int i = 0; i < Math.Min(quant, Illuminators.Count); i++)
        {
            list.Add(Illuminators[i].PositionOnCam());
        }

        return list;
    }
    private static int illuminatorsQuant = 0;
    private static float intensityLight = 0f;
    private static DateTime time = DateTime.Now;

    private static Player player = null;
    private static Image molPlaying = SpriteBuffer.Current.Get("src/sprites/bosses/mel-bot/mel-bot-playing-sprites.png");
    private static Entity molEntity = null;
    private static IAnimation molAnimation = null;

    private static float CameraSpeed = Camera.Speed;
    private static float CameraZoom = Camera.Zoom;

    private static bool WaitingClick = false;
    private List<Action> states = new() {
        () => {
            Screen.Filters.Add(new Lighting() { Illuminators = GetIluminators(illuminatorsQuant), Intensity = .72f });
            molEntity.Move(new PointF(EndMap.X * .48f - molEntity.Size.Width / 2, EndMap.Y * .75f - molEntity.Size.Width / 2));
            player.Entity.FocusCam();
            if (illuminatorsQuant > 2)
            {
                state++;
                Camera.Speed = 0.2f;
            }
        },
        () => {
            Screen.Filters.Add(new Lighting() { Illuminators = GetIluminators(illuminatorsQuant), Intensity = .72f });
            Camera.Speed += 0.001f;
            molEntity.FocusCam(inLimit: false);
            if (Camera.Position.Distance(Camera.Destiny) < 300) state++;
        },
        () => {
            Screen.Filters.Add(new WhiteFilter() { Intensity = 255 });
            intensityLight += 30;
            if (intensityLight >= 255) state++;
            Camera.Zoom = 3f;
            molEntity.FocusCam(motion: false, inLimit: false);
        },
        () => {
            molEntity.FocusCam(motion: false, inLimit: false);
            Screen.Filters.Add(new WhiteFilter() { Intensity = 0 });
            intensityLight -= 50;
            if (intensityLight <= 0) state++;
            Sound.StopMusics();
            var s1 = Sound.OpenFrom(SoundType.Music, "src/Sounds/Enemies/MelBot/guitarraMolFase1.wav");
            var s2 = Sound.OpenFrom(SoundType.Music, "src/Sounds/Enemies/MelBot/fase1Music.wav");

            s1.Wait(s2.Play);
            s1.Play();
        },
        () => {
            molEntity.FocusCam(motion: false, inLimit: false);
            molEntity.Talk("Beep-Bop Vou fazer seus últimos momentos serem...\nbeep EXPLÊNDIDOS Boop");
            WaitingClick = true;
        },
        () => {
            player.Entity.FocusCam();
            if (Camera.Position.Distance(Camera.Destiny) < 200) state++;
        },
        () => {
            Camera.Zoom = CameraZoom;
            Camera.Speed = CameraSpeed;
            state++;
        },
        () => {
            molEntity.Animation = molAnimation;
            new Mol() { Entity = molEntity };
            state++;
        }
    };
    private static int state = 0;
    public IEvent OnFrame()
    {
        if (state >= states.Count) return next;
        if (DateTime.Now.Subtract(time).TotalSeconds > 1)
        {
            time = DateTime.Now;
            illuminatorsQuant += 2;
        }
        if (player == null)
        {
            Screen.Filters.Add(new Lighting() { Illuminators = GetIluminators(illuminatorsQuant), Intensity = .72f });
            molEntity = new MolEntity(PointF.Empty);
            molAnimation = molEntity.Animation;
            molEntity.Animation = new StaticAnimation() { Image = molPlaying.Cut(3, 0, 12) };

            foreach (var entity in Memory.Entities)
            {
                if (entity.Mob is Player)
                    player = (Player)entity.Mob;

                if (player != null) break;
            }
            if (player is not null) player.resetMove();
            if (player is not null && !player.tp.IsEmpty)
            {
                player.Entity.Move(player.tp);
                player.tp = PointF.Empty;
                player.Entity.FocusCam(false);
                player.Entity.AddStaticAnimation("marcos/marcos-sprites-old.png", Direction.BottomLeft);
                player.Entity.Animation = player.Entity.Animation.Skip();
            }

            return this;
        }
        states[state].Invoke();
        return this;
    }

    public void OnKeyDown(object o, KeyEventArgs e)
    {

    }

    public void OnKeyUp(object o, KeyEventArgs e)
    {

    }

    public void OnMouseMove(object o, MouseEventArgs e)
    {
        if (WaitingClick && e.Button == MouseButtons.Left)
        {
            state++;
            WaitingClick = false;
        }
    }
}

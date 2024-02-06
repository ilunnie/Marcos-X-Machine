using System;
using System.Collections.Generic;
using System.Drawing;

public class Sprite : IComparable<Sprite>
{
    public SubImage Image { get; set; }
    public List<TextImage> Text { get; set; } = new List<TextImage>();
    public Hitbox Hitbox { get; set; }
    public PointF Position { get; set; }
    public SizeF Size { get; set; }
    public float Angle { get; set; }
    public Anchor Anchor { get; private set; }
    public int Layer { get; set; }
    public int Preference = 0;

    /// <returns>Uma cópia do Sprite</returns>
    public Sprite Clone => new Sprite(Image, Position, Size, Angle, Layer);

    public void Draw(Graphics g)
    {
        if (Image is not null)
            Image.Draw(g, new RectangleF(
                Position, Size
            ));

        foreach (TextImage text in Text)
            text.Draw(g, Position);
    }

    public Sprite(SubImage image, Hitbox hitbox, PointF position, SizeF size, float angle = 0, int layer = 1)
    {
        this.Image = image;
        this.Hitbox = hitbox;
        if (Hitbox is not null) this.Hitbox.Angle = angle;
        this.Position = position;
        this.Size = size;
        this.Angle = angle;
        this.Anchor = new Anchor(
            new PointF(0, 0));
        this.Layer = layer;
    }
    /// <summary>
    /// Objetos que serão desenhados na tela
    /// </summary>
    /// <param name="image">Imagem a ser desenhada</param>
    /// <param name="position">Posição na tela que será desenhada</param>
    /// <param name="size">Tamanho da imagem</param>
    /// <param name="angle">Angulo de rotação da imagem</param>
    public Sprite(SubImage image, PointF position, SizeF size, float angle = 0, int layer = 1)
        : this(image, null, position, size, angle, layer) { }

    /// <summary>
    /// <para>Define a ancora do sprite</para>
    /// <para>Será usada de referencia ao posicionar e rotacionar a imagem</para>
    /// </summary>
    /// <param name="position">Posição da ancora</param>
    /// <param name="screenReference">Define se a ancora deve ser posicionada referente a tela (a partir de <c>x: 0, y:0</c>) ao invés do sprite</param>
    public void SetAnchor(PointF position, bool screenReference = false)
    {
        this.Anchor = new Anchor(position, screenReference);
    }

    /// <summary>
    /// Diz se o sprite <c>s1</c> deve ser desenhado antes de <c>s2</c>
    /// </summary>
    /// <param name="s1">Sprite antes do comparador</param>
    /// <param name="s2">Sprite depois do comparador</param>
    /// <returns><c>true</c> caso ele deva ser desenhado antes</returns>
    public static bool operator <(Sprite s1, Sprite s2)
    {
        return s1 != s2 &&
            s1.Layer < s2.Layer ||
            s2.Preference < s1.Preference ||
            s1.Position.Y < s2.Position.Y;
    }

    /// <summary>
    /// Diz se o sprite <c>s1</c> deve ser desenhado depois de <c>s2</c>
    /// </summary>
    /// <param name="s1">Sprite antes do comparador</param>
    /// <param name="s2">Sprite depois do comparador</param>
    /// <returns><c>true</c> caso ele deva ser desenhado depois</returns>
    public static bool operator >(Sprite s1, Sprite s2)
    {
        return s1 != s2 &&
            s1.Layer > s2.Layer ||
            s2.Preference > s1.Preference ||
            s1.Position.Y > s2.Position.Y;
    }

    /// <summary>
    /// Diz se o sprite <c>s1</c> pode ser desenhado junto ao <c>s2</c>
    /// </summary>
    /// <param name="s1">Sprite antes do comparador</param>
    /// <param name="s2">Sprite depois do comparador</param>
    /// <returns><c>true</c> caso ele deva ser desenhado depois</returns>
    public static bool operator ==(Sprite s1, Sprite s2)
    {
        return
            s1.Layer == s2.Layer &&
            s2.Preference == s1.Preference &&
            s1.Position.Y == s2.Position.Y;
    }

    /// <summary>
    /// Diz se o sprite <c>s1</c> não pode ser desenhado junto ao <c>s2</c>
    /// </summary>
    /// <param name="s1">Sprite antes do comparador</param>
    /// <param name="s2">Sprite depois do comparador</param>
    /// <returns><c>true</c> caso ele deva ser desenhado depois</returns>
    public static bool operator !=(Sprite s1, Sprite s2)
    {
        return
            s1.Layer != s2.Layer ||
            s2.Preference != s1.Preference ||
            s1.Position.Y != s2.Position.Y;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Sprite other = (Sprite)obj;
        return this.Image == other.Image
            && this.Position == other.Position
            && this.Size == other.Size
            && this.Angle == other.Angle
            && this.Anchor == other.Anchor
            && this.Layer == other.Layer
            && this.Preference == other.Preference;
    }

    public override int GetHashCode()
    {
        return Image.GetHashCode()
            ^ Position.GetHashCode()
            ^ Size.GetHashCode()
            ^ Angle.GetHashCode()
            ^ Anchor.GetHashCode()
            ^ Layer.GetHashCode()
            ^ Preference.GetHashCode();
    }

    public int CompareTo(Sprite other)
    {
        int layer = this.Layer - other.Layer;
        int pref = this.Preference - other.Preference;
        int dy = (int)(this.Position.Y - other.Position.Y);

        return dy + 65_000 * (pref + 10 * layer);
    }
}
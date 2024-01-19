using System;
using System.Drawing;

public class Sprite
{
    public Bitmap Image { get; set; }
    public PointF Position { get; set; }
    public SizeF Size { get; set; }
    public float Angle { get; set; }
    public Anchor Anchor { get; private set; }
    public int Layer { get; set; }
    public int Preference = 0;

    /// <summary>
    /// Objetos que serão desenhados na tela
    /// </summary>
    /// <param name="image">Imagem a ser desenhada</param>
    /// <param name="position">Posição na tela que será desenhada</param>
    /// <param name="size">Tamanho da imagem</param>
    /// <param name="angle">Angulo de rotação da imagem</param>
    public Sprite(Bitmap image, PointF position, SizeF size, float angle = 0, int layer = 1 )
    {
        this.Image = image;
        this.Position = position;
        this.Size = size;
        this.Angle = angle;
        this.Anchor = new Anchor(
            new PointF(0, 0));
        this.Layer = layer;
    }

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
        if (s1.Layer > s2.Layer) return false;
        if (s2.Preference > s1.Preference) return false;
        if (s1.Position.Y > s2.Position.Y) return false;

        if (
            s1.Layer == s2.Layer && 
            s1.Preference == s2.Preference && 
            s1.Position.Y == s2.Position.Y
        ) return false;

        return true;
    }

    /// <summary>
    /// Diz se o sprite <c>s1</c> deve ser desenhado depois de <c>s2</c>
    /// </summary>
    /// <param name="s1">Sprite antes do comparador</param>
    /// <param name="s2">Sprite depois do comparador</param>
    /// <returns><c>true</c> caso ele deva ser desenhado depois</returns>
    public static bool operator >(Sprite s1, Sprite s2)
    {
        if (s1.Layer < s2.Layer) return false;
        if (s2.Preference < s1.Preference) return false;
        if (s1.Position.Y < s2.Position.Y) return false;

        if (
            s1.Layer == s2.Layer && 
            s1.Preference == s2.Preference && 
            s1.Position.Y == s2.Position.Y
        ) return false;

        return true;
    }

    /// <summary>
    /// Diz se o sprite <c>s1</c> pode ser desenhado junto ao <c>s2</c>
    /// </summary>
    /// <param name="s1">Sprite antes do comparador</param>
    /// <param name="s2">Sprite depois do comparador</param>
    /// <returns><c>true</c> caso ele deva ser desenhado depois</returns>
    public static bool operator ==(Sprite s1, Sprite s2)
    {
        if (s1 < s2) return false;
        if (s1 > s2) return false;
        return true;
    }

    /// <summary>
    /// Diz se o sprite <c>s1</c> não pode ser desenhado junto ao <c>s2</c>
    /// </summary>
    /// <param name="s1">Sprite antes do comparador</param>
    /// <param name="s2">Sprite depois do comparador</param>
    /// <returns><c>true</c> caso ele deva ser desenhado depois</returns>
    public static bool operator !=(Sprite s1, Sprite s2)
    {
        if (s1 == s2) return false;
        return true;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Sprite other = (Sprite) obj;
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
}
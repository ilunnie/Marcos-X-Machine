using System.Drawing;

public interface IScreen
{
    /// <summary>
    /// Renderiza toda a fila de Sprites na tela
    /// </summary>
    public void Update(Graphics g, Bitmap bmp);

    /// <summary>
    /// Adiciona um Sprite na fila de renderização
    /// </summary>
    /// <param name="sprite">Objeto Sprite a ser adicionado</param>
    public void Add(Sprite sprite);

    /// <summary>
    /// Deleta um Sprite da fila de renderização
    /// </summary>
    /// <param name="sprite">Objeto Sprite a ser deletado</param>
    public void Delete(Sprite sprite);

    /// <summary>
    /// Re-organiza os Sprites da fila
    /// </summary>
    protected void Sort();
}
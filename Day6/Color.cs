public class Color
{
    private int red;
    private int green;
    private int blue;
    private int alpha;

    // Constructor with all 4 components
    public Color(int red, int green, int blue, int alpha)
    {
        this.red = red;
        this.green = green;
        this.blue = blue;
        this.alpha = alpha;
    }

    // Constructor with RGB, defaults alpha to 255 (fully opaque)
    public Color(int red, int green, int blue) : this(red, green, blue, 255) { }

    // Getters and Setters
    public int GetRed() => red;
    public void SetRed(int value) => red = value;

    public int GetGreen() => green;
    public void SetGreen(int value) => green = value;

    public int GetBlue() => blue;
    public void SetBlue(int value) => blue = value;

    public int GetAlpha() => alpha;
    public void SetAlpha(int value) => alpha = value;

    // Grayscale value (average of R, G, B)
    public int GetGrayscale()
    {
        return (red + green + blue) / 3;
    }

    public override string ToString()
    {
        return $"Color(R: {red}, G: {green}, B: {blue}, A: {alpha})";
    }
}
using System;
using Tao.Sdl;

public class Image
{
    public IntPtr Pointer { get; private set; }
    public int width { get; private set; }
    public int height { get; private set; }

    public Image(string imagePath)
    {
        LoadImage(imagePath);
    }

    private void LoadImage(string imagePath)
    {
        Pointer = SdlImage.IMG_Load(imagePath);
        if (Pointer == IntPtr.Zero)
        {
            Console.WriteLine("Imagen inexistente: {0}", imagePath);
            Environment.Exit(4);
        }
        Sdl.SDL_Surface s = System.Runtime.InteropServices.Marshal.PtrToStructure<Sdl.SDL_Surface>(Pointer); //Access surface of this image.
        width = s.w; //Get width and height of image.
        height = s.h;
    }
}
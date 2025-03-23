using System;
using System.Diagnostics;
using System.Net.Quic;
using System.Reflection.Metadata;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;


class cs_fastCounter{
    
    static void Main() {
        System.ConsoleKeyInfo input;
        Console.WriteLine("Welccome to The Counting Benchmark");
        Console.WriteLine("press any key to start benchmark");
        input = Console.ReadKey();
        Console.WriteLine("Benchmarking I/O...");
        List<float> delta = new List<float>();
         var sw = Stopwatch.StartNew();
        long total = 0;
        for (int i = 0; i < 15001; i++){
            Console.WriteLine(i);
            if (sw.ElapsedMilliseconds - total > 0)
            {
                

            delta.Add(sw.ElapsedMilliseconds - total);
            total = total + sw.ElapsedMilliseconds;
            } else {
                delta.Add(0);
            }
            Console.Clear();

            
        }
        Console.Clear();
        float b1score = sw.ElapsedMilliseconds / delta.Average();
        Console.WriteLine("Benchmarking I/O... Done.");
        Console.WriteLine("Benchmarking Random...");
        Random rnd = new Random();
        sw = Stopwatch.StartNew();
        for (int i = 0; i < 101; i++)
        {
            rnd.Next(1, 100000000);
        }
        float b2time = sw.ElapsedMilliseconds;
        Console.Clear();
        Console.WriteLine("Benchmarking I/O... Done.");
        Console.WriteLine("Benchmarking Random... Done");

        using (Game game = new Game(800, 600, "Benchmarking..."))
        {
            game.Run();
        }
        Console.WriteLine("Done in: " + sw.ElapsedMilliseconds / 1000 + " seconds");
        Console.WriteLine("Average time per iteration: " + delta.Average() + " ms"); 
        Console.WriteLine("Max time per iteration: " + delta.Max() + " ms"); 
        Console.WriteLine("Min time per iteration: " + delta.Min() + " ms"); 
        Console.WriteLine("your score is: " + sw.ElapsedMilliseconds / delta.Average());
        Console.WriteLine("Press any key to exit");
        input = Console.ReadKey();
        Environment.Exit(0);
    }
    public class Game : GameWindow
    {
        float[] vertices = {
    -0.5f, -0.5f, 0.0f, //Bottom-left vertex
     0.5f, -0.5f, 0.0f, //Bottom-right vertex
     0.0f,  0.5f, 0.0f  //Top vertex
};
int VertexArrayObject;
int VertexBufferObject;
        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title })
        {
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

GL.BindVertexArray(VertexArrayObject);
GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
GL.EnableVertexAttribArray(0);

GL.BindVertexArray(VertexArrayObject);
// 2. copy our vertices array in a buffer for OpenGL to use
GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
// 3. then set our vertex attributes pointers
GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
GL.EnableVertexAttribArray(0);

GL.BindVertexArray(VertexArrayObject);

        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }

protected override void OnRenderFrame(FrameEventArgs e)
{
    base.OnRenderFrame(e);

    GL.Clear(ClearBufferMask.ColorBufferBit);

    //Code goes here.

    SwapBuffers();
}
        protected override void OnLoad()
{
    base.OnLoad();
    
    VertexArrayObject = GL.GenVertexArray();

    GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

    //Code goes here
}
protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
{
    base.OnFramebufferResize(e);

    GL.Viewport(0, 0, e.Width, e.Height);
}

    }
    public class Shader
{
    int Handle;

    public Shader(string vertexPath, string fragmentPath)

    {
        int VertexShader;
        int FragmentShader;
        string VertexShaderSource = File.ReadAllText(vertexPath);

string FragmentShaderSource = File.ReadAllText(fragmentPath);

VertexShader = GL.CreateShader(ShaderType.VertexShader);
GL.ShaderSource(VertexShader, VertexShaderSource);

FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
GL.ShaderSource(FragmentShader, FragmentShaderSource);

GL.CompileShader(VertexShader);

GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int success);
if (success == 0)
{
    string infoLog = GL.GetShaderInfoLog(VertexShader);
    Console.WriteLine(infoLog);
}

GL.CompileShader(FragmentShader);

GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out success);
if (success == 0)
{
    string infoLog = GL.GetShaderInfoLog(FragmentShader);
    Console.WriteLine(infoLog);
}

Handle = GL.CreateProgram();

GL.AttachShader(Handle, VertexShader);
GL.AttachShader(Handle, FragmentShader);

GL.LinkProgram(Handle);

GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out success);
if (success == 0)
{
    string infoLog = GL.GetProgramInfoLog(Handle);
    Console.WriteLine(infoLog);
}
    }
    public void Use()
{
    GL.UseProgram(Handle);
}
private bool disposedValue = false;

protected virtual void Dispose(bool disposing)
{
    if (!disposedValue)
    {
        GL.DeleteProgram(Handle);

        disposedValue = true;
    }
}

~Shader()
{
    if (disposedValue == false)
    {
        Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
    }
}


public void Dispose()
{
    Dispose(true);
    GC.SuppressFinalize(this);
}

}

}
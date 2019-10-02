using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


public class Camera : GameComponent
{
    public Matrix view { get; protected set; }
    public Matrix projection { get; protected set; }

    public Vector3 cameraPosition { get; protected set; }
    public Vector3 cameraDirection;
    public Vector3 cameraUp;

    //defines speed of camera movement
    float speed = 0.5F;

    MouseState prevMouseState;

    public Camera(Game game, Vector3 pos, Vector3 target, Vector3 up)
        : base(game)
    {
        // TODO: Construct any child components here

        // Build camera view matrix
        cameraPosition = pos;
        cameraDirection = target - pos;
        cameraDirection.Normalize();
        cameraUp = up;
        CreateLookAt();

        projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)Game.Window.ClientBounds.Width / (float)Game.Window.ClientBounds.Height, 1, 100);
    }

    /// <summary>
    /// Allows the game component to perform any initialization it needs to before starting
    /// to run.  This is where it can query for any required services and load content.
    /// </summary>
    public override void Initialize()
    {
        // TODO: Add your initialization code here

        // Set mouse position and do initial get state
        Mouse.SetPosition(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2);

        prevMouseState = Mouse.GetState();

        base.Initialize();
    }

    /// <summary>
    /// Allows the game component to update itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    public override void Update(GameTime gameTime)
    {
        // TODO: Add your update code here


        // Move forward and backward

        if (Keyboard.GetState().IsKeyDown(Keys.W))
            cameraPosition += cameraDirection * speed;
        if (Keyboard.GetState().IsKeyDown(Keys.S))
            cameraPosition -= cameraDirection * speed;

        if (Keyboard.GetState().IsKeyDown(Keys.A))
            cameraPosition += Vector3.Cross(cameraUp, cameraDirection) * speed;
        if (Keyboard.GetState().IsKeyDown(Keys.D))
            cameraPosition -= Vector3.Cross(cameraUp, cameraDirection) * speed;


        // Rotation in the world
        cameraUp = new Vector3(0, -1, 0);


        float deltaX, deltaY;

        if(Mouse.GetState() != prevMouseState)
        {
            var currentState = Mouse.GetState();

            deltaX = currentState.X - (Game.GraphicsDevice.Viewport.Width / 2);
            deltaY = currentState.Y - (Game.GraphicsDevice.Viewport.Height / 2);


        }


        // Reset prevMouseState
        prevMouseState = Mouse.GetState();

        CreateLookAt();

        base.Update(gameTime);
    }

   


    private void CreateLookAt()
    {
        view = Matrix.CreateLookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
    }

}

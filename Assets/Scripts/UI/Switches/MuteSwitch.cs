using System;

public class MuteSwitch : Switch
{
    public Action<bool> Muted;

    protected override void Display()
    {
        base.Display();

        Mute();
    }

    private void Mute()
    {
        Muted?.Invoke(Position);
    }
}

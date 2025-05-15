using System;
using YG;

public class MuteSwitch : Switch
{
    private void Awake()
    {
        Position = YG2.saves.Muted;
        
        Display();
    }
}

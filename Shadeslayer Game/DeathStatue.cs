using Godot;
using System;

public partial class DeathStatue : AnimatedSprite2D
{
    bool lighting = false;
    bool done = false;

    public override void _Ready()
    {
        Play("unlit");
    }

    
  

    private void _on_animation_finished()
    {
        if (lighting == true)
        {
            lighting = false;
            Play("lit");
        }
    }

    public override void _Process(double delta)
    {
        var deathlight = GetNode<global_dialogue>("/root/GlobalDialogue");
        if (deathlight.death_activated_light == true)
        {
            if (done == false)
            {
                Play("lighting");
                lighting = true;
                done = true;
            }
        }
    }




}

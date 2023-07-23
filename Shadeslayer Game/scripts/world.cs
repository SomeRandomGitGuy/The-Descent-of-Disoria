using Godot;
using System;

public partial class world : Node2D
{
    public override void _Process(double delta)
    {
        var found = GetNode<global_dialogue>("/root/GlobalDialogue").found_orb;
        if (found == true)
        {
            var orb = GetNode<StaticBody2D>("death_orb");
            orb.Free();
        }

    }

   
}

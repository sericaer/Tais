using Godot;
using System;

namespace TaisGodot.Scripts
{
	public class TaishouDetail : Panel
	{
		public const string path = "res://Scenes/Main/Imp/Taishou/TaishouDetail.tscn";

		new Label Name;
		Label Party;

		Tais.Run.ITaishou gmobj;

		//ReactiveLabel Age;


		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			

			Name = GetNode<Label>("CenterContainer/PanelContainer/HBoxContainer/RightContainer/VBoxContainer/HBoxContainer/VBoxContainer/Name/Value");
			Party = GetNode<Label>("CenterContainer/PanelContainer/HBoxContainer/RightContainer/VBoxContainer/HBoxContainer/VBoxContainer/Party/Value");
			//Age = GetNode<ReactiveLabel>("CenterContainer/PanelContainer/HBoxContainer/RightContainer/VBoxContainer/HBoxContainer/VBoxContainer/Age/Value");

			gmobj = Tais.GMRoot.runner.taishou;
			GD.Print(gmobj.name, gmobj.party, gmobj.age);

			Name.Text = gmobj.name;
			Party.Text = gmobj.party;

			//Age.Assoc(GMRoot.runner.taishou.OBSProperty(x=>x.age));
		}
	}
}

using UnityEngine;
using chibi.controller.npc;
using System.Collections.Generic;
using platformer.controller.platform;
using UnityEngine.InputSystem;
using osfera.controller.npc;
using helper;

namespace osfera.controller.player
{
	public class Osfera_player_ui_controller : Osfera_player_controller
	{
		//public metroidvania.grid.ui.Grid_ui grid_ui;

		public Transform debug_shit;

		protected Vector2 _mouse_axis;

		public override Vector2 mouse_position
		{
			get {
				return _mouse_axis;
			}
			set {
				_mouse_axis = value;
			}
		}

		protected override void _init_cache()
		{
			base._init_cache();
		}

		public override void action( string name, string e )
		{
			base.action( name, e );
			switch ( name )
			{
				case "fire":
					debug_action( name, e );
					break;
				default:
					debug_action( name, e );
					break;
			}
		}

		protected void Update()
		{
		}
	}
}
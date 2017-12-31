﻿using HamstarHelpers.BuffHelpers;
using HamstarHelpers.PlayerHelpers;
using HamstarHelpers.TmlHelpers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;


namespace TimeLimit.Logic {
	partial class TimerLogic {
		private void RunAction( TimeLimitMod mymod, string action, bool is_loop ) {
			switch( action ) {
			case "none":
				if( !is_loop ) {
					Main.NewText( "Time's up.", Color.Red );
				} else {
					Main.NewText( "Timer restarting.", Color.Yellow );
				}
				break;
			case "exit":
				Main.NewText( "Time's up. Bye!", Color.Red );
				if( Main.netMode != 2 ) {
					TmlHelpers.ExitToMenu();
				}
				break;
			case "kill":
				if( Main.netMode != 2 ) {   // Not server
					for( int i = 0; i < Main.player.Length; i++ ) {
						Player player = Main.player[i];
						if( player == null || !player.active || player.dead ) { continue; }

						player.KillMe( PlayerDeathReason.ByCustomReason( "Time's up." ), 9999, 0 );
					}
				}
				break;
			case "hardkill":
				if( Main.netMode != 2 ) {   // Not server
					for( int i = 0; i < Main.player.Length; i++ ) {
						Player player = Main.player[i];
						if( player == null || !player.active || player.dead ) { continue; }

						PlayerHelpers.KillWithPermadeath( player, "Time's up. Game over." );
					}
				}
				break;
			case "afflict":
				var afflictions = string.Join( ",", mymod.Config.Afflictions );

				if( !is_loop ) {
					Main.NewText( "Time's up. You now have the following: " + afflictions, Color.Red );
				} else {
					Main.NewText( "Timer restarting. You now have the following: " + afflictions, Color.Yellow );
				}
				this.ApplyAffliction( mymod );
				break;
			case "unafflict":
				if( !is_loop ) {
					Main.NewText( "Time's up. All afflictions removed.", Color.Red );
				} else {
					Main.NewText( "Timer restarting. All afflictions removed.", Color.Yellow );
				}
				this.RemoveAffliction( mymod );
				break;
			default:
				var myworld = mymod.GetModWorld<TimeLimitWorld>();
				
				if( !myworld.Logic.TimesUpHooks.ContainsKey(action) ) {
					ErrorLogger.Log( "No such time's up event by name " + action );
					break;
				}
				myworld.Logic.TimesUpHooks[action]();
				break;
			}
		}


		public void ApplyAffliction( TimeLimitMod mymod ) {
			foreach( string affliction in mymod.Config.Afflictions ) {
				if( !BuffHelpers.BuffIdsByName.ContainsKey(affliction) ) {
					ErrorLogger.Log( "No such afflication ((de)buff) by name " + affliction );
					continue;
				}

				for( int i=0; i<Main.player.Length; i++ ) {
					Player player = Main.player[i];
					if( player == null || !player.active ) { continue; }
					
					int buff_id = BuffHelpers.BuffIdsByName[affliction];
					BuffHelpers.AddPermaBuff( player, buff_id );
				}
			}
		}
		
		public void RemoveAffliction( TimeLimitMod mymod ) {
			foreach( string affliction in mymod.Config.Afflictions ) {
				if( !BuffHelpers.BuffIdsByName.ContainsKey( affliction ) ) {
					ErrorLogger.Log( "No such afflication ((de)buff) by name " + affliction );
					continue;
				}

				for( int i = 0; i < Main.player.Length; i++ ) {
					Player player = Main.player[i];
					if( player == null || !player.active ) { continue; }

					int buff_id = BuffHelpers.BuffIdsByName[affliction];
					BuffHelpers.RemovePermaBuff( player, buff_id );
				}
			}
		}
	}
}

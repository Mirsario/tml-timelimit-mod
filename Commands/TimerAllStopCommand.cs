﻿using HamstarHelpers.Helpers.DebugHelpers;
using Terraria;
using Terraria.ModLoader;
using TimeLimit.NetProtocol;


namespace TimeLimit.Commands {
	class TimerAllStopCommand : ModCommand {
		public override CommandType Type {
			get {
				if( Main.netMode == 0 && !Main.dedServ ) {
					return CommandType.World;
				}
				return CommandType.Console;
			}
		}
		public override string Command { get { return "timer-stop-all"; } }
		public override string Usage { get { return "/"+this.Command; } }
		public override string Description { get { return "Stops all running timers."; } }


		////////////////

		public override void Action( CommandCaller caller, string input, string[] args ) {
			var mymod = (TimeLimitMod)this.mod;
			var myworld = this.mod.GetModWorld<TimeLimitWorld>();
			myworld.Logic.StopAllTimers();

			if( Main.netMode == 2 ) {
				SendPackets.SendStopAllTimersCommand( (TimeLimitMod)this.mod, -1 );
			}

			caller.Reply( "Timers stopped." );

			if( mymod.Config.DebugModeInfo ) {
				LogHelpers.Log( "TimeLimit.TimerAllStopCommand.Action - Success." );
			}
		}
	}
}

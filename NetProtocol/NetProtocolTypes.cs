﻿namespace TimeLimit.NetProtocol {
	public enum TimeLimitProtocolTypes : byte {
		RequestModSettings,
		ModSettings,
		RequestTimers,
		TimerStart,
		TimersStop,
		TimersPause,
		TimersResume,
		TimersAllStop,
		TimersAllPause,
		TimersAllResume
	}
}

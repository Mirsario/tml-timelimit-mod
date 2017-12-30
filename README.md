# Time Limit

Time Limit does what it says on the tin. Worlds can now be set with time limits to have configurable events take place. For example, to run a timer for 5 minutes that exits the game on completion, type this into the chat or server console:

  `/timelimit begin 300 exit false`

List of commands:

* `begin <seconds> <action> <repeat>` - Begins the timer with the given number of seconds before the given action is executed.
* `end` - Aborts all currently running timers.

List of actions:

* `none` - No action. Timer exists for show only.
* `exit` - Exits the server/returns to menu.
* `kill` - All players die.
* `hardkill` - All players die as if in hardcore mode (i.e. permadeath).
* `afflict` - All players receive a permanent affliction as specified in the config file's 'AfflictDebuffs' debuff list.
* <custom action name> - Same as `none`, unless another mod implements the named action via. API.

See the Hamstar Helpers Control Panel for configuration options.
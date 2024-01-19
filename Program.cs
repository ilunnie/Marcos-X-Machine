var mode = args.Length < 1 ? "" : args[0];
Memory.Mode = mode;

var game = new Game();
game.Run();
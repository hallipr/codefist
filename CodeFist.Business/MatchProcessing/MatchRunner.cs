namespace CodeFist.Business.MatchProcessing
{
    using System.Diagnostics;
    using System.Linq;
    using Core.Entities;
    using Microsoft.ClearScript.V8;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class MatchRunner
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings{ContractResolver = new CamelCasePropertyNamesContractResolver()};

        public static Match RunMatch(Game game, Bot[] bots)
        {
            using (var engine = new V8ScriptEngine())
            {
                var stopwatch = new Stopwatch();
                engine.Script.performance = new { stopwatch };

                engine.Execute("var game = " + JsonBounce(new { game.Id, Source = game.GameSource }));
                engine.Execute("var bots = " + JsonBounce(bots.Select(b => new { b.BotId, b.Source })));
                engine.Execute(@"
			        var getConstructor = function(script, constructorName) {
				        var constructorCreator = new Function('global', 'return function(){' + script + '; return ' + constructorName + ';}.call(global);');
				        return constructorCreator({});
			        };");

                engine.Execute(@"
			        try {
				        game.constructor = getConstructor(game.source, 'Game');
			        } catch (_error) {
				        _error.message = 'Error loading game script: ' + game.id + '\n' + _error.message;
				        throw _error;
			        }
			
			        bots.forEach(function(bot) {
				        try {
					        return bot.constructor = getConstructor(bot.source, 'Player');
				        } catch (_error) {
					        _error.message = 'Error loading bot script: ' + bot.botId + '\n' + _error.message;
					        throw _error;
				        }
			        });
		
			        game.instance = new game.constructor();
                    
                    var results = {};

                    var playGame = function() {
                        performance.stopwatch.Start();

                        game.instance.play(bots, function(players, log) {
                        	performance.stopwatch.Stop();
                            results.players = players;
                            results.log = JSON.stringify(log);
                        });

                        return JSON.stringify(results);
                    };
                ");

                var rawResults = (string) engine.Script.playGame();
                var results = JsonConvert.DeserializeObject<MatchResult>(rawResults);

                var matchPlayers = bots.Select(b => new MatchPlayer {GameId = b.GameId, BotId = b.BotId, Bot = b}).ToArray();
                
                foreach (var player in results.Players)
                {
                    var matchPlayer = matchPlayers.First(p => p.BotId == player.BotId);
                    matchPlayer.PrivateLog = player.Log;
                    matchPlayer.Winner = player.Winner;
                }

                var match = new Match
                {
                    Game = game,
                    MatchPlayers = matchPlayers,
                    Log = results.Log,
                    Duration = stopwatch.Elapsed
                };

                return match;
            }
        }

        private static string JsonBounce<T>(T value)
        {
            var stringValue = JsonConvert.SerializeObject(value, JsonSerializerSettings);
            var jsonBounce = string.Format("JSON.parse({0})", JsonConvert.SerializeObject(stringValue, JsonSerializerSettings));
            return jsonBounce;
        }
    }
}
(function() {
  var codefist;



  codefist = angular.module('codefist');

  codefist.controller('LocalFightCtrl', [
    '$scope', 'gameService', 'botService', '$routeParams', '$q', function($scope, gameService, botService, $routeParams, $q) {
      var botIds, botRequests, gameRequest, getConstructor;
      $scope.gameId = $routeParams.gameId;
      $scope.players = [];
      botIds = $routeParams.botIds.split(',');
      $scope.settings = {
        refreshRate: 1000 / 60,
        speed: 1
      };
      gameRequest = gameService.get($scope.gameId).then(function(response) {
        return $scope.game = response.data;
      });
      botRequests = botIds.map(function(botId, index) {
        return botService.get($scope.gameId, botId).then(function(response) {
          return $scope.players[index] = response.data;
        });
      });
      $q.all([gameRequest].concat(botRequests)).then(function(responses) {
        var Game, bot, gameInstance, gameSource, group, groupedBots, i, players, start, visualizerSource, _i, _j, _len, _len1;
        groupedBots = {};
        $scope.players.forEach(function(b) {
          if (!groupedBots.hasOwnProperty(b.botId)) {
            groupedBots[b.botId] = [];
          }
          return groupedBots[b.botId].push(b);
        });
        for (_i = 0, _len = groupedBots.length; _i < _len; _i++) {
          group = groupedBots[_i];
          if (group.length > 1) {
            for (i = _j = 0, _len1 = group.length; _j < _len1; i = ++_j) {
              bot = group[i];
              bot.botId += " (" + i + ")";
            }
          }
        }
        gameSource = $scope.game.gameSource;
        visualizerSource = $scope.game.visualizerSource;
        Game = getConstructor(gameSource, "Game");
        gameInstance = new Game();
        players = $scope.players.map(function(s) {
          return {
            botId: s.botId,
            constructor: getConstructor(s.source, 'Player')
          };
        });
        start = performance.now();
        return gameInstance.play(players, function(playersResults, log) {
          var Visualizer, visualizerInstance;
          log.elapsed = performance.now() - start;
          Visualizer = getConstructor(visualizerSource, "Visualizer");
          visualizerInstance = new Visualizer(log, document.getElementById("playback"), $scope.settings);
          return visualizerInstance.play();
        });
      });
      return getConstructor = function(script, constructorName) {
        var constructorCreator;
        constructorCreator = new Function('global', "return function(){" + script + "; return " + constructorName + ";}.call(global);");
        return constructorCreator({});
      };
    }
  ]);

}).call(this);

(function() {
  var codefist;



  codefist = angular.module('codefist');

  codefist.controller('MatchReplayCtrl', [
    '$scope', 'matchService', '$routeParams', '$q', function($scope, matchService, $routeParams, $q) {
      var getConstructor, matchRequest;
      $scope.matchId = $routeParams.matchId;
      $scope.settings = {
        refreshRate: 1000 / 60,
        speed: 1
      };
      matchRequest = matchService.get($scope.matchId).then(function(response) {
        var Visualizer, log, match, visualizerInstance, visualizerSource;
        match = response.data;
        $scope.game = match.game;
        $scope.players = match.players;
        log = JSON.parse(match.log);
        visualizerSource = $scope.game.visualizerSource;
        Visualizer = getConstructor(visualizerSource, "Visualizer");
        visualizerInstance = new Visualizer(log, document.getElementById("playback"), $scope.settings);
        return visualizerInstance.play();
      });
      return getConstructor = function(script, constructorName) {
        var constructorCreator;
        constructorCreator = new Function('global', "return function(){" + script + "; return " + constructorName + ";}.call(global);");
        return constructorCreator({});
      };
    }
  ]);

}).call(this);

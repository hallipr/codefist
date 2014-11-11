(function() {
  var codefist;



  codefist = angular.module('codefist');

  codefist.controller('GameDetailCtrl', [
    '$scope', '$modal', '$routeParams', '$location', 'session', 'gameService', 'botService', 'matchService', function($scope, $modal, $routeParams, $location, session, gameService, botService, matchService) {
      var onlyUnique;
      $scope.gameId = $routeParams.gameId;
      gameService.get($scope.gameId).then(function(response) {
        return $scope.game = response.data;
      });
      botService.queryByGame($scope.gameId).then(function(response) {
        var bots;
        bots = response.data;
        $scope.bots = bots;
        return $scope.fightBots = [
          {
            botId: bots[Math.floor(Math.random() * bots.length)].botId
          }, {
            botId: bots[Math.floor(Math.random() * bots.length)].botId
          }
        ];
      });
      matchService.queryByGame($scope.gameId).then(function(response) {
        return $scope.matches = response.data;
      });
      session.initializeScope($scope);
      $scope.scripts = [
        {
          name: 'gameSource',
          label: 'Game Source'
        }, {
          name: 'botSource',
          label: 'Sample Bot Source'
        }, {
          name: 'visualizerSource',
          label: 'Visualizer Source'
        }
      ];
      $scope.activeScriptName = '';
      $scope.editorOptions = {
        mode: 'javascript',
        indentUnit: 4,
        tabMode: 'spaces',
        lineNumbers: true,
        lint: true,
        foldGutter: true,
        gutters: ['CodeMirror-linenumbers', 'CodeMirror-foldgutter', 'CodeMirror-lint-markers']
      };
      $scope.saveChanges = function() {
        return gameService.update($scope.game);
      };
      $scope.editScript = function(name) {
        return $scope.activeScriptName = $scope.activeScriptName === name ? null : name;
      };
      $scope.tabClass = function(name) {
        if ($scope.activeScriptName === name) {
          return 'btn-primary';
        } else {
          return 'btn-default';
        }
      };
      $scope.createBot = function() {
        var modalInstance;
        modalInstance = $modal.open({
          templateUrl: '/app/views/dialogs/create-bot.html',
          controller: 'CreateBotCtrl'
        });
        return modalInstance.result.then(function(bot) {
          return $location.path("games/" + bot.gameId + "/bots/" + bot.botId);
        });
      };
      $scope.fightRanked = function() {
        var botIds;
        botIds = $scope.fightBots.map(function(b) {
          return b.botId;
        });
        return matchService.create($scope.gameId, botIds).then(function(response) {
          return $location.path("/matches/" + response.data);
        });
      };
      $scope.fightLocal = function() {
        var botIds;
        botIds = $scope.fightBots.map(function(b) {
          return b.botId;
        });
        return $location.path("/games/" + $scope.gameId + "/fight/" + (botIds.join()));
      };
      onlyUnique = function(value, index, self) {
        return self.indexOf(value) === index;
      };
      return $scope.canFightRanked = function() {
        return ($scope.fightBots != null) && $scope.fightBots.map(function(b) {
          return b.botId;
        }).filter(onlyUnique).length === $scope.fightBots.length;
      };
    }
  ]);

}).call(this);

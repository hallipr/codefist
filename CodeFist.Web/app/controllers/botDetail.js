(function() {
  var codefist;



  codefist = angular.module('codefist');

  codefist.controller('BotDetailCtrl', [
    '$scope', '$routeParams', '$location', 'botService', 'gameService', function($scope, $routeParams, $location, botService, gameService) {
      botService.get($routeParams.gameId, $routeParams.botId).then(function(response) {
        return $scope.bot = response.data;
      });
      gameService.get($routeParams.gameId).then(function(response) {
        return $scope.game = response.data;
      });
      $scope.editorOptions = {
        mode: "javascript",
        lineNumbers: true,
        foldGutter: true,
        gutters: ['CodeMirror-linenumbers', 'CodeMirror-foldgutter', 'CodeMirror-lint-markers'],
        lint: true
      };
      $scope.saveChanges = function() {
        return botService.update($scope.bot);
      };
      $scope.basicEditor = false;
      return $scope.lineCount = function(value) {
        return value.split('\n').length;
      };
    }
  ]);

}).call(this);

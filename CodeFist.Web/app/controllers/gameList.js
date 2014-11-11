(function() {
  var codefist;



  codefist = angular.module('codefist');

  codefist.controller('GameListCtrl', [
    '$scope', '$modal', '$location', 'session', 'gameService', function($scope, $modal, $location, session, gameService) {
      session.initializeScope($scope);
      gameService.query().then(function(response) {
        return $scope.games = response.data;
      });
      return $scope.createGame = function() {
        var modalInstance;
        modalInstance = $modal.open({
          templateUrl: '/app/views/dialogs/create-game.html',
          controller: 'CreateGameCtrl'
        });
        return modalInstance.result.then(function(game) {
          return $location.path("games/" + game.id);
        });
      };
    }
  ]);

}).call(this);

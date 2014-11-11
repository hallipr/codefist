(function() {
  var codefist;



  codefist = angular.module('codefist');

  codefist.controller('CreateGameCtrl', [
    '$scope', '$modalInstance', 'gameService', function($scope, $modalInstance, gameService) {
      $scope.game = {
        name: ''
      };
      $scope.ok = function() {
        return gameService.create($scope.game.name).then(function(response) {
          return $modalInstance.close(response.data);
        }, function(response) {
          var _ref;
          return $scope.error = (response != null ? (_ref = response.data) != null ? _ref.message : void 0 : void 0) || "Error";
        });
      };
      return $scope.cancel = function() {
        return $modalInstance.dismiss('cancel');
      };
    }
  ]);

}).call(this);

(function() {
  var codefist;



  codefist = angular.module('codefist');

  codefist.controller('CreateBotCtrl', [
    '$scope', '$modalInstance', '$routeParams', 'botService', function($scope, $modalInstance, $routeParams, botService) {
      $scope.bot = {
        name: ''
      };
      $scope.ok = function() {
        return botService.create($routeParams.gameId, $scope.bot.name).then(function(response) {
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

(function() {
  var codefist;



  codefist = angular.module('codefist');

  codefist.controller('LoginCtrl', [
    '$scope', 'session', function($scope, session) {
      session.initializeScope($scope);
      $scope.login = function() {
        return session.login();
      };
      return $scope.logout = function() {
        return session.logout();
      };
    }
  ]);

}).call(this);

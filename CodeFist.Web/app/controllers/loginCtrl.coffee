codefist = angular.module 'codefist'

codefist.controller 'LoginCtrl', ['$scope', 'session', ($scope, session) ->
    session.initializeScope($scope)

    $scope.login = -> session.login()
    $scope.logout = -> session.logout()    
]
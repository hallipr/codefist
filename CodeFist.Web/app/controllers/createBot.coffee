codefist = angular.module 'codefist'

codefist.controller 'CreateBotCtrl', ['$scope', '$modalInstance', '$routeParams', 'botService', ($scope, $modalInstance, $routeParams, botService) ->
    $scope.bot = { name: '' };

    $scope.ok = -> 
        botService.create($routeParams.gameId, $scope.bot.name)
            .then(
                (response) -> $modalInstance.close(response.data),
                (response) -> $scope.error = response?.data?.message || "Error"
            )
  
    $scope.cancel = -> $modalInstance.dismiss('cancel')
]
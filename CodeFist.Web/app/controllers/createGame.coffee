codefist = angular.module 'codefist'

codefist.controller 'CreateGameCtrl', ['$scope', '$modalInstance', 'gameService', ($scope, $modalInstance, gameService) ->
    $scope.game = { name: '' };

    $scope.ok = -> 
        gameService.create($scope.game.name)
            .then(
                (response) -> $modalInstance.close(response.data),
                (response) -> $scope.error = response?.data?.message || "Error"
            )

    $scope.cancel = -> $modalInstance.dismiss('cancel')
]
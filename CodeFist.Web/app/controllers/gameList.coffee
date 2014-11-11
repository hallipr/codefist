codefist = angular.module 'codefist'

codefist.controller 'GameListCtrl', ['$scope', '$modal', '$location','session', 'gameService', ($scope, $modal, $location, session, gameService) -> 
    session.initializeScope($scope);

    gameService.query()
       .then((response) -> $scope.games = response.data)    

    $scope.createGame = ->
        modalInstance = $modal.open
            templateUrl: '/app/views/dialogs/create-game.html'
            controller: 'CreateGameCtrl'            

        modalInstance.result.then((game) -> $location.path("games/#{game.id}"))
]
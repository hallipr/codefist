codefist = angular.module 'codefist'

codefist.controller 'MatchReplayCtrl', ['$scope', 'matchService', '$routeParams', '$q', ($scope, matchService, $routeParams, $q) ->
    $scope.matchId = $routeParams.matchId

    $scope.settings = refreshRate: 1000 / 60, speed: 1

    matchRequest = matchService.get($scope.matchId)
        .then((response) -> 
            match = response.data
            $scope.game = match.game
            $scope.players = match.players

            log = JSON.parse(match.log)
            visualizerSource = $scope.game.visualizerSource
            Visualizer = getConstructor(visualizerSource, "Visualizer")
            visualizerInstance = new Visualizer(log, document.getElementById("playback"), $scope.settings)
            visualizerInstance.play()            
        )

    getConstructor = (script, constructorName) ->
        constructorCreator = new Function('global', "return function(){#{script}; return #{constructorName};}.call(global);")
        return constructorCreator({})
]
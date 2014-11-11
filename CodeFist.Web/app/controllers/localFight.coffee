codefist = angular.module 'codefist'

codefist.controller 'LocalFightCtrl', ['$scope', 'gameService', 'botService', '$routeParams', '$q', ($scope, gameService, botService, $routeParams, $q) ->
    $scope.gameId = $routeParams.gameId
    $scope.players = []
    botIds = $routeParams.botIds.split(',')
    $scope.settings = refreshRate: 1000 / 60, speed: 1

    gameRequest = gameService.get($scope.gameId).then((response) -> $scope.game = response.data)
    botRequests = botIds.map((botId, index) ->  botService.get($scope.gameId, botId).then((response) -> $scope.players[index] = response.data))

    $q.all([gameRequest].concat(botRequests))
        .then((responses) ->            
            groupedBots = {}

            $scope.players.forEach((b) -> 
                if !groupedBots.hasOwnProperty(b.botId) then groupedBots[b.botId] = []
                groupedBots[b.botId].push(b)
            )

            for group in groupedBots when group.length > 1
                for bot, i in group
                    bot.botId += " (#{i})"

            gameSource = $scope.game.gameSource
            visualizerSource = $scope.game.visualizerSource

            Game = getConstructor(gameSource, "Game")
            gameInstance = new Game()

            players = $scope.players.map (s) ->
                botId: s.botId
                constructor: getConstructor(s.source, 'Player')

            start = performance.now()
            gameInstance.play(players, (playersResults, log) -> 
                log.elapsed = performance.now() - start
                Visualizer = getConstructor(visualizerSource, "Visualizer")
                visualizerInstance = new Visualizer(log, document.getElementById("playback"), $scope.settings)
                visualizerInstance.play()
            )
        )

    getConstructor = (script, constructorName) ->
        constructorCreator = new Function('global', "return function(){#{script}; return #{constructorName};}.call(global);")
        return constructorCreator({})
]
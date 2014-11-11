codefist = angular.module 'codefist'

codefist.controller 'GameDetailCtrl', ['$scope', '$modal', '$routeParams', '$location', 'session', 'gameService', 'botService', 'matchService', ($scope, $modal, $routeParams, $location, session, gameService, botService, matchService) ->
    $scope.gameId = $routeParams.gameId        
    
    gameService.get($scope.gameId)
        .then((response) -> $scope.game = response.data)

    botService.queryByGame($scope.gameId)
        .then((response) -> 
            bots = response.data
            $scope.bots = bots
            $scope.fightBots = [
                { botId: bots[Math.floor(Math.random()*bots.length)].botId }
                { botId: bots[Math.floor(Math.random()*bots.length)].botId }
            ]
        )

    matchService.queryByGame($scope.gameId)
        .then((response) -> $scope.matches = response.data);

    session.initializeScope($scope);

    $scope.scripts = [
        { name:'gameSource', label:'Game Source' }
        { name:'botSource', label:'Sample Bot Source' }
        { name:'visualizerSource', label:'Visualizer Source' }
    ]

    $scope.activeScriptName = ''

    $scope.editorOptions =
        mode: 'javascript'
        indentUnit: 4
        tabMode: 'spaces'        
        lineNumbers: true
        lint: true
        foldGutter: true
        gutters: ['CodeMirror-linenumbers', 'CodeMirror-foldgutter', 'CodeMirror-lint-markers']
    
    $scope.saveChanges = -> gameService.update($scope.game)

    $scope.editScript = (name) -> $scope.activeScriptName = if $scope.activeScriptName == name then null else name

    $scope.tabClass = (name) -> if $scope.activeScriptName == name then 'btn-primary' else 'btn-default'  

    $scope.createBot = ->
        modalInstance = $modal.open
            templateUrl: '/app/views/dialogs/create-bot.html'
            controller: 'CreateBotCtrl'    

        modalInstance.result.then((bot) -> $location.path("games/#{bot.gameId}/bots/#{bot.botId}"))

    $scope.fightRanked = -> 
        botIds = $scope.fightBots.map((b)->b.botId)
        matchService.create($scope.gameId, botIds)
            .then((response) -> $location.path("/matches/#{response.data}"))
    
    $scope.fightLocal = -> 
        botIds = $scope.fightBots.map((b)->b.botId)
        $location.path("/games/#{$scope.gameId}/fight/#{botIds.join()}")

    onlyUnique = (value, index, self) -> self.indexOf(value) == index

    $scope.canFightRanked = -> $scope.fightBots? and $scope.fightBots.map((b)->b.botId).filter(onlyUnique).length == $scope.fightBots.length
]
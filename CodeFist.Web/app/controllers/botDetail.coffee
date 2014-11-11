codefist = angular.module 'codefist'

codefist.controller 'BotDetailCtrl', ['$scope', '$routeParams', '$location', 'botService', 'gameService', ($scope, $routeParams, $location, botService, gameService) ->
    botService.get($routeParams.gameId, $routeParams.botId)
       .then((response) -> $scope.bot = response.data)    

    gameService.get($routeParams.gameId)
       .then((response) -> $scope.game = response.data)    

    $scope.editorOptions =
        mode: "javascript"
        lineNumbers: true
        foldGutter: true
        gutters: ['CodeMirror-linenumbers', 'CodeMirror-foldgutter', 'CodeMirror-lint-markers']
        lint: true

    $scope.saveChanges = -> botService.update($scope.bot)

    $scope.basicEditor = false;

    $scope.lineCount = (value) -> value.split('\n').length
]
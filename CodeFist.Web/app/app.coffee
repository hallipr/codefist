codefist = angular.module 'codefist', ['ngRoute', 'ui.codemirror', 'ui.bootstrap']

codefist.config ['$routeProvider', ($routeProvider) ->
    $routeProvider
        .when '/games/',
            templateUrl: '/app/views/game-list.html'
            controller: 'GameListCtrl'

        .when '/games/createGame',
            templateUrl: '/app/views/game-detail.html'
            controller: 'GameDetailCtrl'
            creatingGame: true

        .when '/games/:gameId',
            templateUrl: '/app/views/game-detail.html'
            controller: 'GameDetailCtrl'
            creatingGame: false

        .when '/games/:gameId/bots/:botId',
            templateUrl: '/app/views/bot-detail.html'
            controller: 'BotDetailCtrl',
            creatingBot: false

        .when '/games/:gameId/createBot',
            templateUrl: '/app/views/bot-detail.html'
            controller: 'BotDetailCtrl'
            creatingBot: true

        .when '/games/:gameId/fight/:botIds',
            templateUrl: '/app/views/fight-detail.html'
            controller: 'LocalFightCtrl'

        .when '/matches/:matchId',
            templateUrl: '/app/views/fight-detail.html'
            controller: 'MatchReplayCtrl'

        .when '/users/',
            templateUrl: '/app/views/user-list.html'
            controller: 'UserListCtrl'

        .when '/users/:userId',
            templateUrl: '/app/views/user-detail.html'
            controller: 'UserDetailCtrl'

        .otherwise
            redirectTo: '/games'
]

codefist.directive('contenteditable', ->
    require: 'ngModel',
    link: (scope, element, attrs, ctrl) -> 
        # view -> model
        element.bind('blur', -> 
            scope.$apply( -> ctrl.$setViewValue(element.html()))
        )

        replaceSelection = (replacement) ->
            sel = window.getSelection()
            if sel.rangeCount == 0
                return;

            range = sel.getRangeAt(0)

            original = element.html()
            start = original.substring(0, range.startOffset)
            end = original.substring(range.endOffset)
            cursorPosition = range.startOffset + replacement.length

            element.html(start + replacement + end)

            range = document.createRange()
            node = element[0].firstChild
            range.setStart(node, cursorPosition)
            range.setEnd(node, cursorPosition)
            sel.removeAllRanges()
            sel.addRange(range);           

        element.bind('keydown', (event) ->
            switch(event.keyCode)
                when 13
                    replaceSelection('\n')
                    event.preventDefault()
                when 9 # tab            
                    replaceSelection('    ')
                    event.preventDefault()
        )

        # model -> view
        ctrl.$render = -> element.html(ctrl.$viewValue)

        # load init value from DOM
        ctrl.$render()
)

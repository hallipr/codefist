codefist = angular.module 'codefist'

codefist.factory 'gameService', ['$http', 'urls', ($http, urls) ->
    query: () -> $http.get(urls.games)
    gamesByUser: (userId) -> $http.get(urls.gamesByUser(userId))
    create: (displayName) -> $http.post(urls.games, JSON.stringify(displayName))
    get: (id) -> $http.get(urls.game(id))
    delete: (id) -> $http.delete(urls.game(id))
    update: (game) -> $http.put(urls.game(game.id), game)
]